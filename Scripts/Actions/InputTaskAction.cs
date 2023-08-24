using course;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputTaskAction : TaskAction, I_InteractableInput, ITriggerZone
{
    [SerializeField] float delay = 0f;
    public KeyCode actionKey;
    [SerializeField] private bool isCorrect; // should this action proceed linked steps or fail them
    [SerializeField] private bool requiresTrigger; // this value should be set in inspector if action is performed only in limited area
    [SerializeField] private bool triggered;
    //[Serializable] public struct Option { public KeyCode key; public bool isValid; public string description; };
    //public Option[] options;

    public event ITriggerZone.ITriggerZone onEnter;
    public event ITriggerZone.ITriggerZone onExit;

    public string GetActionsDescriptions()
    {
        return actionDescription;
    }

    // Start is called before the first frame update
    public void HandleInput()
    {
        //if(!isInteractable) return;
        if(linkedTasks.Count == 0) return;
        if(!triggered && requiresTrigger) { return; }

        if(Input.GetKeyDown(actionKey))
        {
            Debug.LogError("Test");
            Invoke("HandleAction", delay);
        }
    }

    void HandleAction()
    {
        if (isCorrect) { Complete(); }
        else { Fail(); }
    }

    private void Update()
    {
        HandleInput();
    }

    public void HandleEnter()
    {
        if (requiresTrigger) triggered = true;
        onEnter.Invoke();
    }

    public void HandleExit()
    {
        if(requiresTrigger) triggered = false;
        onExit.Invoke();
    }

    public void HandleStay()
    {
    }
}
