using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatComponent : MonoBehaviour
{
    public float minHP = 0;
    public float maxHP = 100;
    public float currentHP = 100;
    public float damage = 50;

    private Canvas MainUI;
    private Canvas DeathUI;

    private Slider hpSlider;

    public bool CanBeDealtDamage = true;

    void Start()
    {
        MainUI = GameObject.Find("Main_UI").GetComponent<Canvas>();
        DeathUI = GameObject.Find("Death_UI").GetComponent<Canvas>();
        hpSlider = GameObject.Find("Slider").GetComponent<Slider>();

        hpSlider.minValue = minHP;
        hpSlider.maxValue = maxHP;
    }

    private void Update()
    {
        hpSlider.value = currentHP;
    }

    public void ModifyHealthBy(float by)
    {
        if (CanBeDealtDamage)
        {
            currentHP += by;
            if (currentHP <= 0) OnDeath();
            if (currentHP > maxHP) currentHP = maxHP;

            if (tag == "Player" & by < 0)
            {
                GetComponent<Animator>().SetTrigger("IsHurt");
            }

            if (by < 0) { CanBeDealtDamage = false; StartCoroutine(CanBeDamagedAgain()); }
        }
    }

    void OnDeath()
    {

        //Time.timeScale sets the speed of the game. 0 = 0% , 0.1f = 10% , 1 = 100%
        if (tag == "Player")
        {
            MainUI.enabled = false;
            DeathUI.enabled = true;
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
