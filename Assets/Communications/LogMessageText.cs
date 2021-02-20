using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 必須コンポーネントを指定
[RequireComponent(typeof(Text))]
public class LogMessageText : MonoBehaviour
{
    /// デバッグログ用テキスト
    /// </summary>
    private Text p_Text;

    /// <summary>
    /// 表示行数
    /// </summary>
    [SerializeField, Tooltip("表示行数")]
    private int p_LineNum = 30;

    private string[] linecodes = new string[] { "\n", "\r", "\r\n" };

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Awake()
    {
        // Logメッセージイベント追加
        Application.logMessageReceived += LogMessageOutput;

        p_Text = this.GetComponent<Text>();
    }

    /// <summary>
    /// Logメッセージイベント処理
    /// </summary>
    private void LogMessageOutput(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
                // ログメッセージとスタックトレースを表示
                ShowMessage(p_Text.text, condition, stackTrace);
                break;
            case LogType.Assert:
                // ログメッセージとスタックトレースを表示
                ShowMessage(p_Text.text, condition, stackTrace);
                break;
            case LogType.Warning:
                // ログメッセージのみ表示
                ShowMessage(p_Text.text, condition, "");
                break;
            case LogType.Log:
                // ログメッセージを表示
                ShowMessage(p_Text.text, condition, "");
                break;
            case LogType.Exception:
                break;
        }
    }

    /// <summary>
    /// 指定行数でのメッセージ表示処理
    /// </summary>
    private void ShowMessage(string basetext, string message, string stacktrace)
    {
        string[] baselines = basetext.Split(linecodes, System.StringSplitOptions.RemoveEmptyEntries);
        string[] messagelines = message.Split(linecodes, System.StringSplitOptions.RemoveEmptyEntries);
        string[] tracelines = stacktrace.Split(linecodes, System.StringSplitOptions.RemoveEmptyEntries);

        List<string> lines = new List<string>();
        lines.AddRange(baselines);
        lines.AddRange(messagelines);
        foreach(string trace in tracelines)
        {
            lines.Add(" " + trace);
        }

        int linecount = 0;
        string textmessage = "";
        if (lines.Count > p_LineNum)
        {
            linecount = lines.Count - p_LineNum;
        }
        for (int num = linecount; num < lines.Count; num++)
        {
            if (lines[num].Length > 0)
            {
                textmessage += lines[num] + linecodes[0];
            }
        }
        
        p_Text.text = textmessage;
    }
}