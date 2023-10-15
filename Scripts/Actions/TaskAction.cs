using course;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace course
{
    public class TaskAction : MonoBehaviour, ITaskAction
    {
        [SerializeField] protected List<Task> linkedTasks = new List<Task>();
        [SerializeField] protected bool isInteractable = false;
        [SerializeField] protected string actionDescription; // for UI

        [SerializeField] private UnityEvent _OnSuccess, _OnFail, _OnDone;

        // task is a more abstract/wider thing than action. task may contain multiple actions
        // Action may refer to different tasks as well.
        // Action subscribes to tasks when the corresponding course is activated
        public void LinkTask(Task task)
        {
            if (linkedTasks.Contains(task)) return;
            linkedTasks.Add(task);
            if (linkedTasks.Count > 0) isInteractable = true;
        }
        public void RemoveTask(Task task)
        {
            linkedTasks.Remove(task);
            if (linkedTasks.Count == 0) isInteractable = false;
        }

        public void Complete()
        {
            if (linkedTasks.Count == 0) { return; }
            if (!isInteractable) { return; }
            for (int i = 0; i < linkedTasks.Count; i++) // DANGEROUS ------------------------------------ better to think up something better, possibly breaks when 2 tasks are linked
            {
                linkedTasks[i].CompleteStep();
            }
            //foreach (SimpleTask task in linkedTasks)
            //{
            //    task.Proceed();
            //}
            _OnSuccess?.Invoke();
            _OnDone?.Invoke();
        }

        public void Fail()
        {
            if (linkedTasks.Count == 0) { return; }
            if (!isInteractable) { return; }
            for (int i = 0; i < linkedTasks.Count; i++) // DANGEROUS ------------------------------------ better to think up something better
            {
                linkedTasks[i].FailStep();
            }
            _OnFail?.Invoke();
            _OnDone?.Invoke();
        }
        public void Execute(bool isValid)
        {
            if (isValid)
            { Complete(); }
            else
            { Fail(); }
        }

        // for UI & debugging
        public string GetActionDescription()
        {
            return actionDescription;
        }

        // easier to work with inspector with this
        private void OnValidate()
        {
            if (actionDescription != string.Empty)
            {
                gameObject.name = "Действие - " + actionDescription;
            }
        }
    }

}
