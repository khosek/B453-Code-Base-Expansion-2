using UnityEngine;

namespace Items
{
    public class PickupItem : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;
        public DeliveryItemData deliveryItemData;

        private GameObject childModelItem;

        void Start()
        {
            childModelItem = Instantiate(deliveryItemData.model, transform.position, transform.rotation, transform);
        }

        private void Update()
        {
            childModelItem.transform.Rotate(_rotation * Time.deltaTime);
        }
    }

}
