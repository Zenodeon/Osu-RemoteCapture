using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class InputSystem : MonoBehaviour
{
    private UDPCommTemp udp = new UDPCommTemp();

    Dictionary<int, TouchPhase> touchPoint = new Dictionary<int, TouchPhase>();

    private void Start()
    {
        udp.Connect();
    }

    private void OnApplicationQuit()
    {
        udp.Connect(false);
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
                    SendTouchState(touch);
                }
                else if (touchPoint[id] != phase)
                    SendTouchState(touch);
            }
            else
            {
                touchPoint.Add(id, phase);
                SendTouchState(touch);
            }
        }
    }

    void SendTouchState(Touch touch)
    {
        TouchState touchState = new TouchState(touch);

        if (touchState.phase == TouchPhase.Stationary | touchState.phase == TouchPhase.Moved)
            return;

        if (touchState.touchIndex <= 1)
            if (touchState.phase == TouchPhase.Began)
                Haptic(touchState.touchIndex);
                //Handheld.Vibrate();

        udp.send(JsonConvert.SerializeObject(touchState));
    }

    void Haptic(int a)
    {
        /*
        HapticFeedbackTypes haptic = HapticFeedbackTypes.MEDIUM;

        if (a == 0)
            haptic = HapticFeedbackTypes.LIGHT;
        else if (a == 1)
            haptic = HapticFeedbackTypes.HEAVY;

        IOSNative.StartHapticFeedback(haptic);
        */

        if (a == 0)
            HapticFeedback.ImpactOccurred(ImpactFeedbackStyle.Rigid);

        if (a == 1)
            HapticFeedback.ImpactOccurred(ImpactFeedbackStyle.Soft);
    }
}
