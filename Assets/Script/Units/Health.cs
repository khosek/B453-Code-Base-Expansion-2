using Items;
using UnityEngine;

namespace Units
{
    public class Health : MonoBehaviour
    {

        public PickupAction player;

        public void DamagePlayer(int damagePoints)
        {
            player.cargo.health -= damagePoints;

            HudManager.instance.SetHealth(player.cargo.health);

            if (player.cargo.health <= 0)
            {
                player.deliverItem();

                HudManager.instance.HideHud();

                AudioManager.instance.PlayNo();
            }
        }
    }
}
