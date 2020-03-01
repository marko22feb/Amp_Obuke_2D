using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int GemAmount = 10;
    public GameObject GemPrefab;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (GemAmount > 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.transform.position.y + .75f < this.transform.position.y)
                {
                    GemAmount--;
                    GameObject temp;
                    temp = Instantiate(GemPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1, 0), this.transform.rotation);
                    temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150f, 150f), 250f));
                }
            }
        }
    }
}
