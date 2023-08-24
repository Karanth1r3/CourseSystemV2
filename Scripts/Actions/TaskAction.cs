using course;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TaskAction : MonoBehaviour, ITaskAction
{
    [SerializeField] protected List<Task> linkedTasks = new List<Task>();
    [SerializeField] protected bool isInteractable = false;
    public UnityEvent _successEvent, _failEvent, _doneEvent;
    [SerializeField] protected string actionDescription; // for UI
    // Start is called before the first frame update

    public void Complete()
    {
        if (linkedTasks.Count == 0) { return; }
        if(!isInteractable) { return; }
        for(int i = 0; i < linkedTasks.Count; i++) // DANGEROUS ------------------------------------ better to think up something better, possible breaks when 2 tasks are linked
        {
            linkedTasks[i].CompleteStep();
        }
        //foreach (SimpleTask task in linkedTasks)
        //{
        //    task.Proceed();
        //}
        _successEvent.Invoke();
        _doneEvent.Invoke();
    }

    public void Fail()
    {
        if(linkedTasks.Count == 0) { return; }
        if (!isInteractable) { return; }
        for (int i = 0; i < linkedTasks.Count; i++) // DANGEROUS ------------------------------------ better to think up something better
        {
            linkedTasks[i].FailStep();
        }
        _failEvent.Invoke();
        _doneEvent.Invoke();
    }

    public void RemoveTask(Task task)
    {
        linkedTasks.Remove(task);
        if (linkedTasks.Count == 0) isInteractable = false;
    }

    // Update is called once per frame
   public void LinkTask(Task task)
    {
        if (linkedTasks.Contains(task)) return;
        linkedTasks.Add(task);
        if (linkedTasks.Count > 0) isInteractable = true;
    }

    public string GetActionDescription()
    {
        return actionDescription;
    }

    private void OnValidate()
    {
        if(actionDescription != string.Empty)
        {
            gameObject.name = "Действие - " + actionDescription;
        }
    }
}
