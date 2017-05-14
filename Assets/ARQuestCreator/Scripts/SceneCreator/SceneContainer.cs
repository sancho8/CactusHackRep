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
            public string description;
            public GameObjectInfo gameObject;

            public ItemInfo() { }

            public static ItemInfo GetFromItem(Item item)
            {
                ItemInfo r = new ItemInfo();
                r.name = item.name;
                r.description = item.description;
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
            /// <summary>
            /// Local position of GameObject
            /// </summary>
            public Vector3 position;
            /// <summary>
            /// Local rotation of GameObject
            /// </summary>
            public Quaternion rotation;
            /// <summary>
            /// Local scale of GameObject
            /// </summary>
            public Vector3 scale;
            public string parentInstanceName;

            public GameObjectInfo() { }

            public static GameObjectInfo GetFromGameObject(GameObject go)
            {
                GameObjectInfo r = new GameObjectInfo();
                IPrefab iprefab = go.GetComponent<IPrefab>();
                if (iprefab == null)
                {
                    Debug.Log("WTF");
                    return null;
                }
                r.instanceName = go.name;
                r.position = go.transform.localPosition;
                r.rotation = go.transform.localRotation;
                r.scale = go.transform.localScale;
                if (go.transform.parent != null)
                    r.parentInstanceName = go.transform.parent.name;
                else
                    r.parentInstanceName = "noParent";
                return r;
            }
        }

        public SceneContainer() { }
        public SceneContainer(bool isNew)
        {
            if (isNew)
            {
                _items = new List<ItemInfo>();
                _chests = new List<ChestInfo>();
            }
        }

        public string name;
        public List<ItemInfo> _items;
        public List<ChestInfo> _chests;
    }
}