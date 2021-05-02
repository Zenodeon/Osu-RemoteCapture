using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchState 
{
    public int touchIndex { get; private set; }

    public TouchPhase phase { get; private set; }

    public TouchState(Touch touch)
    {
        touchIndex = touch.fingerId;

        phase = touch.phase;
    }
}
