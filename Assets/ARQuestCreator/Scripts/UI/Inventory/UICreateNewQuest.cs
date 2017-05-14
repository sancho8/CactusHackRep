using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ARQuestCreator.UI
{
    public class UICreateNewQuest : MonoBehaviour, IUIController
    {

        [SerializeField] Text _newQuestName;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void OnCreateNewQuest()
        {
            if (_newQuestName.text == "")
            {
                Debug.LogError("Unacceptable name!");
                return;
            }
            GameManager.Instance.CreateNewGame(_newQuestName.text);
        }
    }
}
