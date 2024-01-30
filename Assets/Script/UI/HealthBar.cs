using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI textMeshPro;

    [SerializeField] private int _maxHealth = 10;

    [SerializeField] private int _currentHealth = 10;


    public void SetHealth(int health)
    {
        slider.value = health;

        _currentHealth = health;

        SetText(_currentHealth + " $");

    }

    public void SetMaxHealth(int maxHealth)
    {

        slider.minValue = 0;

        slider.maxValue = maxHealth;

        slider.value = maxHealth;

        _maxHealth = maxHealth;

        SetText(_maxHealth + " $ ");
    }

    public void SetText(string newText)
    {
        textMeshPro.text = newText;
    }
}
