using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandLineSystem : MonoBehaviour
{
    public static CommandLineSystem instance;
    
    public Font cmdpromptFont;
    public bool showHelp { get; private set; }

    bool showCmd, userhit, showItem;
    string input;

    public static DebugCommand RUN, HELP, ITEMLIST;
    public static DebugCommand<int> BUGATTACK, HEAVYATTACK, COMBOATTACK, ITEM;

    public List<object> cmdList;
    private List<string> cmdNameList;
    private GUIStyle acStyle = new GUIStyle(),
                     greenStyle = new GUIStyle();

    Vector2 scroll;

    void Awake()
    {
        instance = this;

        BUGATTACK = new DebugCommand<int>("attack:bug", "bug specialised attack on enemy", "attack:bug <enemy_position>",  (x) => 
        { BattleBaseSystem.instance.BugAttack(x);SetOffBoolean(); });

        HEAVYATTACK = new DebugCommand<int>("attack:heavy", "heavy attack on enemy", "attack:heavy <enemy_position>", (x) =>
        { BattleBaseSystem.instance.HeavyAttack(x);SetOffBoolean(); });

        COMBOATTACK = new DebugCommand<int>("attack:combo", "combo attack on enemy", "attack:combo <enemy_position>", (x) =>
        { BattleBaseSystem.instance.ComboAttack(x);SetOffBoolean(); });

        ITEM = new DebugCommand<int>("use_item", "use items on the inventory (use itemhelp to show item list)", "use_item <itemindex>", (x) =>
        { ItemLibraries.instance.UseItem(x);SetOffBoolean(); });

        RUN = new DebugCommand("run", "attempt to run from battle", "run", () =>
        { BattleBaseSystem.instance.Run();SetOffBoolean(); });

        ITEMLIST = new DebugCommand("list_item", "check item list", "list_item", () =>
        { showHelp = false;showItem = true; });

        HELP = new DebugCommand("help", "show list command", "help", () => 
        { showItem = false;showHelp = true; });

        cmdList = new List<object>
        {
            BUGATTACK,
            HEAVYATTACK,
            COMBOATTACK,
            ITEM,
            RUN,
            ITEMLIST,
            HELP
        };

        cmdNameList = new List<string>
        {
            BUGATTACK.commandFormat,
            HEAVYATTACK.commandFormat,
            COMBOATTACK.commandFormat,
            ITEM.commandFormat,
            RUN.commandFormat,
            ITEMLIST.commandFormat,
            HELP.commandFormat
        };
    }

    void Start()
    {
        acStyle.normal.textColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        acStyle.font = cmdpromptFont;
        greenStyle.normal.textColor = Color.green;
        greenStyle.font = cmdpromptFont;
    }

    public void OnToggleCommand(bool state)
        =>showCmd = state;

    public void SetOffBoolean()
    {
        showHelp = false;
        showItem = false;
    }

    public void OnEnter()
    {
        if (showCmd)
        {
            SFXController.PlaySFX(SFXData.commandenter);
            HandleInput();
            input = "";
            userhit = false;
        }
    }

    public void OnGUI()   
    {
        if (!showCmd) { return; }

        float y = Screen.height - 25;
        string acText = "help";

        List<ItemData> itemList = ItemLibraries.instance.items;  

        if (showHelp)
        {
            GUI.Box(new Rect(0, y - 75, Screen.width, 100), "");

            Rect viewPort = new Rect(0, y - 75, Screen.width - 30, 20 * cmdList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y - 70f, Screen.width, 90), scroll, viewPort);

            for (int i = 0; i < cmdList.Count; i++)
            {
                DebugCommandBase cmd = cmdList[i] as DebugCommandBase;
                string label = $"{cmd.commandFormat} - {cmd.commandDesc}";
                Rect labelRect = new Rect(5, (y-75f) + (20 * i), viewPort.width - 100, 20);

                GUI.Label(labelRect, label, greenStyle);
            }

            GUI.EndScrollView();
            y -= 100;
        } else 
        if (showItem)
        {
            GUI.Box(new Rect(0, y - 75, Screen.width, 100), "");

            Rect viewPort = new Rect(0, y - 75, Screen.width - 30, 20 * itemList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y - 70f, Screen.width, 90), scroll, viewPort);

            for (int i = 0; i < itemList.Count; i++)
            {
                ItemData item = itemList[i] as ItemData;
                string label = $"{item.ITEMname} - Heal Amount:{item.HEALamount}";
                Rect labelRect = new Rect(5, (y-75f) + (20 * i), viewPort.width - 100, 20);

                GUI.Label(labelRect, label, greenStyle);
            }

            GUI.EndScrollView();
            y -= 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(1, 1, 1, 1f);

        Event enter = Event.current;
        
        if (enter.keyCode == KeyCode.Return && input.Length != 0) { userhit = true;OnEnter(); } else 
        if (userhit == false) 
        { 
            input = GUI.TextField(new Rect(10f, y - 5f, Screen.width - 20f, 20f), input, greenStyle); 
            if (!string.IsNullOrEmpty(input) && input.Length > 0)
            {
                List<string> found = cmdNameList.FindAll(w => w.StartsWith(input));
                if (found.Count > 0)
                    acText = found[0];
            }
        }

        GUI.Label(new Rect(12.75f, y - 5f, Screen.width -20f, 20f), acText, acStyle);
    }
    
    public void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < cmdList.Count; i++)
        {
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            DebugCommandBase cmdBase = cmdList[i] as DebugCommandBase;

            if (input.Contains(cmdBase.commandID))
            {
                if (cmdList[i] as DebugCommand != null)
                {
                    (cmdList[i] as DebugCommand).Invoke();
                } else 
                if (cmdList[i] as DebugCommand<int> != null)
                {
                    (cmdList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            } 
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        
    }
}
