using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeloDevilBodyPart : MonoBehaviour
{
    public float Damage { get; protected set; }     // Damage dealt by the enemy
    //Enum used to track what part the gameobject is
    public enum BodyPart { Head, Torso, Left_Arm, Right_Arm, Left_leg, Right_leg, bone }
    public BodyPart Part;
    public bool immune;
    private float health;
    private bool inFlight;
    float s = 1;
    //targeting vars
    Transform target;
    Vector3 player;
    Vector3 newTarget;
    Vector3 startingLayer;
    //audio source
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();

        audioSource = gameObject.GetComponent<AudioSource>();

        immune = false;
        Damage = 1;
        //Checks if part is head. the head part has the same health as the parent
        if (Part == BodyPart.Head)
        {
            health = gameObject.transform.parent.GetComponent<SkeloDevil>().health;
        }
        health = 1;
        //saves which layer the object was on
        startingLayer = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int i = 5;

        //timers
        s -= Time.deltaTime;

        //If the Part is attacking
        if (inFlight)
        {
            //makes sure the part is rendered
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Rotate(0, 0, i);
            i = i + 5;

            //moves towards target location
            if (gameObject.transform.position != player + (newTarget * 5))
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player + (newTarget * 5), 6.0f * Time.deltaTime);
            }
            else
            {
                inFlight = false;

                //the head is last to attack and so calls all parts to assemble after reaching the target location
                if (Part == BodyPart.Head)
                {
                    gameObject.transform.parent.GetComponent<SkeloDevil>().AssembleParts();
                }
                else if (Part == BodyPart.bone)
                {

                    Destroy(gameObject);
                }
            }

        }
        /*
        else if (s <= 0)
        {

            if (Part == BodyPart.Left_leg || Part == BodyPart.Right_leg)
            {
                if (foot == 1 && Part == BodyPart.Left_leg)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    foot = 2;
                }
                else if (Part == BodyPart.Left_leg) gameObject.GetComponent<SpriteRenderer>().enabled = true;
                if (foot == 2 && Part == BodyPart.Right_leg)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    foot = 1;
                }
                else if (Part == BodyPart.Right_leg) gameObject.GetComponent<SpriteRenderer>().enabled = true;

            }

            s = 1;
        }
        */
    }
    /// <summary>
    /// Moves part back to parent
    /// </summary>
    public void Assemble()
    {
        inFlight = false;
        gameObject.transform.position = new Vector3(gameObject.transform.parent.position.x, gameObject.transform.parent.position.y, startingLayer.z);
        gameObject.transform.rotation = gameObject.transform.parent.rotation;
    }
    /// <summary>
    /// sets the part enum
    /// </summary>
    /// <param name="newPart"></param>
    public void SetPart(BodyPart newPart)
    {
        Part = newPart;
    }
    /// <summary>
    /// returns part enum
    /// </summary>
    /// <returns></returns>
    public BodyPart GetBodyPart()
    {
        return Part;
    }
    /// <summary>
    /// Uses passed in Vector3 to start pathing
    /// </summary>
    /// <param name="attackLocation"></param>
    public void Attack(Vector3 attackLocation)
    {

        player = attackLocation;

        newTarget = (player - gameObject.transform.position).normalized;

        inFlight = true;
    }
    /// <summary>
    /// used when the object takes damage
    /// </summary>
    public void TakeDamage()
    {
        if (Part != BodyPart.bone)
        {
            if (!immune)
            {
                gameObject.transform.parent.GetComponent<SkeloDevil>().TakeDamage(1);
                health--;
            }

            if (Part == BodyPart.Head)
            {
                health = gameObject.transform.parent.GetComponent<SkeloDevil>().health;
            }

            if (health <= 0) Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //make player take damage
            collision.gameObject.GetComponent<PlayerMove>().KnockBack(transform.position);
        }
        else if (collision.gameObject.transform.parent != null && immune == false)
        {
            if (collision.gameObject.transform.parent.tag == "Player")
            {
                TakeDamage();
            }
        }
    }
}
