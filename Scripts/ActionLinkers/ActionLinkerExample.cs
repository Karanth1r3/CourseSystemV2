using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace course
{
    // some lever or button may be inherited from IActionLinker for connection with the system
    // probably will look like that
    public class ActionLinkerExample : MonoBehaviour, IActionLinker
    {
        [SerializeField] private TaskAction taskAction;
        [SerializeField] private UnityEvent onConditionsMet, onConditionsFailed;


        public void LinkAction()
        {
            onConditionsMet.AddListener(ProcessValidSignal);
            onConditionsFailed.AddListener(ProcessInvalidSignal);
        }

        private void Start()
        {
            LinkAction();
        }
        public void ProcessValidSignal()
        {
            taskAction.Complete();
        }

        public void PressCorrectButton()
        {
            onConditionsMet?.Invoke();
        }

        public void PressWrongButton()
        {
            onConditionsFailed?.Invoke();
        }

        public void ProcessInvalidSignal()
        {
            taskAction.Fail();
        }
    }
}

