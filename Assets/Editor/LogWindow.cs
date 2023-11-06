using System;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Collections;

/// <summary>
/// 発行されたログを表示するウィンドウ
/// </summary>
public class LogWindow : EditorWindow
{

    private StringBuilder _stringBuilder;

    //=================================================================================
    //初期化
    //=================================================================================

    //メニューからウィンドウを表示
    [MenuItem("Tools/Open/LogWindow")]
    public static void Open()
    {
        LogWindow.GetWindow(typeof(LogWindow));
    }

    //ウィンドウを開いた時に実行される(表示するパーツの設定用)
    private void OnEnable()
    {

        Application.logMessageReceived += OnReceiveLog;

        _stringBuilder = new StringBuilder();
        _stringBuilder.Append($"ログ一覧");
    }

    //=================================================================================
    //ログ取得
    //=================================================================================

    //ログを受け取った
    private void OnReceiveLog(string logText, string stackTrace, LogType logType)
    {
        _stringBuilder.Append($"\n================\nlogText\n{logText}\n\nLogType\n{logType}\n\nstackTrace\n{stackTrace}");
    }

    //=================================================================================
    //表示するGUIの設定
    //=================================================================================

    private void OnGUI()
    {
        if (GUILayout.Button("ログ追加ボタン"))
        {
            Debug.Log("普通のログ");
            Debug.LogWarning("警告");
            Debug.LogError("エラー");
            Debug.Assert(false, "アサート");

            try
            {
                int a = 1, b = 0;
                var c = a / b;
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }

        EditorGUILayout.TextArea(_stringBuilder.ToString());
    }

}
