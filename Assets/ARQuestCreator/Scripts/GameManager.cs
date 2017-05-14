using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ARQuestCreator.UI;
using ARQuestCreator.SceneCreator;
namespace ARQuestCreator
{
    public class GameManager : Singleton<GameManager>{

        public DefaultTrackableEventHandler _currentT { get; private set; }
        [SerializeField] GameContainer _currentGC;
        [SerializeField] GameRunningType _currentGameRunningType;
        public enum GameRunningType
        {
            Play,
            Edit,
            Menu
        }

        private void Start()
        {
            OnMainMenu();
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
            Debug.Log("Load game" + gameName+" |  editMode: "+ grt.ToString());
            Vuforia.VuforiaBehaviour.Instance.enabled = true;
            _currentGC = GameContainer.Load(gameName);
            _currentGameRunningType = grt;
            DefaultTrackableEventHandler[] tehs = GameObject.FindObjectsOfType<DefaultTrackableEventHandler>();
            foreach (var teh in tehs)
            {
                teh.Reload();
            }
            switch (grt)
            {
                case GameRunningType.Edit:
                    ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Edit);
                    break;
                case GameRunningType.Play:
                    List<string> snames = new List<string>();
                    foreach (var sc in _currentGC.scenes)
                    {
                        snames.Add(sc.name);
                    }
                    foreach (var teh in tehs)
                    {
                        if (!snames.Contains(teh.name))
                        {
                            teh.gameObject.SetActive(false);
                        }
                    }
                    ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Player);
                    break;
            }
            
        }

        public void CreateNewGame(string gameName)
        {
            var newGame = AllGamesContainer.Load().CreateNewGameContainer(gameName);
            LoadGame(gameName, GameRunningType.Edit);
        }

        public void SetTrackableEH(DefaultTrackableEventHandler go)
        {
            if (go.scene == null)
            {
                var sc = _currentGC.GetSceneContainerByName(go.name);
                if (sc == null)
                {
                    Debug.Log("SceneContainer is not initialized");
                    if (_currentGameRunningType == GameRunningType.Edit)
                    {
                        Debug.Log("Add new scene to quest");
                        sc = new SceneContainer(true);
                        sc.name = go.name;
                    }
                    else
                    {
                        go.gameObject.SetActive(false);
                        return;
                    }
                }
                _currentT = go;
                GameObject newSM = new GameObject();
                ARQuestCreator.SceneCreator.SceneManager newSceneManager = newSM.AddComponent<ARQuestCreator.SceneCreator.SceneManager>();
                newSceneManager.SetScene(sc);
                _currentT.scene = newSceneManager;
            }
        }

        public void OnExitToMainMenu()
        {
            OnMainMenu();
        }

        void OnMainMenu()
        {
            Vuforia.VuforiaBehaviour.Instance.enabled = false;
            _currentGameRunningType = GameRunningType.Menu;
            ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Main);
        }
    }
}


