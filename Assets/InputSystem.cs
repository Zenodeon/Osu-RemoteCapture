using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSystem : MonoBehaviour
{
    public UDPTest udp;
    public TestS ss;

    Dictionary<int, TouchPhase> touchPoint = new Dictionary<int, TouchPhase>();

    private void Start()
    {
        
    }

    void Update()
    {
        Touch[] touches = Input.touches;

        foreach (Touch touch in touches)
        {
            int id = touch.fingerId;
            TouchPhase phase = touch.phase;

            if (touchPoint.ContainsKey(id))
            {
                if (phase == TouchPhase.Ended)
                {
                    touchPoint.Remove(id);
                    test(id, phase);
                }
                else if (touchPoint[id] != phase)
                    test(id, phase);
            }
            else
            {
                touchPoint.Add(id, phase);
                test(id, phase);
            }
        }
    }

    void test(int id, TouchPhase phase)
    {
        if (phase == TouchPhase.Stationary)
            return;

        udp.send(id + " :: " + phase.ToString());
    }
}
