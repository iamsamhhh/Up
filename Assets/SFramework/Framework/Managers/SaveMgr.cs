using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace SFramework{
    public class SaveMgr : SingletonBase<SaveMgr>
    {
        private const string valueTypeStr = "ValueType";
        private string filePath {
            get {return Application.persistentDataPath + "/";}
        }
        const string FILE_TYPE = ".sfdat";
        public void Save(float value, string name)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("Root");

            XmlElement valueType = xml.CreateElement(valueTypeStr);
            valueType.InnerText = "float";
            root.AppendChild(valueType);

            XmlElement element = xml.CreateElement(name);
            element.InnerText = value.ToString();
            root.AppendChild(element);

            xml.AppendChild(root);
            xml.Save(filePath + name + FILE_TYPE);
        }

        public void Save(int value, string name)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("Root");

            XmlElement valueType = xml.CreateElement(valueTypeStr);
            valueType.InnerText = "int";
            root.AppendChild(valueType);

            XmlElement element = xml.CreateElement(name);
            element.InnerText = value.ToString();
            root.AppendChild(element);

            xml.AppendChild(root);
            xml.Save(filePath + name + FILE_TYPE);
        }

        public void Save(bool value, string name)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("Root");

            XmlElement valueType = xml.CreateElement(valueTypeStr);
            valueType.InnerText = "bool";
            root.AppendChild(valueType);

            XmlElement element = xml.CreateElement(name);
            element.InnerText = value.ToString();
            root.AppendChild(element);

            xml.AppendChild(root);
            xml.Save(filePath + name + FILE_TYPE);
        }

        public void Save(Vector3 value, string name)
        {
            
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("Root");

            XmlElement element = xml.CreateElement(name);

            XmlElement valueType = xml.CreateElement(valueTypeStr);
            valueType.InnerText = "Vector3";
            root.AppendChild(valueType);

            XmlElement xValue = xml.CreateElement("X");
            xValue.InnerText = value.x.ToString();
            element.AppendChild(xValue);

            XmlElement yValue = xml.CreateElement("Y");
            yValue.InnerText = value.y.ToString();
            element.AppendChild(yValue);

            XmlElement zValue = xml.CreateElement("Z");
            zValue.InnerText = value.z.ToString();
            element.AppendChild(zValue);


            root.AppendChild(element);
            xml.AppendChild(root);
            xml.Save(filePath + name + FILE_TYPE);
        }

        public void Save(Vector2 value, string name)
        {
            
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("Root");

            XmlElement element = xml.CreateElement(name);

            XmlElement valueType = xml.CreateElement(valueTypeStr);
            valueType.InnerText = "Vector3";
            root.AppendChild(valueType);

            XmlElement xValue = xml.CreateElement("X");
            xValue.InnerText = value.x.ToString();
            element.AppendChild(xValue);

            XmlElement yValue = xml.CreateElement("Y");
            yValue.InnerText = value.y.ToString();
            element.AppendChild(yValue);


            root.AppendChild(element);
            xml.AppendChild(root);
            xml.Save(filePath + name + FILE_TYPE);
        }

        public void Save(string value, string name)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("Root");

            XmlElement valueType = xml.CreateElement(valueTypeStr);
            valueType.InnerText = "string";
            root.AppendChild(valueType);

            XmlElement element = xml.CreateElement(name);
            element.InnerText = value.ToString();
            root.AppendChild(element);

            xml.AppendChild(root);
            xml.Save(filePath + name + FILE_TYPE);
        }

        public void Save(object value, string name){
            if (!Directory.Exists(filePath + "Objects")){
                Directory.CreateDirectory(filePath + "Objects");
            }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File. Create(filePath + "Objects/" + name + FILE_TYPE);

            var json = JsonUtility.ToJson(value);

            formatter.Serialize(file, json);

            file.Close();
        }


        public bool TryGetNodeList(string name, out XmlNodeList nodeList)
        {
            
            XmlDocument xml = new XmlDocument();

            if (File.Exists(filePath + name + FILE_TYPE))
            {
                xml.Load(filePath + name + FILE_TYPE);
                nodeList = xml.GetElementsByTagName(name);
                return true;
            }
            else
            {
                nodeList = null;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>return 0 if not founded</returns>
        public float LoadFloat(string name)
        {
            
            const string type = "float";
            XmlDocument xml = new XmlDocument();

            if (File.Exists(filePath + name + FILE_TYPE))
            {
                xml.Load(filePath + name + FILE_TYPE);
                if (xml.GetElementsByTagName(valueTypeStr)[0].InnerText != type)
                {
                    Debug.LogError("return type is not the same type of the value type");
                    return 0f;
                }
                return float.Parse(xml.GetElementsByTagName(name)[0].InnerText);
            }
            return 0f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>return 0 if not founded</returns>
        public int LoadInt(string name)
        {
            const string type = "int";
            
            if (File.Exists(filePath + name + FILE_TYPE))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(filePath + name + FILE_TYPE);
                if (xml.GetElementsByTagName(valueTypeStr)[0].InnerText != type)
                {
                    Debug.LogError("return type is not the same type of the value type");
                    return 0;
                }
                return int.Parse(xml.GetElementsByTagName(name)[0].InnerText);
            }
            else{
                Debug.LogWarning("File not found");
                return 0;
            }
        }

        /// <summary>
        /// load a bool value
        /// </summary>
        /// <param name="name"></param>
        /// <returns>false if not fouded.</returns>
        public bool LoadBool(string name)
        {
            
            const string type = "bool";
            XmlDocument xml = new XmlDocument();

            if (File.Exists(filePath + name + FILE_TYPE))
            {
                xml.Load(filePath + name + FILE_TYPE);
                if (xml.GetElementsByTagName(valueTypeStr)[0].InnerText != type)
                {
                    Debug.LogError("return type is not the same type of the value type");
                    return false;
                }
                return bool.Parse(xml.GetElementsByTagName(name)[0].InnerText);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>return Vector3.zero if not fouded</returns>
        public Vector3 LoadVector3(string name)
        {
            
            const string type = "Vector3";
            XmlDocument xml = new XmlDocument();

            if (File.Exists(filePath + name + FILE_TYPE))
            {
                xml.Load(filePath + name + FILE_TYPE);
                if (xml.GetElementsByTagName(valueTypeStr)[0].InnerText != type)
                {
                    Debug.LogError("return type is not the same type of the value type");
                    return Vector3.zero;
                }
                var x = float.Parse(xml.GetElementsByTagName("X")[0].InnerText);
                var y = float.Parse(xml.GetElementsByTagName("Y")[0].InnerText);
                var z = float.Parse(xml.GetElementsByTagName("Z")[0].InnerText);

                return new Vector3(x, y, z);
            }
            return Vector3.zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>return Vector2.zero if not fouded</returns>
        public Vector2 LoadVector2(string name)
        {
            
            const string type = "Vector2";
            XmlDocument xml = new XmlDocument();

            if (File.Exists(filePath + name + FILE_TYPE))
            {
                xml.Load(filePath + name + FILE_TYPE);
                if (xml.GetElementsByTagName(valueTypeStr)[0].InnerText != type)
                {
                    Debug.LogError("return type is not the same type of the value type");
                    return Vector2.zero;
                }
                var x = float.Parse(xml.GetElementsByTagName("X")[0].InnerText);
                var y = float.Parse(xml.GetElementsByTagName("Y")[0].InnerText);

                return new Vector3(x, y);
            }
            return Vector2.zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>return "" if not founded</returns>
        public string LoadString(string name)
        {
            const string type = "string";
            XmlDocument xml = new XmlDocument();

            if (File.Exists(filePath + name + FILE_TYPE))
            {
                xml.Load(filePath + name + FILE_TYPE);
                if (xml.GetElementsByTagName(valueTypeStr)[0].InnerText != type)
                {
                    Debug.LogError("return type is not the same type of the value type");
                    return "";
                }
                return xml.GetElementsByTagName(name)[0].InnerText;
            }
            return "";
        }

        public bool LoadObject(string name, object obj){
            if (File.Exists(filePath + "Objects/" + name + FILE_TYPE)){
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(filePath + "Objects/" + name + FILE_TYPE, FileMode.Open);

                JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), obj);

                file.Close();
                return true;
            }
            else{
                Debug.LogWarning("File not found! (" + filePath + "Objects/" + name + FILE_TYPE + ")");
                return false;
            }
        }
    }
}