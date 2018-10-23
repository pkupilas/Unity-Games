using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System.IO;

namespace HardShellStudios.CompleteControl
{
    [InitializeOnLoad]
    [CustomEditor(typeof(hScheme))]
    public class hSchemeEditor : Editor
    {
        hScheme myTarget;
        AnimBool[] showFields = null;

        bool tested = false;
        bool isGood = false;

        void OnEnable()
        {
            myTarget = (hScheme)target;
            InitFadeBoxes(myTarget);
            for (int i = 0; i < showFields.Length; i++)
            {
                showFields[i].valueChanged.AddListener(Repaint);
            }
        }

        private void OnDisable()
        {
            EditorUtility.SetDirty(myTarget);
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }

        private void OnDestroy()
        {
            EditorUtility.SetDirty(myTarget);
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }


        void InitFadeBoxes(hScheme myTarget)
        {
            if (showFields == null || showFields.Length == 0)
            {
                showFields = new AnimBool[0];
            }

            if (myTarget.inputs != null)
            {

                if (showFields.Length != myTarget.inputs.Length)
                {
                    AnimBool[] newFields = new AnimBool[myTarget.inputs.Length];
                    for (int i = 0; i < newFields.Length; i++)
                    {
                        if (i < showFields.Length)
                            newFields[i] = showFields[i];
                        else
                            newFields[i] = new AnimBool();
                        newFields[i].valueChanged.AddListener(Repaint);
                    }

                    showFields = newFields;
                }
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(hStyles.TitleBox());

            EditorGUILayout.LabelField("Complete Control", hStyles.TitleText());
            EditorGUILayout.LabelField("by Hard Shell Studios", hStyles.TitleSubText());
            if (GUILayout.Button("Developed by www.HaydnComley.com", hStyles.TitleCorner()))
            {
                System.Diagnostics.Process.Start("http://www.haydncomley.com");
            }

            EditorGUILayout.EndVertical();

            myTarget = (hScheme)target;
            InitFadeBoxes(myTarget);

            if (!TestUnitySettings())
            {
                EditorGUILayout.BeginVertical(hStyles.InputParent());

                EditorGUILayout.LabelField("Unity Bindings Need Update", hStyles.TitleSubText());
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                if (GUILayout.Button("Overwrite Current Unity Bindings"))
                {
                    ImportUnityBindings();
                }
                EditorGUILayout.EndVertical();
            }

            if (myTarget.inputs != null)
            {
                // Iterate through all inputs showing their details.
                for (int i = 0; i < myTarget.inputs.Length; i++)
                {
                    // Get the current input.
                    hInputDetails currentInput = myTarget.inputs[i];
                    EditorGUILayout.BeginVertical(hStyles.InputParent());

                    #region Input Title Bar
                    // Top Bar
                    EditorGUILayout.BeginHorizontal();

                    //      Button that allows expansion to reveal more details
                    if (GUILayout.Button(hStyles.GetFadeText(currentInput), hStyles.GetFadeStyle(currentInput)))
                    {
                        showFields[i].target = !showFields[i].value;
                    }

                    if (i != 0)
                        if (GUILayout.Button(hStyles.Up(), hStyles.MoveButton()))
                            SwitchInputDetails(i, i - 1);

                    if (i != myTarget.inputs.Length - 1)
                        if (GUILayout.Button(hStyles.Down(), hStyles.MoveButton()))
                            SwitchInputDetails(i, i + 1);

                    if (GUILayout.Button(hStyles.DuplicateButton(), hStyles.MoveButton()))
                    {
                        DuplicateInput(i);
                    }

                    //      Remove Button
                    if (GUILayout.Button("X", hStyles.RemoveCross()))
                    {
                        Remove(i);
                    }

                    EditorGUILayout.EndHorizontal();
                    // End of top bar

                    #endregion

                    // If opened it shows these details
                    if (i < showFields.Length && EditorGUILayout.BeginFadeGroup(showFields[i].faded))
                    {
                        EditorGUILayout.BeginVertical(hStyles.DetailGroup());

                        myTarget.inputs[i].Name = EditorGUILayout.TextField("Name", myTarget.inputs[i].Name);
                        myTarget.inputs[i].UniqueName = EditorGUILayout.TextField("Unique Name", myTarget.inputs[i].UniqueName);
                        myTarget.inputs[i].Type = (KeyType)EditorGUILayout.EnumPopup("Type", myTarget.inputs[i].Type);

                        #region KeyPress display in editor
                        if (myTarget.inputs[i].Type == KeyType.KeyPress)
                        {
                            EditorGUILayout.LabelField("+ Positive");

                            EditorGUILayout.BeginHorizontal();
                            myTarget.inputs[i].Positive.Primary = (KeyCode)EditorGUILayout.EnumPopup("Primary", myTarget.inputs[i].Positive.Primary);
                            if (GUILayout.Button("X", hStyles.RemoveInput()))
                            {
                                myTarget.inputs[i].Positive.Primary = KeyCode.None;
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            myTarget.inputs[i].Positive.Secondary = (KeyCode)EditorGUILayout.EnumPopup("Secondary", myTarget.inputs[i].Positive.Secondary);
                            if (GUILayout.Button("X", hStyles.RemoveInput()))
                            {
                                myTarget.inputs[i].Positive.Secondary = KeyCode.None;
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.LabelField("- Negative");
                            EditorGUILayout.BeginHorizontal();
                            myTarget.inputs[i].Negative.Primary = (KeyCode)EditorGUILayout.EnumPopup("Primary", myTarget.inputs[i].Negative.Primary);
                            if (GUILayout.Button("X", hStyles.RemoveInput()))
                            {
                                myTarget.inputs[i].Negative.Primary = KeyCode.None;
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            myTarget.inputs[i].Negative.Secondary = (KeyCode)EditorGUILayout.EnumPopup("Secondary", myTarget.inputs[i].Negative.Secondary);
                            if (GUILayout.Button("X", hStyles.RemoveInput()))
                            {
                                myTarget.inputs[i].Negative.Secondary = KeyCode.None;
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        #endregion
                        #region MouseAxis display in editor
                        else if (myTarget.inputs[i].Type == KeyType.MouseAxis)
                        {
                            // Display the 3 Axis for the mice and map it to the 28 availbale axis inputs.
                            MouseAxis tempAxis = (MouseAxis)(((int)myTarget.inputs[i].Axis) > 3 ? 0 : (int)myTarget.inputs[i].Axis);
                            myTarget.inputs[i].Axis = (AxisCode)EditorGUILayout.EnumPopup("Axis", tempAxis);
                        }
                        #endregion
                        #region ControllerAxis display in editor
                        else if (myTarget.inputs[i].Type == KeyType.ControllerAxis)
                        {
                            myTarget.inputs[i].targetController = (TargetController)EditorGUILayout.EnumPopup("Controller", myTarget.inputs[i].targetController);
                            myTarget.inputs[i].Axis = (AxisCode)EditorGUILayout.EnumPopup("Axis", myTarget.inputs[i].Axis);
                        }
                        #endregion

                        myTarget.inputs[i].Invert = EditorGUILayout.Toggle("Invert", myTarget.inputs[i].Invert);
                        myTarget.inputs[i].Sensitivity = EditorGUILayout.FloatField("Sensitivity", myTarget.inputs[i].Sensitivity);

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndFadeGroup();
                    }
                    // End of details.

                    EditorGUILayout.EndVertical();
                }

            }

            EditorGUILayout.Space();

            #region Controls Panel
            EditorGUILayout.BeginVertical(hStyles.InputParent());

            EditorGUILayout.LabelField("Control Panel", hStyles.TitleSubText());
            // Totally efficient way of making a gap.
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // Add An Input
            if (GUILayout.Button("Add Input"))
                myTarget.inputs = AddInput(myTarget.inputs);

            // Rebind Remove Key.
            myTarget.rebindRemoveKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Rebind Remove", 
                "When trying to rebind keys at runtime, this specific one will act as none. E.g. Normally 'Escape' or 'Delete'"), 
                myTarget.rebindRemoveKey);

            // Force reset of bindings.
            myTarget.forceResetInEditor = EditorGUILayout.Toggle("Reset Editor Bindings on play.", myTarget.forceResetInEditor);

            if (GUILayout.Button("Goto Keybindings Save File"))
            {
                System.Diagnostics.Process.Start(hUtility.GetSavePath().Replace(hUtility.SaveName, ""));
            }

            if (GUILayout.Button("Import"))
            {
                System.Diagnostics.Process.Start(hUtility.GetSavePath().Replace(hUtility.SaveName, ""));
            }

            EditorGUILayout.EndVertical();
            #endregion
        }

        bool TestUnitySettings()
        {
            FileInfo currentAsset = new FileInfo(Application.dataPath.Replace("Assets", "ProjectSettings/InputManager.asset"));
            FileInfo assetFixFile = new FileInfo(PathToAssetFix());
            return currentAsset.Length.Equals(assetFixFile.Length);
        }

        void ImportUnityBindings()
        {
            FileInfo currentAsset = new FileInfo(Application.dataPath.Replace("Assets", "ProjectSettings/InputManager.asset"));
            FileInfo assetFixFile = new FileInfo(PathToAssetFix());

            currentAsset.Delete();
            assetFixFile.CopyTo(Application.dataPath.Replace("Assets", "ProjectSettings/InputManager.asset"));
            Debug.Log("Default Unity Inputs should have been overwritten correctly :)");
        }

        static string PathToAssetFix()
        {
            string[] results = AssetDatabase.FindAssets("InputManagerAsset");
            if (results.Length != 0)
            {
                return AssetDatabase.GUIDToAssetPath(results[0]);
            }
            return null;
        }

        void Remove(int index)
        {
            myTarget.inputs = RemoveInput(myTarget.inputs, index);

            AnimBool[] newFields = new AnimBool[myTarget.inputs.Length];
            for (int n = 0; n < newFields.Length; n++)
            {
                int id = n + (n >= index ? 1 : 0);
                newFields[n] = showFields[id];
                newFields[n].valueChanged.AddListener(Repaint);
            }
            showFields = newFields;
            InitFadeBoxes(myTarget);
        }

        void SwitchInputDetails(int moveThis, int toThis)
        {
            hInputDetails holder = myTarget.inputs[toThis];
            myTarget.inputs[toThis] = myTarget.inputs[moveThis];
            myTarget.inputs[moveThis] = holder;
        }

        void DuplicateInput(int index)
        {
            myTarget.inputs = AddInput(myTarget.inputs);
            hInputDetails[] inputs = new hInputDetails[myTarget.inputs.Length];
            int count = 0;
            for (int i = 0; i < myTarget.inputs.Length; i++)
            {
                inputs[i] = myTarget.inputs[count];

                if (i != index)
                {
                    count++;
                }
            }

            myTarget.inputs = inputs;
            InitFadeBoxes(myTarget);
        }

        hInputDetails[] AddInput(hInputDetails[] inputArray)
        {
            if (myTarget.inputs == null)
            {
                myTarget.inputs = new hInputDetails[0];
                inputArray = myTarget.inputs;
            }
            

            hInputDetails[] newInputs = new hInputDetails[inputArray.Length + 1];
            for (int i = 0; i < inputArray.Length; i++)
                newInputs[i] = inputArray[i];

            newInputs[inputArray.Length].Sensitivity = 1f;

            return newInputs;
        }

        hInputDetails[] RemoveInput(hInputDetails[] inputArray, int index)
        {
            hInputDetails[] newInputs = new hInputDetails[inputArray.Length - 1];
            int count = 0;
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (i != index)
                {
                    newInputs[count] = inputArray[i];
                    count++;
                }
                
            }

            return newInputs;
        }
    }

}