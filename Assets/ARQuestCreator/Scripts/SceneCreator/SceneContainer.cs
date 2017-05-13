using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ARQuestCreator.SceneCreator
{
    [System.Serializable]
    public class SceneContainer
    {
        [System.Serializable]
        public class ItemInfo
        {
            public string name;
            public string discription;
            public GameObjectInfo gameObject;

            public ItemInfo() { }

            public static ItemInfo GetFromItem(Item item)
            {
                ItemInfo r = new ItemInfo();
                r.name = item.name;
                r.discription = item.description;
                r.gameObject = GameObjectInfo.GetFromGameObject(item.gameObject);
                return r;
            }
        }

        [System.Serializable]
        public class ChestInfo
        {
            public GameObjectInfo gameObject;
            public string containerContent; // name of item instance
        }

        [System.Serializable]
        public class GameObjectInfo
        {
            public string prefabName;
            public string instanceName;
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
            public string parentInstanceName;

            public GameObjectInfo() { }

            public static GameObjectInfo GetFromGameObject(GameObject go)
            {
                GameObjectInfo r = new GameObjectInfo();
                r.instanceName = go.name;
                r.position = go.transform.position;
                r.rotation = go.transform.rotation;
                r.scale = go.transform.lossyScale;
                if (go.transform.parent != null)
                    r.parentInstanceName = go.transform.parent.name;
                else
                    r.parentInstanceName = "noParent";
                return r;
            }
        }

        public List<ItemInfo> _items;
        public List<ChestInfo> _chests;
        
        public void Save(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SceneContainer));
            TextWriter writer = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            serializer.Serialize(writer, this);
            writer.Close();
            Debug.Log("Objects saved into XML file\n");
        }

        public static SceneContainer Load(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SceneContainer));
            TextReader reader = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            SceneContainer sceneContainer = serializer.Deserialize(reader) as SceneContainer;
            reader.Close();
            Debug.Log("Objects loaded from XML file\n");
            return sceneContainer;
        }

    }
}