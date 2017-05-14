using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARQuestCreator.SceneCreator;

namespace ARQuestCreator.UI
{
    public class UIEditiSceneController : MonoBehaviour, IUIController
    {
        [SerializeField] SceneManager _sceneManager;

        [Header("Main")]
        public GameObject _main;
        [Header("ItemCatalog")]
        public GameObject _catalog;
        public GameObject _catalogGrid;
        [Header("OnSelected")]
        public GameObject _onSelected;
        public GameObject _itemSelected;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void SetSceneManager(SceneManager sm)
        {
            _sceneManager = sm;
        }


    }
}
