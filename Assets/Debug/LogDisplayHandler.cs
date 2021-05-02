using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LogDisplayHandler : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject logMessage;

    private Dictionary<string, LogMessage> logs = new Dictionary<string, LogMessage>();

    private int logCount = 0;

    private void Start()
    {
        Application.logMessageReceived += DisplayLog;

        Debug.Log("~~Test Log : Started~~");
        Debug.LogWarning("~~Test Log Type : Warning~~");
        Debug.LogError("~~Test Log Type : Error~~");
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= DisplayLog;
    }


    public void DisplayLog(string logString, string stackTrace, LogType type)
    {
        Color color;

        switch (type)
        {
            case LogType.Log:
                color = Color.white;

                break;
            case LogType.Warning:
                color = Color.yellow;

                break;
            case LogType.Error:
                color = Color.red;

                break;

            default:
                color = Color.blue;

                break;
        }

        if (logs.ContainsKey(logString))
            logs[logString].ReLog();
        else
        {
            LogMessage newLog = Instantiate(logMessage, content.transform).GetComponent<LogMessage>();

            RectTransform logTransform = newLog.GetComponent<RectTransform>();

            float height = logTransform.rect.height;

            logTransform.localPosition = new Vector3(logTransform.localPosition.x, -(height * logCount), logTransform.localPosition.z);

            newLog.Details(logCount, logString, color);

            logCount++;

            content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, height * logCount);

            logs.Add(logString, newLog);
        }

    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LogDisplayHandler))]
public class LogDisplayHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LogDisplayHandler mainClass = (LogDisplayHandler)target;

        if (GUILayout.Button("New Log Message"))
        {
            mainClass.DisplayLog("Test Log", null, LogType.Log);
        }
    }
}

#endif
