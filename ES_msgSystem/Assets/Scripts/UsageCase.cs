using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ES_MsgSystem;

[RequireComponent(typeof(MsgSystem))]
public class UsageCase : MonoBehaviour
{
    private MsgSystem msgSys;
    public UnityEngine.UI.Text uiText;
    public TextAsset textAsset;
    private List<string> textList = new List<string>();
    private int textIndex = 0;
    void Start()
    {
        msgSys = this.GetComponent<MsgSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
            ReadTextDataFromAsset(textAsset);
    }
    private void ReadTextDataFromAsset(TextAsset _textAsset)
    {
        textList.Clear();
        textList = new List<string>();
        textIndex = 0;
        var lineTextData = _textAsset.text.Split('\n');
        foreach (string line in lineTextData)
        {
            textList.Add(line);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //You can sending the messages from strings or text-based files.
            if (msgSys.IsCompleted)
            {
                msgSys.SetText("Send the messages![lr] HelloWorld![w]");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }
        //If the message is complete, stop updating text.
        if (msgSys.IsCompleted == false)
        {
            uiText.text = msgSys.text;
        }
        //Auto update from textList.
        if (msgSys.IsCompleted == true && textIndex < textList.Count)
        {
            msgSys.SetText(textList[textIndex]);
            textIndex++;
        }
    }
}
