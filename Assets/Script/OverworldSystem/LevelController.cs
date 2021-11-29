using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static bool resetedlevel;
    public static int level = 1;

    private static MonoBehaviour lvlcon;

    [SerializeField]
    private Font cmdfnt;
    private GUIStyle greenStyle = new GUIStyle();   

    void Start()
    {
        lvlcon = this;
        greenStyle.normal.textColor = Color.green;
        greenStyle.font = cmdfnt;
        resetedlevel = false;
    }

    public void OnGUI()
    {
        Rect labelRect = new Rect(5f, 5f, Screen.width, 32f);
        GUI.Label(labelRect, $"Firewall Level : {level}", greenStyle);
    }

    public static void ResetLevel()
    {
        if (!resetedlevel)
        {
            level += 1;
            if (level < 5) { AbstractDungeonGenerator.instance.GenerateDungeon(); HealthSystem.instance.ResetHealth(); }
            else if (level >= 5) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); }
            resetedlevel = true;
            lvlcon.Invoke("returnBool", 3f);
        }
    }

    void returnBool() => resetedlevel = false;
}
