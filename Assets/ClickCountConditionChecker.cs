using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator
{
    public class ClickCountConditionChecker : MonoBehaviour, IWorldButtonClickHandler, IConditionChecker
    {
        
        public int needClicks = 5;
        public WorldButton worldButton;

        public bool IsSatisfied()
        {
            throw new NotImplementedException();
        }

        public void OnWorldButtonClickHandler()
        {
            throw new NotImplementedException();
        }

        // Use this for initialization
        void Awake()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
