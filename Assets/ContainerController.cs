using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator
{
    public class ContainerController : MonoBehaviour
    {
        [SerializeField] bool _isLocked = true;
        [SerializeField] GameObject _container;
        [SerializeField] Material _mat;
        private IConditionChecker[] _conditions;
        // Use this for initialization
        void Start()
        {
            _container.SetActive(!_isLocked);
            _conditions = GetComponents<IConditionChecker>();
            _mat.SetFloat("_LightPower", 0);
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
            _mat.SetFloat("_LightPower", 1);
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
