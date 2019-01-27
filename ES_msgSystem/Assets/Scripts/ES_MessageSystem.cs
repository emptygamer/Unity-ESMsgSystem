using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RemptyTool.ES_MessageSystem
{
    /// <summary>The messageSystem is made by Rempty EmptyStudio. 
    /// UserFunction
    ///     SetText(string) -> Make the system to print or execute the commands.
    ///     Next()          -> If the system is WaitingForNext, then it will continue the remaining contents.
    ///     AddSpecialCharToFuncMap(string _str, Action _act)   -> You can add your customized special-characters into the function map.
    /// Parameters
    ///     IsCompleted     -> Is the input text parsing completely by the system.
    ///     text            -> The result, witch you can show on your interface as a dialog.
    ///     IsWaitingForNext-> Waiting for user input -> The Next() function.
    ///     textSpeed       -> Setting the updating period of text.
    /// </summary> 
    public class ES_MessageSystem : MonoBehaviour
    {
        public bool IsCompleted { get { return IsMsgCompleted; } }
        public string text { get { return msgText; } }
        public bool IsWaitingForNext { get { return IsWaitingForNextToGo; } }
        public float textSpeed = 0.01f; //Updating period of text. The actual period may not less than deltaTime.

        private const char SPECIAL_CHAR_STAR = '[';
        private const char SPECIAL_CHAR_END = ']';
        private enum SpecialCharType { StartChar, CmdChar, EndChar, NormalChar }
        private bool IsMsgCompleted = true;
        private bool IsOnSpecialChar = false;
        private bool IsWaitingForNextToGo = false;
        private bool IsOnCmdEvent = false;
        private string specialCmd = "";
        private string msgText;
        private char lastChar = ' ';
        private Dictionary<string, Action> specialCharFuncMap = new Dictionary<string, Action>();

        void Start()
        {
            //Register the Keywords Function.
            specialCharFuncMap.Add("w", () => StartCoroutine(CmdFun_w_Task()));
            specialCharFuncMap.Add("r", () => StartCoroutine(CmdFun_r_Task()));
            specialCharFuncMap.Add("l", () => StartCoroutine(CmdFun_l_Task()));
            specialCharFuncMap.Add("lr", () => StartCoroutine(CmdFun_lr_Task()));
        }

        #region Public Function
        public void AddSpecialCharToFuncMap(string _str, Action _act)
        {
            specialCharFuncMap.Add(_str, _act);
        }
        #endregion

        #region User Function
        public void Next()
        {
            IsWaitingForNextToGo = false;
        }
        public void SetText(string _text)
        {
            StartCoroutine(SetTextTask(_text));
        }
        #endregion

        #region Keywords Function
        private IEnumerator CmdFun_l_Task()
        {
            IsOnCmdEvent = true;
            IsWaitingForNextToGo = true;
            yield return new WaitUntil(() => IsWaitingForNextToGo == false);
            IsOnCmdEvent = false;
            yield return null;
        }
        private IEnumerator CmdFun_r_Task()
        {
            IsOnCmdEvent = true;
            msgText += '\n';
            IsOnCmdEvent = false;
            yield return null;
        }
        private IEnumerator CmdFun_w_Task()
        {
            IsOnCmdEvent = true;
            IsWaitingForNextToGo = true;
            yield return new WaitUntil(() => IsWaitingForNextToGo == false);
            msgText = "";   //Erase the messages.
            IsOnCmdEvent = false;
            yield return null;
        }
        private IEnumerator CmdFun_lr_Task()
        {
            IsOnCmdEvent = true;
            IsWaitingForNextToGo = true;
            yield return new WaitUntil(() => IsWaitingForNextToGo == false);
            msgText += '\n';
            IsOnCmdEvent = false;
            yield return null;
        }
        #endregion

        #region Messages Core
        private void AddChar(char _char)
        {
            msgText += _char;
            lastChar = _char;
        }
        private SpecialCharType CheckSpecialChar(char _char)
        {
            if (_char == SPECIAL_CHAR_STAR)
            {
                if (lastChar == SPECIAL_CHAR_STAR)
                {
                    specialCmd = "";
                    IsOnSpecialChar = false;
                    return SpecialCharType.NormalChar;
                }
                IsOnSpecialChar = true;
                return SpecialCharType.CmdChar;
            }
            else if (_char == SPECIAL_CHAR_END && IsOnSpecialChar)
            {
                //exe cmd!
                if (specialCharFuncMap.ContainsKey(specialCmd))
                {
                    specialCharFuncMap[specialCmd]();
                    //Debug.Log("The keyword : [" + specialCmd + "] execute!");
                }
                else
                    Debug.LogError("The keyword : [" + specialCmd + "] is not exist!");
                specialCmd = "";
                IsOnSpecialChar = false;
                return SpecialCharType.EndChar;
            }
            else if (IsOnSpecialChar)
            {
                specialCmd += _char;
                return SpecialCharType.CmdChar;
            }
            return SpecialCharType.NormalChar;
        }
        private IEnumerator SetTextTask(string _text)
        {
            IsOnSpecialChar = false;
            IsMsgCompleted = false;
            specialCmd = "";
            for (int i = 0; i < _text.Length; i++)
            {
                switch (CheckSpecialChar(_text[i]))
                {
                    case SpecialCharType.NormalChar:
                        AddChar(_text[i]);
                        lastChar = _text[i];
                        yield return new WaitForSeconds(textSpeed);
                        break;
                }
                lastChar = _text[i];
                yield return new WaitUntil(() => IsOnCmdEvent == false);
            }
            IsMsgCompleted = true;
            yield return null;
        }
        #endregion
    }
}

