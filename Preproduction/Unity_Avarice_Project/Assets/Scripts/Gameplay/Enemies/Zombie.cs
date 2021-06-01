using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{

    AudioSource audioSource;
    float jumpTowardsPlayer = 0;
    float jumpTimer = 1;
    float jumpDistance = 2;
    int zombieHealth = 5;
    Transform target;
    float idleAngle = 0.0f;
    float idleChange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // finds GameObject named Player in scene.
        target = GameObject.FindWithTag("Player").transform;
        audioSource = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if player exists and is in the range then aggro/attack
        if(GameObject.Find("Player(Clone)") != null && Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) <= 10)
        {
            Aggro();
            Attack();
        }
        // Otherwise idle
        else
        {
            Idle();
        }
    }

    // Zombie and arrow collision interactions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().KnockBack(transform.position);
        }
        if (collision.collider.tag == "Arrow")
        {
            //does nothing to zombie health
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == "FireArrow")
        {
            zombieHealth -= 1;
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == "BombArrow")
        {
            zombieHealth -= 2;
            Destroy(collision.collider.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMove>().KnockBack(transform.position);
        }
        else if (collision.gameObject.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.tag == "Player")
            {
                TakeDamage(3);
            }
        }
    }

    /// <summary>
    /// Enemy idle behavior
    /// </summary>
    public override void Idle()
    {
        transform.rotation = Quaternion.AngleAxis(idleAngle, Vector3.forward);
        idleAngle += idleChange;

        if(idleAngle >= 90 || idleAngle <= -90)
        {
            idleChange *= -1f;
        }
    }

    /// <summary>
    /// Enemy aggro behavior
    /// </summary>
    public override void Aggro()
    {
        var dir = target.position - transform.position;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// Enemy attack behavior
    /// </summary>
    public override void Attack()
    {
        //determines weather hermit should jump towards or away from player every second and then does it
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0)
        {
            jumpTowardsPlayer = Random.Range(1, 3);
            if (jumpTowardsPlayer <= 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, jumpDistance);
                AudioManager.Play(AudioClipName.vsSkeletonWalk, audioSource);
            }
            if (jumpTowardsPlayer > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, -target.position, jumpDistance);
                AudioManager.Play(AudioClipName.vsSkeletonWalk, audioSource);
            }
            jumpTimer = 1;
        }
    }

    /// <summary>
    /// Deal damage to the enemy
    /// </summary>
    /// <param name="damage">Amount of damage to do</param>
    public override void TakeDamage(float damage)
    {
        zombieHealth -= (int)damage;

        //Checks to see if zombie should be dead
        if (zombieHealth <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// Enemy death behavior
    /// </summary>
    public override void Death()
    {
        base.Death();
        gameObject.AddComponent<Pickup>();
        gameObject.GetComponent<Pickup>().DropMoney(5, 15, gameObject);
        AudioManager.Play(AudioClipName.vsSkeletonAttack, audioSource);

        Destroy(gameObject);
    }
}
