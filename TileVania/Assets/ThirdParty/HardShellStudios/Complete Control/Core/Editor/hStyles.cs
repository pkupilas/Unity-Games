using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HardShellStudios.CompleteControl
{

    public static class hStyles
    {

        public static GUIStyle TitleBox()
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.padding = new RectOffset(5, 5, 20, 5);
            style.alignment = TextAnchor.MiddleCenter;
            return style;
        }

        public static GUIStyle TitleText()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 25;
            style.fixedHeight = 30;
            return style;
        }

        public static GUIStyle TitleSubText()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 12;
            style.fixedHeight = 40;
            return style;
        }

        public static GUIStyle TitleCorner()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.LowerLeft;
            style.fontSize = 10;
            //style.fixedHeight = 20;
            style.margin.top = 30;
            style.normal.textColor = Color.red;
            return style;
        }

        public static GUIContent GetFadeText(hInputDetails input)
        {
            GUIContent content = new GUIContent();
            content.text = (input.Name == "" || input.Name == null) ? "Unnamed Input" : input.Name;
            return content;
        }

        public static GUIStyle GetFadeStyle(hInputDetails input)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = (input.Name == "" || input.Name == null) ? Color.gray : Color.white;
            style.fixedHeight = 20;
            style.padding.left = 10;
            style.alignment = TextAnchor.MiddleLeft;
            return style;
        }

        public static GUIStyle RemoveCross()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.red;
            style.fixedWidth = 20;
            style.fixedHeight = 20;
            return style;
        }
        
        public static GUIStyle MoveButton()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.fixedWidth = 20;
            style.fixedHeight = 20;
            return style;
        }

        public static GUIContent DuplicateButton()
        {
            GUIContent style = new GUIContent();
            style.image = (Texture)Resources.Load("editor-duplicate");
            style.tooltip = "Duplicate Input";
            return style;
        }

        public static GUIContent Up()
        {
            GUIContent style = new GUIContent();
            style.image = (Texture)Resources.Load("editor-up");
            style.tooltip = "Move Input Up";
            return style;
        }

        public static GUIContent Down()
        {
            GUIContent style = new GUIContent();
            style.image = (Texture)Resources.Load("editor-down");
            style.tooltip = "Move Input Down";
            return style;
        }

        public static GUIStyle RemoveInput()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.red;
            style.fixedWidth = 14;
            style.fixedHeight = 14;
            style.fontSize = 12;
            return style;
        }

        public static GUIStyle InputParent()
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.padding = new RectOffset(10, 10, 10, 10);
            return style;
        }

        public static GUIStyle DetailGroup()
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.padding = new RectOffset(10, 10, 10, 10);
            return style;
        }
    }

}
