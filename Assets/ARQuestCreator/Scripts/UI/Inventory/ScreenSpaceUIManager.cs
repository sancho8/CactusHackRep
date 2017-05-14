using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuestCreator.UI {
    public class ScreenSpaceUIManager : Singleton<ScreenSpaceUIManager> {

        [SerializeField] UIInventoryController _inventoryUI;
        [SerializeField] UIViewItemController _itemViewUI;
        [SerializeField] UIPlayerController _playerUI;
        [SerializeField] UIPushNotificationController _notificationUI;
        [SerializeField] UIEditiSceneController _editSceneUI;
        [SerializeField] UIStartScene _mainUI;

        private IUIController[] _array;

        public enum UIType
        {
            Nothing,
            Inventory,
            ItemView,
            Player,
            Edit,
            Main
        }

        private void Awake()
        {
            _array = new IUIController[] {
                _inventoryUI,
                _itemViewUI,
                _playerUI,
                _editSceneUI,
                _mainUI
            };
        }

        public void ShowUI(UIType uitype)
        {
            int index = -1;
            _editSceneUI.Hide();
            switch (uitype)
            {
                case UIType.Inventory:
                    index = 0;
                    break;
                case UIType.ItemView:
                    index = 1;
                    break;
                case UIType.Player:
                    index = 2;
                    break;
                case UIType.Edit:
                    index = 3;
                    break;
                case UIType.Main:
                    index = 4;
                    break;
                case UIType.Nothing:
                    index = -1;
                    break;
                default:
                    Debug.LogError("Undefined UIType!!!", this);
                    return;
                    break;
            }

            for (int i=0; i<_array.Length; i++)
            {
                if (i == index)
                {
                    _array[i].Show();
                }
                else
                {
                    _array[i].Hide();
                }
            }
        }

        public void ShowNotification(string message,
            UIPushNotificationController.NotificationLifeTimeType lifeTime = UIPushNotificationController.NotificationLifeTimeType.Medium,
            UIPushNotificationController.NotificationType type = UIPushNotificationController.NotificationType.Neutral)
        {
            _notificationUI.SetNotification(message, lifeTime, type);
            _notificationUI.Show();
        }
    }
}
