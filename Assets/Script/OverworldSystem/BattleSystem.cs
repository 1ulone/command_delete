using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;

    [SerializeField]
    private GameObject panel, ovParent, fade;
    public GameObject glitchobj { get; private set; }
    
    [HideInInspector]
    public bool encounter;

    void Start()
    {
        instance = this;
        encounter = false;
    }

    public void ResetEncounter() => encounter = false;

    IEnumerator EnterBattle(bool doBoss)
    {
        EncounterStart.boss = doBoss;
        fade.SetActive(true);
        encounter = true;

        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
        ovParent.SetActive(false);
    }

    public void StartBattle(bool doBoss, GameObject _glitchobj)
    {
        glitchobj = _glitchobj;
        StartCoroutine(EnterBattle(doBoss));
    }
}