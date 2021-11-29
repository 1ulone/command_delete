using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class ScoreCounter
{
    public static int bugexecuted;
    public static int itemget;
    public static float ranamount;
}

public class ScoreSystem : MonoBehaviour
{
    float totalScore;
    bool canQuit = false;

    void Awake()
        => DontDestroyOnLoad(this.gameObject);

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(calculateScore());
        }

        if (canQuit) { Application.Quit(); }
    }

    IEnumerator calculateScore()
    {
        totalScore = (ScoreCounter.bugexecuted *1000) + (ScoreCounter.itemget*500) - (ScoreCounter.ranamount*10);
        DialogueBox.DoText($"Bug Executed : {ScoreCounter.bugexecuted} x 1000"); 
        yield return new WaitForSeconds(2f);

        DialogueBox.DoText($"Item Got : {ScoreCounter.itemget} x 500");
        yield return new WaitForSeconds(2f);

        DialogueBox.DoText($"Damage Taken : -{ScoreCounter.ranamount} x 10");     
        yield return new WaitForSeconds(3f);

        DialogueBox.DoText($"TOTAL SCORE : {totalScore}");
        yield return new WaitForSeconds(1f);

        DialogueBox.DoText($"Thank you for playing! (press escape to quit game)");
        canQuit = true;
    }
}