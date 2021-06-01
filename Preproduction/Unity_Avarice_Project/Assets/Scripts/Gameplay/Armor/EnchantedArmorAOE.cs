using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantedArmorAOE : MonoBehaviour
{

    public float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (attackTimer <= 0)
            {
                collision.gameObject.GetComponent<Skeleton>().TakeDamage(1f);
                attackTimer = 1;
                Camera.main.GetComponent<ScreenShake>().Shake(0.5f, 0.2f, 1.5f);
            }
        }
    }
}
