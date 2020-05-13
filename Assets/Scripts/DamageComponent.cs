using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
   
    StatComponent EnemyStats;
    [Header("Base Stats")]
    public DamageType damageType = default;
    private int layer = 0;
    public float LaunchForce = 10f;

    [Header("Trap Stats")]
    public float trapDamage = 10f;
    public TrapType trapType = default;
    public List<DamageComponent> conjoinedTraps = new List<DamageComponent>();

    [Header("Spawner")]
    public GameObject Spawner;

    void Start()
    {
        if (damageType == DamageType.Enemy)
        EnemyStats = GetComponent<StatComponent>();

        if (trapType == TrapType.Rotating) foreach(Transform child in transform.parent.transform) conjoinedTraps.Add(child.GetComponent<DamageComponent>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PerformDamage(collision.gameObject.GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        PerformDamage(collision);
    }

    private void PerformDamage(Collider2D collision)
    {
        if (Spawner != null) if (Spawner == collision.gameObject) return;

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemies")
        {
            StatComponent hitStat = collision.gameObject.GetComponent<StatComponent>();
            if (!hitStat.CanBeDealtDamage) return;

            switch (damageType)
            {
                case DamageType.Null:
                    break;

                case DamageType.Enemy:
                    if (collision.transform.position.y - 1f < transform.position.y) hitStat.ModifyHealthBy(-EnemyStats.damage, 1f);
                    else EnemyStats.ModifyHealthBy(-hitStat.damage, 1f);
                    break;

                case DamageType.Trap:
                    hitStat.ModifyHealthBy(-trapDamage,1f);
                    break;

                case DamageType.Projectile:
                    hitStat.ModifyHealthBy(-trapDamage,0.1f);
                    StopCoroutine(GetComponent<projectile>().DestroySelf(0f));
                    StartCoroutine(GetComponent<projectile>().DestroySelf(0.1f));
                    break;
                default:
                    break;
            }

            if (trapType != TrapType.Rotating) TempDisable();
            else foreach (DamageComponent dc in conjoinedTraps) dc.TempDisable();
            StartCoroutine(LaunchPlayer(hitStat.gameObject.GetComponent<Rigidbody2D>()));
        }
    }

    public void TempDisable()
    {
        layer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ignore");
        StartCoroutine(EnableAgain());
    }

    public IEnumerator EnableAgain()
    {
        yield return new WaitForSeconds(1f);
        gameObject.layer = layer;
    }

    public IEnumerator LaunchPlayer(Rigidbody2D rigid)
    {
        yield return new WaitForSeconds(0.01f);
        rigid.velocity = new Vector2(rigid.velocity.x, LaunchForce);
    }
}

public enum DamageType {Null, Enemy, Trap, Projectile}
public enum TrapType {Null, Rotating}
