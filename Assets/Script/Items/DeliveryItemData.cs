using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Delivery Item", menuName = "Delivery Item")]
    public class DeliveryItemData : ScriptableObject
    {
        public string name = "";

        public int health = 3;

        public int deliveryTime = 0;

        public int score = 0;

        public GameObject model = null;

        public GameObject damagedModel = null;
    }

}
