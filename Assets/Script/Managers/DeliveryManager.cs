using UI;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public GameObject[] deliveryPoints;

    public static DeliveryManager instance { get; private set; }

    public ArrowCompass arrowCompass;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    public GameObject GetDeliveryPoint()
    {
        GameObject newDeliveryPoint = deliveryPoints[Random.Range(0, deliveryPoints.Length)];

        newDeliveryPoint.SetActive(true);

        arrowCompass.gameObject.SetActive(true);

        arrowCompass.target = newDeliveryPoint.transform;

        return newDeliveryPoint;
    }

}
