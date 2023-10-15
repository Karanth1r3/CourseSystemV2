using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public void ProcessInvalidSignal()
    {
        taskAction.Fail();
    }
}
