using Items;
using UnityEngine;

namespace Units
{
    public class Health : MonoBehaviour
    {

        public PickupAction player;

        public void DamagePlayer(int damagePoints)
        {
            Debug.Log("Player has a cargo with: " + player.cargo.health + " health points.");

            player.cargo.health -= damagePoints;

            Debug.Log("Player cargo was damaged ! it now have : " + player.cargo.health + " health points.");
        }
    }
}
