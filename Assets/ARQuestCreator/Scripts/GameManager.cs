using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARQuestCreator.UI;
using ARQuestCreator.SceneCreator;
namespace ARQuestCreator
{
    public class GameManager : Singleton<GameManager>{

        public enum GameRunningType
        {
            Play,
            Edit
        }

        private void Start()
        {
            ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Player);
        }

        public void ViewItem(Item item)
        {
            //PlayerInventory.Instance.RemoveItem(item);
            if (ItemViewer.Instance.IsEmpty())
            {
                ItemViewer.Instance.ViewItem(item);
                ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.ItemView);
            }
            else
            {
                Debug.Log("Cant view item coz alredy view another item! Pickup immediately");
                PickupItem(item);
            }
        }
                     
        public void PickupItem(Item item)
        {
            PlayerInventory.Instance.AddItem(item);
            ScreenSpaceUIManager.Instance.ShowNotification("Pickup " + item.name, UIPushNotificationController.NotificationLifeTimeType.Short, UIPushNotificationController.NotificationType.Positive);
            if (ItemViewer.Instance.IsEmpty())
                ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Player);
        }

        public void OnInventoryShow()
        {
            ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Inventory);
        }

        public void OnInventoryHide()
        {
            ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Player);
        }

        public List<string> GetAllGamesNames()
        {
            return AllGamesContainer.Load().gameNames;
        }

        public void LoadGame(string gameName, GameRunningType grt)
        {
            Debug.Log("Load game" + gameName+" |  editMode: "+ grt);
        }

        public void CreateNewGame(string gameName)
        {
            var newGame = AllGamesContainer.Load().CreateNewGameContainer(gameName);
            LoadGame(gameName, GameRunningType.Edit);
        }
    }
}


