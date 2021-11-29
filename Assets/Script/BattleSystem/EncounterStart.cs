using UnityEngine;

public class EncounterStart : MonoBehaviour
{
    public static bool boss;

    [SerializeField]
    private GameObject[] EnemyPrefab;
    [SerializeField]
    private GameObject bossPrefab;

    [SerializeField]
    private Transform[] EnemyTransform;

    int EnemyCount;
    bool spawned;

    void OnEnable()
    {
        EnemyCount = Random.Range(1, 4);
        spawned = false;
        StartEncounter();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && spawned) { DeleteEncounter(); StartEncounter(); }
    }

    void StartEncounter()
    {
        if (!spawned) 
        {
            SFXController.PlaySFX(SFXData.encounter);
            if (!boss)
            {
                MusicChanger.ChangeMusic(1, true);
                for (int i = 0; i < EnemyCount; i++)
                {
                    GameObject enemyp = Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)], EnemyTransform[i].position, Quaternion.identity, this.transform);
                    if (i+1 >= EnemyCount) { spawned = true; }
                }
            } else
            if (boss) 
            {
                MusicChanger.ChangeMusic(2, true);
                Instantiate(bossPrefab, EnemyTransform[0].position + new Vector3(50, 0, 0), Quaternion.identity, this.transform);
            }
        }
    }

    void DeleteEncounter()
    {
        GameObject[] en = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var item in en) { Destroy(item); }
        EnemyCount = Random.Range(1, 4);
        spawned = false;
    }
}
