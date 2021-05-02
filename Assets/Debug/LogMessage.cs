using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogMessage : MonoBehaviour
{
    public TextMeshProUGUI logD;
    public Text indexD;
    public Text countD;


    private int index;
    private int logCount = 1;

    private string logMessage;

    public void Details(int index, string log, Color color)
    {
        this.index = index;
        logMessage = log;

        indexD.text = index + "";
        logD.text = log;
        countD.text = logCount + "";

        indexD.color = logD.color = countD.color = color;
    }

    public void ReLog()
    {
        countD.text = logCount++ + "";
    }
}
