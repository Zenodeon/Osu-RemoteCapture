using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestS : MonoBehaviour
{
    public Text text;

    private int count = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void click()
    {
        text.text = count++ + "";
    }

    public void Action(ButtonState state)
    {
        if (state == ButtonState.Selected)
            text.text = count++ + "";
    }
}
