using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    StatComponent EnemyStats;

    void Start()
    {
        EnemyStats = GetComponent<StatComponent>();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StatComponent playerStat = collision.gameObject.GetComponent<StatComponent>();
            if (collision.transform.position.y - 1f < transform.position.y)
            {
                playerStat.ModifyHealthBy(-EnemyStats.damage);
            }
            else
            {
                EnemyStats.ModifyHealthBy(-playerStat.damage);
            }

            playerStat.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 500);
        }
    }
}
