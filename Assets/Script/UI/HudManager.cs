using TMPro;
using UI;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public static HudManager instance { get; private set; }

    public TextMeshProUGUI itemName;

    public GameObject itemNamePanel;

    public GameObject itemNamePanel;

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
        itemNamePanel.SetActive(false);
    }

    public void SetItemName(string newText)
    {
        itemNamePanel.SetActive(true);

        itemName.text = newText;
    }
}
