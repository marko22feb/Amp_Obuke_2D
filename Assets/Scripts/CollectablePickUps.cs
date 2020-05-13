using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePickUps : MonoBehaviour
{
    public CollectableType collectableType;
    private GameObject Player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player = collision.gameObject;
            PickedUp();
            Destroy(this.gameObject);
        }
    }

    public void PickedUp()
    {
        switch (collectableType)
        {
            case CollectableType.Gem:
                GameController.Control.GemAmount++;
                GameController.Control.UpdateGemText();
                break;
            case CollectableType.PowerUp:
                break;
            case CollectableType.Shooter:
                Player.GetComponent<AbilityComponent>().ShooterPickedUp = true;
                StartCoroutine (Player.GetComponent<AbilityComponent>().RemoveShooter());
                break;
            default:
                break;
        }
    }
}
