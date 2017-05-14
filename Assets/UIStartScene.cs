using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator.UI
{
    public class UIStartScene : MonoBehaviour, IUIController
    {

        [SerializeField] UIMainMenu _main;
        [SerializeField] UICreateNewQuest _createQuest;
        [SerializeField] UIChooseQuest _chooseQuest;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        void Start()
        {
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            _main.Show();
            _createQuest.Hide();
            _chooseQuest.Hide();
        }
    }
}