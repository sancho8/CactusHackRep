using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator.UI
{
    public class UIChooseQuest : MonoBehaviour, IUIController
    {

        [SerializeField] Transform grid;
        [SerializeField] UIQuestPrefab _uiQuestPrefab;

        public enum ChoosingType
        {
            Play,
            Edit
        }
        private ChoosingType _currentCT;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            UpdateGrid();
        }

        public void SetChoosingType(string choosingType)
        {
            switch (choosingType)
            {
                case "Play":
                    _currentCT = ChoosingType.Play;
                    return;
                case "Edit":
                    _currentCT = ChoosingType.Edit;
                    return;
                default:
                    Debug.LogError("WTF");
                    break;
            }
        }

        private void UpdateGrid()
        {

            var quests = GameManager.Instance.GetAllGamesNames();
            int x;
            if ((x = quests.Count - grid.childCount) > 0)
            {
                for (; x > 0; x--)
                {
                    GameObject go = ((UIQuestPrefab)Instantiate(_uiQuestPrefab)).gameObject;
                    go.transform.SetParent(grid);
                    go.transform.localScale = Vector3.one;
                }
            }
            UIQuestPrefab[] uiItems = grid.GetComponentsInChildren<UIQuestPrefab>(true);
            for (int i = 0; i < uiItems.Length; i++)
            {
                if (i >= quests.Count)
                    Destroy(uiItems[i].gameObject);
                else
                {
                    uiItems[i].SetQuest(quests[i]);
                    uiItems[i].Show();
                }
            }
        }

        public void OnChooseQuest(string name)
        {
            //Debug.Log("Choose quest " + name + "  |  ChoosingType: " + _currentCT.ToString());
            GetComponentInParent<UIStartScene>().ShowMainMenu();
            switch (_currentCT)
            {
                case ChoosingType.Edit:
                    GameManager.Instance.LoadGame(name, GameManager.GameRunningType.Edit);
                    break;
                case ChoosingType.Play:
                    GameManager.Instance.LoadGame(name, GameManager.GameRunningType.Play);
                    break;
                default:
                    Debug.Log("WTF");
                    break;
            }
        }
    }
}