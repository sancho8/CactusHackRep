﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using ARQuestCreator.SceneCreator;

namespace ARQuestCreator
{
    [AddComponentMenu("ARQuestCreator/TrackableEventHandler")]
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES

        public SceneManager scene;

        public enum State
        {
            Disabled,
            WaitingForScene,
            EnabledScene
        }
        [SerializeField] private State _currentState = State.Disabled;
        #region UNTIY_MONOBEHAVIOUR_METHODS

        void OnEnable()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

        }

        private void OnDisable()
        {
            if (mTrackableBehaviour != null)
                mTrackableBehaviour.UnregisterTrackableEventHandler(this);
        }

        private void Update()
        {
            if (scene != null)
            {
                scene.transform.position = transform.position;
                scene.transform.rotation = transform.rotation;
                scene.transform.localScale = transform.localScale;
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        public void Reload()
        {
            _currentState = State.Disabled;
            if (scene != null)
            {
                Destroy(scene.gameObject);
            }
            scene = null;
        }

        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {

            if (_currentState == State.Disabled)
            {
                _currentState = State.WaitingForScene;
                GameManager.Instance.SetTrackableEH(this);
            }

            scene.SetActive(true);
            
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            if (scene != null)
            {
                OnDisable();
                scene.SetActive(false);
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}

