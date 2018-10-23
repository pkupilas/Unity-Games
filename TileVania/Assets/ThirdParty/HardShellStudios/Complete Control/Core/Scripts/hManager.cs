using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyTarget {
    PositivePrimary,
    PositiveSecondary,
    NegativePrimary,
    NegativeSecondary
}

namespace HardShellStudios.CompleteControl
{
    public class hManager
    {
        // Static variables
        static hManager main;

        // Runtime variables
        hInputDetails[] inputs;
        bool resetInEditor;
        public KeyCode rebindRemoveKey;
        float[,] inputAxis;

        #region Time difference handler
        int lastFrame = 0;
        float lastTime = 0;
        float difference = 0;
        float timeDifference
        {
            get
            {
                if (lastFrame != Time.frameCount)
                {
                    difference = Time.time - lastTime;
                    lastTime = Time.time;
                    lastFrame = Time.frameCount;
                    return difference;
                }
                else
                {
                    return difference;
                }

            }
            set
            {
                lastTime = value;
            }
        }
        #endregion

        public static hManager Active()
        {
            if (main == null)
                main = new hManager();

            return main;
        }

        public hManager()
        {
            LoadDefaultScheme();
            if(!resetInEditor || !Application.isEditor)
                LoadPersonal();
            hUtility.SaveBinings(inputs);
        }

        public void LoadDefaultScheme()
        {
            hScheme defaultScheme = hUtility.GetDefaultScheme();
            resetInEditor = defaultScheme.forceResetInEditor;
            rebindRemoveKey = defaultScheme.rebindRemoveKey;
            inputs = new hInputDetails[defaultScheme.inputs.Length];
            for (int i = 0; i < defaultScheme.inputs.Length; i++)
            {
                inputs[i] = defaultScheme.inputs[i];
            }
            inputAxis = new float[inputs.Length, 3];
            
        }

        public void LoadPersonal()
        {
            hInputDetails[] loaded = hUtility.LoadBindings(ref inputs);
            if (loaded == null)
                hUtility.SaveBinings(inputs);
            else
                inputs = loaded;
        }

        #region Get Key Etc is in here.

        public bool GetButton(string keyName)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Name == keyName)
                    if (inputs[i].Type == KeyType.KeyPress)
                        if (Input.GetKey(inputs[i].Positive.Primary) || Input.GetKey(inputs[i].Positive.Secondary) ||
                            Input.GetKey(inputs[i].Negative.Primary) || Input.GetKey(inputs[i].Negative.Secondary))
                            return true;
            }
            return false;
        }

        public bool GetButtonDown(string keyName)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Name == keyName)
                    if (inputs[i].Type == KeyType.KeyPress)
                        if (Input.GetKeyDown(inputs[i].Positive.Primary) || Input.GetKeyDown(inputs[i].Positive.Secondary) ||
                            Input.GetKeyDown(inputs[i].Negative.Primary) || Input.GetKeyDown(inputs[i].Negative.Secondary))
                            return true;
            }
            return false;
        }

        public bool GetButtonUp(string keyName)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Name == keyName)
                    if (inputs[i].Type == KeyType.KeyPress)
                        if (Input.GetKeyUp(inputs[i].Positive.Primary) || Input.GetKeyUp(inputs[i].Positive.Secondary) ||
                            Input.GetKeyUp(inputs[i].Negative.Primary) || Input.GetKeyUp(inputs[i].Negative.Secondary))
                            return true;
            }
            return false;
        }

        float CompareAxis(float first, float second)
        {
            if (first > 0)
            {
                if (second > first)
                    return second;
                else
                    return first;
            }
            else if (first < 0)
            {
                if (second < first)
                    return second;
                else
                    return first;
            }
            else
                return second;
        }

        public float GetAxis(string keyName)
        {
            float biggest = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Name == keyName)
                {
                    if (inputs[i].Type == KeyType.MouseAxis)
                    {
                        biggest = CompareAxis(biggest, Input.GetAxis(string.Format("Mouse Axis-{0}", inputs[i].Axis.ToString())));
                    }
                    else if (inputs[i].Type == KeyType.ControllerAxis)
                    {
                        biggest = CompareAxis(biggest, Input.GetAxis(string.Format("Controller Axis-{0}-{1}", inputs[i].targetController.ToString(), inputs[i].Axis.ToString())));
                    }
                    else if (inputs[i].Type == KeyType.KeyPress)
                    {
                        biggest = CompareAxis(biggest, GetAxisFromKey(inputs[i], i));
                        /*
                        float dir = 0;
                        if (Input.GetKey(inputs[i].Positive.Primary) || Input.GetKey(inputs[i].Positive.Secondary))
                            dir += 1;
                        if (Input.GetKey(inputs[i].Negative.Primary) || Input.GetKey(inputs[i].Negative.Secondary))
                            dir -= 1;

                        inputs[i].val = Mathf.MoveTowards(inputs[i].val, dir, inputs[i].Sensitivity);
                        biggest = inputs[i].val;*/

                    }

                    if (inputs[i].Invert && biggest != 0)
                        biggest *= -1;
                }
                        
            }

            return biggest;
        }

        float GetAxisFromKey(hInputDetails details, int i)
        {
            if (Time.frameCount > inputAxis[i, 0])
            {
                inputAxis[i, 1] = inputAxis[i, 2];
                float dir = 0;
                if (Input.GetKey(inputs[i].Positive.Primary) || Input.GetKey(inputs[i].Positive.Secondary))
                    dir += 1;
                if (Input.GetKey(inputs[i].Negative.Primary) || Input.GetKey(inputs[i].Negative.Secondary))
                    dir -= 1;

                inputAxis[i, 2] = Mathf.Clamp(Mathf.MoveTowards(inputAxis[i, 2], dir, inputs[i].Sensitivity * timeDifference), -1, 1);
            }
            else
                return inputAxis[i, 2];

            inputAxis[i, 0] = lastFrame;
            return inputAxis[i, 2];
        }

        #endregion

        #region Key Management Here

        int GetUniqueIndex(string uniqueKeyName)
        {
            for (int i = 0; i < inputs.Length; i++)
                if (inputs[i].UniqueName == uniqueKeyName)
                    return i;

            Debug.LogError("Unique key '" + uniqueKeyName + "' not found.");
                
            return 0;
        }

        public void SetKey(string uniqueKeyName, KeyCode keyCode, KeyTarget keyTarget = KeyTarget.PositivePrimary)
        {
            int i = GetUniqueIndex(uniqueKeyName);
            inputs[i].Type = KeyType.KeyPress;

            switch (keyTarget)
            {
                case KeyTarget.PositivePrimary:
                    inputs[i].Positive.Primary = keyCode;
                    break;
                case KeyTarget.PositiveSecondary:
                    inputs[i].Positive.Secondary = keyCode;
                    break;
                case KeyTarget.NegativePrimary:
                    inputs[i].Negative.Primary = keyCode;
                    break;
                case KeyTarget.NegativeSecondary:
                    inputs[i].Negative.Secondary = keyCode;
                    break;
            }

            hUtility.SaveBinings(inputs);
        }

        public void SetKey(string uniqueKeyName, MouseAxis mouseAxis)
        {
            int i = GetUniqueIndex(uniqueKeyName);
            inputs[i].Type = KeyType.MouseAxis;
            inputs[i].Axis = (AxisCode)mouseAxis;

            hUtility.SaveBinings(inputs);
        }

        public void SetKey(string uniqueKeyName, AxisCode joystickAxis, TargetController targetController = TargetController.All)
        {
            int i = GetUniqueIndex(uniqueKeyName);
            inputs[i].Type = KeyType.ControllerAxis;
            inputs[i].Axis = joystickAxis;
            inputs[i].targetController = targetController;

            hUtility.SaveBinings(inputs);
        }

        public void SetKeySensitivity(string uniqueKeyName, float Sensitivity)
        {
            int i = GetUniqueIndex(uniqueKeyName);
            inputs[i].Sensitivity = Sensitivity;

            hUtility.SaveBinings(inputs);
        }

        public void ResetKey(string uniqueKeyName)
        {
            hScheme defaultScheme = hUtility.GetDefaultScheme();
            int i = GetUniqueIndex(uniqueKeyName);
            inputs[i] = defaultScheme.inputs[i];

            hUtility.SaveBinings(inputs);
        }

        public void ResetAllKeys()
        {
            LoadDefaultScheme();

            hUtility.SaveBinings(inputs);
        }

        public KeyCode CurrentKeyDown()
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
                if (Input.GetKeyDown(kcode))
                    return kcode;

            return KeyCode.None;
        }

        public KeyCode DefailsFromKey(string uniqueKeyCode, KeyTarget keyTarget)
        {
            int i = GetUniqueIndex(uniqueKeyCode);
            switch (keyTarget)
            {
                default:
                    return inputs[i].Positive.Primary;
                case KeyTarget.PositiveSecondary:
                    return inputs[i].Positive.Secondary;
                case KeyTarget.NegativePrimary:
                    return inputs[i].Negative.Primary;
                case KeyTarget.NegativeSecondary:
                    return inputs[i].Negative.Secondary;
            }
        }

        #endregion
    }

}
