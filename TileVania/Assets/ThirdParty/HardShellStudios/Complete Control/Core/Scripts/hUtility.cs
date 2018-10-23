using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

namespace HardShellStudios.CompleteControl
{

    public static class hUtility {

        public static string SaveName = "KeyBindings.xml";
        public const string DefaultName = "KeyBindings";

        public static hScheme GetDefaultScheme()
        {
            try
            {
                return (hScheme)Resources.Load(DefaultName);
            }
            catch
            {
                Debug.LogError("No '" + DefaultName + "' found inside a Resources folder.");
            }

            return null;
        }

        public static string GetSavePath()
        {
            return Application.persistentDataPath + "/" + SaveName;
        }

        public static void SaveBinings(hInputDetails[] inputs)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(GetSavePath(), settings);
            writer.WriteStartElement("KeyBindings");
            foreach (hInputDetails key in inputs)
            {
                writer.WriteStartElement("Input");
                writer.WriteAttributeString("Name", key.Name);
                writer.WriteAttributeString("UniqueName", key.UniqueName);
                writer.WriteAttributeString("Type", ((int)key.Type).ToString());

                writer.WriteAttributeString("PositivePrimary", ((int)key.Positive.Primary).ToString());
                writer.WriteAttributeString("PositiveSecondary", ((int)key.Positive.Secondary).ToString());
                writer.WriteAttributeString("NegativePrimary", ((int)key.Negative.Primary).ToString());
                writer.WriteAttributeString("NegativeSecondary", ((int)key.Negative.Secondary).ToString());

                writer.WriteAttributeString("TargetController", ((int)key.targetController).ToString());
                writer.WriteAttributeString("Axis", ((int)key.Axis).ToString());
                writer.WriteAttributeString("Invert", key.Invert.ToString());
                writer.WriteAttributeString("Sensitivity", key.Sensitivity.ToString());
                writer.WriteEndElement();
            }

            writer.WriteFullEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public static int GetUniqueIndex(string uniqueKeyName, hInputDetails[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
                if (inputs[i].UniqueName.Equals(uniqueKeyName))
                    return i;

            return -1;
        }

        public static hInputDetails[] LoadBindings(ref hInputDetails[] details)
        {
            try
            {
                hInputDetails[] keys = details;

                FileInfo file = new FileInfo(GetSavePath());
                if (file.Exists)
                {
                    XDocument document = XDocument.Load(GetSavePath());
                    var Inputs = document.Descendants("Input");
                    int i = 0;
                    foreach (var Input in Inputs)
                    {
                        if (Input.Attribute("Name").Value == details[i].Name)
                        {
                            keys[i].Name = details[i].Name;
                            keys[i].UniqueName = details[i].UniqueName;
                            keys[i].Type = (KeyType)int.Parse(Input.Attribute("Type").Value);

                            keys[i].Positive.Primary = (KeyCode)int.Parse(Input.Attribute("PositivePrimary").Value);
                            keys[i].Positive.Secondary = (KeyCode)int.Parse(Input.Attribute("PositiveSecondary").Value);
                            keys[i].Negative.Primary = (KeyCode)int.Parse(Input.Attribute("NegativePrimary").Value);
                            keys[i].Negative.Secondary = (KeyCode)int.Parse(Input.Attribute("NegativeSecondary").Value);

                            keys[i].targetController = (TargetController)int.Parse(Input.Attribute("TargetController").Value);
                            keys[i].Axis = (AxisCode)int.Parse(Input.Attribute("Axis").Value);
                            keys[i].Invert = Input.Attribute("Axis").Value == "True" ? true : false;
                            keys[i].Sensitivity = float.Parse(Input.Attribute("Sensitivity").Value);
                        }

                        i++;
                    }

                    return keys;
                }
                else
                    Debug.LogWarning("No Saved Bindings Found");
            }
            catch
            {
                Debug.LogWarning("Bindings Error");
            }
            
            return null;
        }

    }

}