using UnityEngine;

namespace Items
{
    public class DeliveryPoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            PickupAction player = other.GetComponent<PickupAction>();

            if (player.cargo == null) return;

            Debug.Log("Player scored : " + player.cargo.score + " points!");

            player.deliverItem();

            DeliveryManager.instance.arrowCompass.target = null;

            DeliveryManager.instance.arrowCompass.gameObject.SetActive(false);

        }


    }

}
