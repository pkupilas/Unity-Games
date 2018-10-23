using System;
using UnityEngine;

namespace HardShellStudios.CompleteControl
{

    [Serializable]
    public struct hInputDetails
    {
        public string Name;
        public string UniqueName;
        public KeyType Type;
        public hInputBundle Positive;
        public hInputBundle Negative;
        public TargetController targetController;
        public AxisCode Axis;
        public bool Invert;
        public float Sensitivity;

        public float val;
    }

    [Serializable]
    public struct hInputBundle {
        public KeyCode Primary;
        public KeyCode Secondary;
    }

    public enum KeyType
    {
        KeyPress,
        MouseAxis,
        ControllerAxis
    }

    public enum TargetController {
        All,
        Joystick1,
        Joystick2,
        Joystick3,
        Joystick4,
        Joystick5,
        Joystick6,
        Joystick7,
        Joystick8
        /*Joystick9
        Joystick10,
        Joystick11,
        Joystick12,
        Joystick13,
        Joystick14,
        Joystick15,
        Joystick16*/
    }

    public enum MouseAxis
    {
        X = 0,
        Y,
        ScrollWheel
    }

    public enum AxisCode
    {
        Axis1 = 0,
        Axis2,
        Axis3,
        Axis4,
        Axis5,
        Axis6,
        Axis7,
        Axis8,
        Axis9,
        Axis10,
        Axis11,
        Axis12,
        Axis13,
        Axis14,
        Axis15,
        Axis16,
        Axis17,
        Axis18,
        Axis19,
        Axis20,
        Axis21,
        Axis22,
        Axis23,
        Axis24,
        Axis25,
        Axis26,
        Axis27,
        Axis28
    }

}