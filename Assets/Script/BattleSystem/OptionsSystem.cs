using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSystem : MonoBehaviour
{
    public static OptionsSystem instance;

    public PlayerData pData;

    [SerializeField]
    private GameObject slash, uiEffectParent, player;
    [SerializeField]
    private DialogueBoxSystem DBSystem;

    private GameObject[] en;

    void Start()
    {
        instance = this;
        startOS();
    }

    void OnEnable() => startOS();

    void startOS()
    {
        CommandLineSystem.instance.OnToggleCommand(true);
        DialogueBox.DoText("A wild bug appeared!");
    }

    void Update()
    {
        en = GameObject.FindGameObjectsWithTag("Enemy");
        if (en.Length == 0 && !TurnSystem.instance.endBattle) 
        { 
            MusicChanger.ChangeMusic(3, false);
            StartCoroutine(EndBattle("Bugs defeated!," + pData.playername + " won!")); 
        }
    }

    public IEnumerator WaitAttacking(int enemyID, int atkCount, float damageMult, string attackType)
    {
        CommandLineSystem.instance.OnToggleCommand(false);
        ResetTurn();

        int r = 0;
        float damageToEnemy;
        InBattleEnemySystem currEnemy = en[enemyID].GetComponent<InBattleEnemySystem>();

        if (enemyID > en.Length-1) { enemyID = en.Length-1; } else 
        if (enemyID < 0) { enemyID = 0; }

        for (int i = 0; i < atkCount; i++)
        {
            damageToEnemy = Mathf.Abs((pData.damage * damageMult * (pData.critMultiplier)+ Random.Range(-1, pData.luck)) - 
                            (currEnemy.thisdef + Random.Range(-3, 4)));
            r = Random.Range(0, 2);

            currEnemy.hp -= damageToEnemy;
            SFXController.PlaySFX(SFXData.attack);
            Instantiate(slash, en[enemyID].transform.position, new Quaternion(Quaternion.identity.x, 
                                                                              Quaternion.identity.y + r == 0? 0 : -90, 
                                                                              Quaternion.identity.z, Quaternion.identity.w), uiEffectParent.transform);
            DialogueBox.DoText(currEnemy.data.enemyName + " takes " + damageToEnemy + " damage!");
            switch(attackType)
            {
                case AttackType.BREAKER:
                {
                    if (currEnemy.state != EnemyState.BREAK) { currEnemy.OnBreak(); } else 
                    if (currEnemy.state == EnemyState.RAGE)  { currEnemy.OnNeutral(); } else
                    if (currEnemy.state == EnemyState.BREAK) { currEnemy.OnRage(); }
                    currEnemy.ResetInvokeTimer();
                } break;
                case AttackType.SWIFT:
                {
                    if (currEnemy.state != EnemyState.NEUTRAL) { currEnemy.OnRage(); } else 
                    { currEnemy.OnDefensive(); }
                    currEnemy.ResetInvokeTimer();
                } break;
                case AttackType.BUFF:
                {
                    if (currEnemy.state != EnemyState.DEFENSIVE) { currEnemy.OnDefensive(); } else
                    { currEnemy.OnBreak(); }
                    currEnemy.ResetInvokeTimer();
                } break;
            }

            yield return new WaitForSeconds(1f);
            if (en.Length == 0) { break; }
        }
    }

    //Reset Turn and Continue to next
    public void ResetTurn() 
    {
        TurnSystem.instance.onTurn = true;
    }

    IEnumerator EndBattle(string text)
    {
        CommandLineSystem.instance.OnToggleCommand(false);
        Destroy(BattleSystem.instance.glitchobj);
        TurnSystem.instance.onTurn = false;

        yield return new WaitForSeconds(2f);
        DialogueBox.DoText(text);
        TurnSystem.instance.endBattle = true;
    }

    public void PlayerHurt(EnemyData data, float dmg)
    {
        int r = Random.Range(0, 2);
        float damagetoPlayer = Mathf.Abs((dmg * data.critMultiplier) - (pData.defense - Random.Range(-pData.luck, pData.luck)));

        player.GetComponent<InBattlePlayerSystem>().Shake();
        HealthSystem.health -= damagetoPlayer;
        SFXController.PlaySFX(SFXData.attack);
        Instantiate(slash, player.transform.position, new Quaternion(Quaternion.identity.x, 
                                                                     Quaternion.identity.y + r == 0? 0 : -90, 
                                                                     Quaternion.identity.z, Quaternion.identity.w), uiEffectParent.transform);
        DialogueBox.DoText(data.enemyName + " Attacks! " + pData.playername + ", You" + " Takes " + damagetoPlayer + " Damage!");
    }

    public void DestroyEnemies()
    {
        if (en.Length >= 0) 
        {
            for (int i = 0; i < en.Length; i++)
                Destroy(en[i].gameObject);
        }
    }
}