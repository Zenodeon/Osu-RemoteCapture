using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonPlus : Button
{
    [Serializable]
    public class ButtonAction : UnityEvent<ButtonState> { };

    public ButtonAction OnButtonAction = new ButtonAction();

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        OnButtonAction.Invoke((ButtonState)state);
    }   
}

public enum ButtonState
{
    Normal,
    Pressed,
    Selected,
    Highlighted,
    Disabled
}

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonPlus))]
public class ButtonPlusEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ButtonPlus mainClass = (ButtonPlus)target;

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("OnButtonAction"));
        serializedObject.ApplyModifiedProperties();
    }
}
#endif