using UnityEngine;
using UI;

namespace Items
{
    public class DeliveryPoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            PickupAction player = other.GetComponent<PickupAction>();

            if (player.cargo == null) return;

            GameManager.instance.AddScore(player.cargo);

            player.deliverItem();

            HudManager.instance.HideHud();

            AudioManager.instance.PlayDrop();

        }


    }

}
