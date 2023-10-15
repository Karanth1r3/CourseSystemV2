using course;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour, ITask
{
    [Header("Debug")]
    public bool isActivated;
    public bool isDone;

    [SerializeField] private Task successfulNext, failedNext;
    [SerializeField] private string description;
    [Header("Действия, связанные с текущим шагом")]
    [SerializeField] TaskAction[] linkedActions;

    [Header("Events")]
    public UnityEvent _stepActivated, _stepDeactivated, _stepCompleted, _stepFailed, _stepEnded;
    // Start is called before the first frame update


    void LinkActions()
    {
        foreach (TaskAction action in linkedActions)
        {
            action.LinkTask(this);
        }
    }

    void UnlinkActions()
    {
        foreach (TaskAction action in linkedActions)
        {
            action.RemoveTask(this);
        }
    }

    public string GetDescription() { return description; }

    // Update is called once per frame
    public void DeactivateStep()
    {
        UnlinkActions();
        isActivated = false;
        _stepDeactivated.Invoke();
    }

    public void ActivateStep()
    {
        isDone = false;
        isActivated = true;
        LinkActions();
        _stepActivated.Invoke();
    }
    
    void ActivateNextStep(Task next)
    {
        next.ActivateStep();
    }

    public void EndStep()
    {
        isDone = true;
        _stepEnded.Invoke();
    }

    public void CompleteStep()
    {
        DeactivateStep();
        EndStep();
        _stepCompleted.Invoke();
        if(successfulNext != null) ActivateNextStep(successfulNext);
    }

    public void FailStep()
    {
        DeactivateStep();
        EndStep();
        _stepFailed.Invoke();
        if(failedNext != null) ActivateNextStep(failedNext);
    }

    public TaskAction[] GetLinkedActions()
    {
        return linkedActions;
    }

    public string[] GetActionsDescriptions()
    {
        string[] descs = new string[linkedActions.Length];
        for(int i = 0; i < linkedActions.Length; ++i)
        {
            descs[i] = linkedActions[i].GetActionDescription();
        }
        return descs;
    }

}
