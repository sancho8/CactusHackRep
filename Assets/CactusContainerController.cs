using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator
{
    public class CactusContainerController : MonoBehaviour
    {

        [SerializeField] bool _isLocked = true;
        [SerializeField] GameObject _container;
        private IConditionChecker[] _conditions;
        // Use this for initialization
        void Start()
        {
            _container.SetActive(!_isLocked);
            _conditions = GetComponents<IConditionChecker>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_isLocked && IsAllRequiredConditionsSatisfied())
            {
                OnOpen();
            }
        }

        void OnOpen()
        {
            _isLocked = false;
            _container.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
        }

        private bool IsAllRequiredConditionsSatisfied()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.IsSatisfied())
                    return false;
            }
            return true;
        }
    }
}