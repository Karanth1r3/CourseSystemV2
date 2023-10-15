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

        public event IActionLinker.IActionLinker OnValidActionPerformed;
        public event IActionLinker.IActionLinker OnWrongActionPerformed;

        public void LinkAction()
        {
            onConditionsMet.AddListener(ProcessValidSignal);
            onConditionsFailed.AddListener(ProcessInvalidSignal);
            OnValidActionPerformed += onConditionsMet.Invoke;
            OnWrongActionPerformed += onConditionsFailed.Invoke;
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
            //onConditionsMet?.Invoke();
            OnValidActionPerformed?.Invoke();
        }

        public void PressWrongButton()
        {
            //onConditionsFailed?.Invoke();
            OnWrongActionPerformed?.Invoke();
        }

        public void ProcessInvalidSignal()
        {
            taskAction.Fail();
        }
    }
}

