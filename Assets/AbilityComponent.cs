using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    public bool ShooterPickedUp = false;
    public GameObject projectile;

    public void Update()
    {
        if (ShooterPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject temp;
                temp = Instantiate(projectile);
                temp.GetComponent<DamageComponent>().Spawner = this.gameObject;

                bool goingRight = false;
                if (transform.rotation.y != 0) goingRight = true;
                temp.transform.position = new Vector3 (this.transform.position.x - (goingRight ? 1 : -1), this.transform.position.y - 0.3f, this.transform.position.z);
                temp.GetComponent<projectile>().x = goingRight? -10f : 10f;
                temp.GetComponent<projectile>().y = 0f;
            }
        }
    }

    public IEnumerator RemoveShooter()
    {
        yield return new WaitForSeconds(30f);
        ShooterPickedUp = false;
    }
}
