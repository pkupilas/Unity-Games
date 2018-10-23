using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HardShellStudios.CompleteControl;

public static class hInput {

    public static KeyCode RebindRemovalKey { get { return hManager.Active().rebindRemoveKey; } }

    public static bool GetButton(string buttonName)
    {
        return hManager.Active().GetButton(buttonName);
    }

    public static bool GetButtonDown(string buttonName)
    {
        return hManager.Active().GetButtonDown(buttonName);
    }

    public static bool GetButtonUp(string buttonName)
    {
        return hManager.Active().GetButtonUp(buttonName);
    }

    public static float GetAxis(string buttonName)
    {
        return hManager.Active().GetAxis(buttonName);
    }

    public static void SetKey(string uniqueKeyName, KeyCode keyCode, KeyTarget keyTarget = KeyTarget.PositivePrimary)
    {
        hManager.Active().SetKey(uniqueKeyName, keyCode, keyTarget);
    }

    public static void SetKey(string uniqueKeyName, MouseAxis mouseAxis)
    {
        hManager.Active().SetKey(uniqueKeyName, mouseAxis);
    }

    public static void SetKey(string uniqueKeyName, AxisCode joystickAxis, TargetController targetController = TargetController.All)
    {
        hManager.Active().SetKey(uniqueKeyName, joystickAxis, targetController);
    }

    public static void SetKeySensitivity(string uniqueKeyName, float sensitivity)
    {
        hManager.Active().SetKeySensitivity(uniqueKeyName, sensitivity);
    }

    public static KeyCode CurrentKeyDown()
    {
        return hManager.Active().CurrentKeyDown();
    }

    public static KeyCode DetailsFromKey(string uniqueKeyName, KeyTarget keyTarget)
    {
        return hManager.Active().DefailsFromKey(uniqueKeyName, keyTarget);
    }
}
