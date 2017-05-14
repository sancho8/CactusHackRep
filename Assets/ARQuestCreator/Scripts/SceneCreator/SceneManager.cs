using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARQuestCreator.SceneCreator
{
    public class SceneManager : MonoBehaviour
    {
        
        [SerializeField] SceneContainer _sceneContainer = new SceneContainer();
        [SerializeField] bool _save;
        [SerializeField] bool _load;
        [SerializeField] bool _reset;


        // Use this for initialization
        void Start()
        {

        }

        private void Update()
        {
            if (_load)
            {
                _load = false;
                Load();
            }
            if (_save)
            {
                _save = false;
                Save();
            }
            if (_reset)
            {
                _reset = false;
                ResetContent();
            }
        }

        public void Save()
        {

        }

        public void Load()
        {
        }

        public void ResetContent()
        {
            _sceneContainer = new SceneContainer();
        }

        #region Loading Scene

        private void InstantiateItem(SceneContainer.ItemInfo itemInfo)
        {
            GameObject prefab = Resources.Load("Prefabs/Items/" + itemInfo.gameObject.prefabName) as GameObject;
            GameObject go = (GameObject)Instantiate(prefab);

            go.name = itemInfo.gameObject.instanceName;
            go.transform.parent = transform;
            go.transform.localPosition = itemInfo.gameObject.position;
            go.transform.localRotation = itemInfo.gameObject.rotation;
            go.transform.localScale = itemInfo.gameObject.scale;

            Item item = go.GetComponent<Item>();
            item.name = itemInfo.name;
            item.description = itemInfo.description;
        }

        #endregion Loading Scene

        public void AddItem(Item item)
        {
            //ItemInfo itemInfo = ItemInfo.GetFromItem(item);
            //// validation
            //foreach (var ii in _items)
            //{
            //    if (ii.gameObject.instanceName == itemInfo.gameObject.instanceName)
            //    {
            //        Debug.Log("WTF");
            //        return;
            //    }
            //}
            //// validation 
            //_items.Add(itemInfo);
        }
    }
}
