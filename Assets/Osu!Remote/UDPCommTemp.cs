using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPCommTemp
{
    private IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.100.100"), 64);
    private UdpClient client = new UdpClient();

    public void Connect(bool connect = true)
    {
        try
        {
            if (connect)
            {
                client.Connect(endPoint);
                send("Connected");
            }
            else
            {
                send("Disconnecting");
                client.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void send(string sData = null)
    {
        Debug.Log("Sending UDP Data : " + sData);

        try
        {
            string data = sData == null ? "bruh" : sData;

            client.Send(Encoding.ASCII.GetBytes(data), data.Length);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}