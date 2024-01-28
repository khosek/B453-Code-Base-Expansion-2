using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{

    public Transform itemDisplayPoint;

    [SerializeField] private Vector3 _rotation;

    private GameObject _childModelItem;

    public void DisplayItem(DeliveryItemData deliveryItemData)
    {
        _childModelItem = Instantiate(deliveryItemData.model, itemDisplayPoint.position, itemDisplayPoint.rotation, itemDisplayPoint);

    }

    public void ClearItem()
    {
        Destroy(_childModelItem);
        _childModelItem = null;
    }

    private void Update()
    {

        if (_childModelItem != null)
            _childModelItem.transform.Rotate(_rotation * Time.deltaTime);
    }

}
