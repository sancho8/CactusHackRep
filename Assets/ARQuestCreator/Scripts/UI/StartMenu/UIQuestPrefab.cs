using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ARQuestCreator.UI
{
    public class UIQuestPrefab : MonoBehaviour, IUIController
    {

        [SerializeField] Text _questNameText;
        private string _name;


        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }


        public void SetQuest(string name)
        {
            _questNameText.text = name;
            _name = name;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        void OnClick()
        {
            Debug.Log("AAAAAAAAAAAAAA");
        }


    }
}
