using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : MonoBehaviour
{
    public float minHP = 0;
    public float maxHP = 100;
    public float currentHP = 100;
    public float damage = 50;

    public bool CanBeDealtDamage = true;

    void Start()
    {
        
    }

    public void ModifyHealthBy(float by)
    {
        currentHP += by;
        if (currentHP <= 0) OnDeath();
        if (currentHP > maxHP) currentHP = maxHP;

        if (tag == "Player" & by < 0) {
            GetComponent<Animator>().SetTrigger("IsHurt");
        }

        if (by < 0) { CanBeDealtDamage = false; StartCoroutine(CanBeDamagedAgain()); }
    }

    void OnDeath()
    {
        if (tag == "Player")
        {
            Time.timeScale = 0;
        }
        else
        {
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator DestroySelf()
    {
        GetComponent<Animator>().SetTrigger("IsDead");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);

        StopCoroutine(DestroySelf());
    }

    IEnumerator CanBeDamagedAgain()
    {
        yield return new WaitForSeconds(3.5f);
        CanBeDealtDamage = true;

        StopCoroutine(CanBeDamagedAgain());
    }
}
