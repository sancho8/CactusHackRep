using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ARQuestCreator.SceneCreator
{
    public class SceneManager : MonoBehaviour
    {

        [SerializeField] string _xmlFileName;
        [SerializeField] SceneContainer _sceneContainer = new SceneContainer();
        [SerializeField] bool _save;
        [SerializeField] bool _load;
        [SerializeField] bool _reset;


        // Use this for initialization
        void Start()
        {

        }
        [SerializeField] Transform _root;
        [SerializeField] GameObject _prefab;
        // Update is called once per frame
        void Update()
        {
            if (_save)
            {
                _save = false;
                Save();
            }
            if (_load)
            {
                _load = false;
                Load();
            }
            if (_reset)
            {
                _reset = false;
                ResetContent();
            }

            for (int i=0; i<_root.transform.childCount; i++) { 
                Destroy(_root.GetChild(i).gameObject);
            }
            foreach(var item in _sceneContainer._items)
            {
                GameObject go = (GameObject)Instantiate(_prefab);
                go.transform.GetChild(0).GetComponent<Text>().text = item.name;
                go.transform.SetParent(_root.transform);
            }
        }

        public void Save()
        {
            _sceneContainer.Save(_xmlFileName);
        }

        public void Load()
        {

            _sceneContainer = SceneContainer.Load(_xmlFileName);
        }

        public void ResetContent()
        {

            _sceneContainer = new SceneContainer();
        }

  

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
