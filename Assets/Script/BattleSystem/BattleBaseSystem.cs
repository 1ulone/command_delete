using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBaseSystem : MonoBehaviour
{
    public static BattleBaseSystem instance;

    private float damageMultiplier, maxhealth, bugdamage;
    private int attackCount, bugcount;

    void Awake() 
        => instance = this;

    void OnEnable()
    { 
        bugcount = 1;
        bugdamage = 1;
    }

    public void BugAttack(int i)
    {
        int r = Random.Range(0, 2), a = Random.Range(0, 2);
        bugcount = r == 0? bugcount : bugcount+1;
        bugdamage = a == 0? bugdamage : bugdamage+1;
        StartCoroutine(OptionsSystem.instance.WaitAttacking(i-1, bugcount, bugdamage, AttackType.BUFF));
    }

    public void HeavyAttack(int i)
    {
        attackCount = bugcount;
        damageMultiplier = Random.Range(2, 5);
        TurnSystem.instance.turntimerMultiplier = 2;
        StartCoroutine(OptionsSystem.instance.WaitAttacking(i-1, attackCount, damageMultiplier, AttackType.BREAKER));
    }

    public void ComboAttack(int i)
    {
        attackCount = Random.Range(2, 4);
        damageMultiplier = bugdamage;
        StartCoroutine(OptionsSystem.instance.WaitAttacking(i-1, attackCount, damageMultiplier, AttackType.SWIFT));
    }

    public void Run()
    {   
        if (EncounterStart.boss) { return; }

        bugcount = 1;bugdamage = 1;
        CommandLineSystem.instance.OnToggleCommand(false);
        OptionsSystem.instance.ResetTurn();
        int rr = Random.Range(0, 100);

        if (rr <= 45) 
        {
            DialogueBox.DoText(OptionsSystem.instance.pData.playername + " were not able to escape");   
        } else 
        if (rr > 45)
        {
            ScoreCounter.ranamount++;
            DialogueBox.DoText("Escaping.....");
            Invoke("CloseBattleScreen", 1f);
        }
    }

    void CloseBattleScreen() => TurnSystem.instance.endBattle = true;
}
