using course;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TasksHub : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public List<Task> activeTasks = new List<Task>();
    public UnityEvent<Task> activeTaskAdded, activeTaskRemoved;
    public UnityEvent activaTaskListChanged;
    // Start is called before the first frame update

    public class SimpleTaskEvent : UnityEvent<Task> { }

    private void Awake()
    {
        tasks = FindObjectsOfType<Task>().ToList();
        SubscribeTasks();
    }

    void SubscribeTasks()
    {
        foreach (Task task in tasks)
        {
            task._stepActivated.AddListener(delegate { AddActiveTask(task); });
            task._stepDeactivated.AddListener(delegate { RemoveActiveTask(task); });
        }

    }

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
