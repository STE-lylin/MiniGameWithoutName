using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth = 100;

    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        currentHealth = maxHealth-30;
        UpdateHealthBar();
    }

    // Update is called once per frame
    public void SetHealth(int value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        Debug.Log(currentHealth + " " + maxHealth);
        float ratio = (float)currentHealth / maxHealth;
        Debug.Log(ratio);
        healthBar.fillAmount = ratio;
    }
}
