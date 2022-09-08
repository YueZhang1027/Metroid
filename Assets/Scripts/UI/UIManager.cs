using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text healthText;
    public GameObject deathPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void SetHealth(int newHealth) 
    {
        healthText.text = newHealth.ToString();
    }

    public void OnDeath() 
    {
        deathPanel.SetActive(true);
    }
}
