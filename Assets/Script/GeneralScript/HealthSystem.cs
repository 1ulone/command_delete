using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem instance;

    public static float health;

    public GameObject gameoverpanel;
    public PlayerData playerData;
    public Image healthbar;

    float maxhealth;

    void Start()
    {
        instance = this;
        health = playerData.health;
        maxhealth = health;
    }

    void Update()
    {
        if (health >= maxhealth) { health = maxhealth; }
        healthbar.fillAmount = health/maxhealth;

        if (health <= 0) 
        { 
            MusicChanger.StopMusic();
            CommandLineSystem.instance.OnToggleCommand(false); 

            SFXController.PlaySFX(SFXData.error);
            gameoverpanel.SetActive(true);

            Invoke("QuitGame", 1f); 
        }
    }

    void QuitGame()
        => Application.Quit();

    public void ResetHealth()
        => health = maxhealth;
}
