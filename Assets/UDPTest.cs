using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void send(string sData = null)
    {
        Debug.Log("Sending UDP Data : " + sData);
        try
        {
            var endPoint = new IPEndPoint(IPAddress.Parse("192.168.100.100"), 64);
            var client = new UdpClient();

            string data = sData == null ? "bruh" : sData;

            client.Connect(endPoint);

            client.Send(Encoding.ASCII.GetBytes(data), data.Length);

            client.Close();
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UDPTest))]
public class UDPTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UDPTest mainClass = (UDPTest)target;

        if(GUILayout.Button("Send UDP"))
        {
            mainClass.send();
        }
    }
}
#endif