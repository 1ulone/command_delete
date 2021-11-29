using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class DialogueBoxSystem : MonoBehaviour 
{
    public static DialogueBoxSystem dialogueBoxSystem;

    [SerializeField]
    private Text textLabel;
    [SerializeField]
    private float typeSpeed = 50f;
    [SerializeField]
    private int lineMax = 12;

    bool ExecuteCoroutine;
    int currLine;

    void Start() 
    { 
        dialogueBoxSystem = this;
        ExecuteCoroutine = false;
    }
    
    public void StartTyping(string textToType)
    {
        if (!ExecuteCoroutine)
            StartCoroutine(typeText(textToType));
    }

    IEnumerator typeText(string texttotype)
    {
        ExecuteCoroutine = true;
        float t = 0;
        int charIndex = 0;
        string oldLabel = "";

        if (currLine >= lineMax) { textLabel.text = ""; }
        oldLabel = textLabel.text;

        while (charIndex < texttotype.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, texttotype.Length);

            textLabel.text = (oldLabel == ""? oldLabel : oldLabel + "\n") + texttotype.Substring(0, charIndex);

            yield return null;
        }

        textLabel.text = (oldLabel == ""? oldLabel : oldLabel + "\n") + texttotype;
        currLine++;
        ExecuteCoroutine = false;
    }
}

public static class DialogueBox
{
    public static void DoText(string text) { DialogueBoxSystem.dialogueBoxSystem.StartTyping(text); }
}
