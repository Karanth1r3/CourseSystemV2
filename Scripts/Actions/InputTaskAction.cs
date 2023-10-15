using course;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


//created for testing purposes, probably won't be used in some serious aspect
public class InputTaskAction : TaskAction, I_InteractableInput, ITriggerZone
{
    [SerializeField] InputActionReference keyBinding;
    [SerializeField] float delay = 0f;
    public KeyCode actionKey;
    [SerializeField] private bool isCorrect; 
    [SerializeField] private bool requiresTrigger; // this value should be set in inspector if action is performed only in limited area
    [SerializeField] private bool triggered;

    public event ITriggerZone.ITriggerZone onEnter;
    public event ITriggerZone.ITriggerZone onExit;

    void LinkBindings()
    {
        keyBinding.action.performed += e => HandleAction();
    }

    private void RemoveBindings()
    {
        keyBinding.action.performed -= e => HandleAction();
    }

    private void Start()
    {
        LinkBindings();
    }

    private void OnDestroy()
    {
        RemoveBindings();
    }

    public string GetActionsDescriptions()
    {
        return actionDescription;
    }

    // Start is called before the first frame update
    public void HandleInput()
    {
        //if(!isInteractable) return;
        if (linkedTasks.Count == 0) return;
        if (!triggered && requiresTrigger) { return; }

        if (Input.GetKeyDown(actionKey))
        {
            Invoke("HandleAction", delay);
        }
    }

    private void HandleAction()
    {
        if (isCorrect) { Complete(); }
        else { Fail(); }
    }

    //private void Update()
    //{
    //    HandleInput();
    //}

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
