using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCommandLineSystem : MonoBehaviour
{
    public static DebugCommand PLAY, EXIT;

    public Font cmdpromptFont;

    bool showCmd, userhit;
    string input;

    public List<object> titleCmdList;
    private List<string> titleCmdNameList;
    private GUIStyle acStyle = new GUIStyle(),
                     greenStyle = new GUIStyle();

    void Awake()
    {
        PLAY = new DebugCommand("Load*1", "execute game", "Load*1", () => 
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); });

        EXIT = new DebugCommand("Exit*1", "exit game", "Exit*1", () => 
        { Application.Quit(); });

        titleCmdList = new List<object>
        {
            PLAY,
            EXIT
        };

        titleCmdNameList = new List<string>
        {
            PLAY.commandFormat,
            EXIT.commandFormat
        };
    }

    void Start()
    {
        acStyle.normal.textColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        acStyle.font = cmdpromptFont;
        greenStyle.normal.textColor = Color.green;
        greenStyle.font = cmdpromptFont;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { OnToggleCommand(true); }
    }

    public void OnToggleCommand(bool state)
    {
        showCmd = state;
    }

    public void OnEnter()
    {
        if (showCmd)
        {
            HandleInput();
            input = "";
            userhit = false;
        }
    }

    void OnGUI()
    {
        if (!showCmd) { return; }

        float y = Screen.height - 25;
        string acText = titleCmdNameList[0];

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(1, 1, 1, 1f);

        Event enter = Event.current;
        
        if (enter.keyCode == KeyCode.Return && input.Length != 0) { userhit = true;OnEnter(); } else 
        if (userhit == false) 
        { 
            input = GUI.TextField(new Rect(10f, y - 5f, Screen.width - 20f, 20f), input, greenStyle); 
            if (!string.IsNullOrEmpty(input) && input.Length > 0)
            {
                List<string> found = titleCmdNameList.FindAll(w => w.StartsWith(input));
                if (found.Count > 0)
                    acText = found[0];
            }
        }

        GUI.Label(new Rect(12.75f, y - 5f, Screen.width -20f, 20f), acText, acStyle);
    }

    public void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < titleCmdList.Count; i++)
        {
            DebugCommandBase cmdBase = titleCmdList[i] as DebugCommandBase;

            if (input.Contains(cmdBase.commandID))
            {
                if (titleCmdList[i] as DebugCommand != null)
                    (titleCmdList[i] as DebugCommand).Invoke();
            }
        }
    }
}
