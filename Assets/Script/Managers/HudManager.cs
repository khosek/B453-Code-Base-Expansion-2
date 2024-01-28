using Items;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager instance { get; private set; }

    public TextMeshProUGUI deliveryTimer;

    public TextMeshProUGUI score;

    public ArrowCompass arrowCompass;

    public TextMeshProUGUI itemName;

    public GameObject itemNamePanel;

    public GameObject itemDisplayPanel;

    public ItemDisplay itemDisplay;

    public GameObject itemHealthPanel;

    public HealthBar itemHealthBar;

    public GameObject gameOverPanel;


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

    private void Start()
    {
        HideHud();
        gameOverPanel.SetActive(false);
    }

    public void HideHud()
    {
        itemNamePanel.SetActive(false);

        itemDisplayPanel.SetActive(false);

        itemHealthPanel.SetActive(false);

        arrowCompass.target = null;

        arrowCompass.gameObject.SetActive(false);

        itemDisplay.ClearItem();
    }

    public void SetItemDetails(DeliveryItemData deliveryItemData)
    {
        SetItemName(deliveryItemData.name);

        SetMaxHealth(deliveryItemData.health);

        itemDisplayPanel.SetActive(true);

        itemDisplay.DisplayItem(deliveryItemData);

    }

    public void SetItemName(string newText)
    {
        itemNamePanel.SetActive(true);

        itemName.text = newText;
    }

    public void SetHealth(int value)
    {
        itemHealthBar.SetHealth(value);
    }

    public void SetMaxHealth(int value)
    {
        itemHealthPanel.SetActive(true);

        itemHealthBar.SetMaxHealth(value);
    }

    public void SetArrowDirection(GameObject deliveryPoint)
    {
        arrowCompass.gameObject.SetActive(true);

        arrowCompass.target = deliveryPoint.transform;
    }

    public void SetTimerSeconds(int seconds)
    {
        deliveryTimer.text = seconds + " Secs";
    }

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void SetScore(string newScore)
    {
        score.text = newScore;
    }
}
