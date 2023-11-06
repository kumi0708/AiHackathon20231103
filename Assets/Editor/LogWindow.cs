using System;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Collections;

/// <summary>
/// ���s���ꂽ���O��\������E�B���h�E
/// </summary>
public class LogWindow : EditorWindow
{

    private StringBuilder _stringBuilder;

    //=================================================================================
    //������
    //=================================================================================

    //���j���[����E�B���h�E��\��
    [MenuItem("Tools/Open/LogWindow")]
    public static void Open()
    {
        LogWindow.GetWindow(typeof(LogWindow));
    }

    //�E�B���h�E���J�������Ɏ��s�����(�\������p�[�c�̐ݒ�p)
    private void OnEnable()
    {

        Application.logMessageReceived += OnReceiveLog;

        _stringBuilder = new StringBuilder();
        _stringBuilder.Append($"���O�ꗗ");
    }

    //=================================================================================
    //���O�擾
    //=================================================================================

    //���O���󂯎����
    private void OnReceiveLog(string logText, string stackTrace, LogType logType)
    {
        _stringBuilder.Append($"\n================\nlogText\n{logText}\n\nLogType\n{logType}\n\nstackTrace\n{stackTrace}");
    }

    //=================================================================================
    //�\������GUI�̐ݒ�
    //=================================================================================

    private void OnGUI()
    {
        if (GUILayout.Button("���O�ǉ��{�^��"))
        {
            Debug.Log("���ʂ̃��O");
            Debug.LogWarning("�x��");
            Debug.LogError("�G���[");
            Debug.Assert(false, "�A�T�[�g");

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
