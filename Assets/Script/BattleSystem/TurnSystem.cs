using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem instance;

    [HideInInspector]
    public bool onTurn, endBattle;

    [SerializeField]
    private GameObject fade, ovParent;
    [SerializeField]
    public float maxturntimer;
    [SerializeField]
    public float turntimerMultiplier;

    private float turntimer;

    void OnEnable() => startPanel();
    void Start() => startPanel();

    void startPanel()
    {
        instance = this;
        turntimer = 0;
        onTurn = false;
        endBattle = false;
        turntimerMultiplier = 1;
    }

    void Update()
    {
        if (!endBattle)
        {
            if (onTurn) 
                turntimer = maxturntimer*turntimerMultiplier; onTurn =false; 

            if (turntimer > 0) { turntimer -= Time.deltaTime; } else 
            if (turntimer <= 0) { CommandLineSystem.instance.OnToggleCommand(true);turntimerMultiplier = 1; }
        } 
        else 
        {
            fade.SetActive(true);
            Invoke("DisableBattlePanel", 1f);
        }
    }

    void DisableBattlePanel() 
    {
        MusicChanger.ChangeMusic(0, true);

        OptionsSystem.instance.DestroyEnemies();
        CommandLineSystem.instance.OnToggleCommand(false);
        BattleSystem.instance.ResetEncounter();

        ovParent.SetActive(true);
        if (EncounterStart.boss) { LevelController.ResetLevel(); }
        this.gameObject.SetActive(false);
    }
}
