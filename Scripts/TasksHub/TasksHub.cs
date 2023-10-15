using course;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


// class created for easier access to tasks and their included information
public class TasksHub : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public List<Task> activeTasks = new List<Task>();
    public UnityEvent<Task> activeTaskAdded, activeTaskRemoved;
    public UnityEvent activaTaskListChanged;

    // probably will be needed, not clear for now
    public Course CurrentCourse => currentCourse;
    private Course currentCourse;

    public class SimpleTaskEvent : UnityEvent<Task> { }

    // when course/quest becomes active, it's tasks are processed with this function,
    // when deactivated - with second one
    public void SubscribeTask(Task task)
    {
        task._OnStepActivated.AddListener(delegate { AddActiveTask(task); });
        task._OnStepDeactivated.AddListener(delegate { RemoveActiveTask(task); });
    }

    public void UnsubscribeTask(Task task)
    {
        task._OnStepActivated.RemoveListener(delegate { AddActiveTask(task); });
        task._OnStepDeactivated.RemoveListener(delegate { RemoveActiveTask(task); });
    }

    // if task becomes active in course/quest it will be processed here
    // if deactivated - second one is called
    void AddActiveTask(Task task)
    {
        if (activeTasks.Contains(task)) return;
        activeTasks.Add(task);
        activeTaskAdded.Invoke(task);
        activaTaskListChanged.Invoke();
    }

    void RemoveActiveTask(Task task)
    {
        activeTasks.Remove(task);
        activeTaskRemoved.Invoke(task);
        activaTaskListChanged.Invoke();
    }

    public List<Task> GetAllTasks()
    {
        return tasks;
    }
}
