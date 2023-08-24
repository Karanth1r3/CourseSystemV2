using course;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Course : MonoBehaviour
{
    [SerializeField] private TasksHub stepHub; //
    [SerializeField] private Task[] courseSteps;
    [SerializeField] private Task[] mainSteps;
    [SerializeField] private List<Task> completedMainSteps;
    [SerializeField] private Task[] firstSteps;
    [SerializeField] private Task lastStep;
    private List<Task> currentActiveSteps = new List<Task>();

    public UnityEvent _courseStarted, _courseEnded, _courseSucceed, _courseFailed, _courseBroke, _stepChanged;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ActivateCourse();
    }

    void LinkWithStepHub()
    {
        if(stepHub == null) stepHub = transform.parent.GetComponent<TasksHub>();

        if (stepHub == null) { Debug.LogError("No Step Hub found in scene"); return; }

        stepHub.activeTaskAdded.AddListener(AddStepToActiveList);
        stepHub.activeTaskRemoved.AddListener(RemoveStepFromActiveList);
    }


    void ActivateCourse()
    {
        LinkWithStepHub();
        InitializeCourse();
        LinkStepsWithCourse();
        SubscribeLastTask();
    }

    public void InitializeCourse()
    {
        foreach (Task task in firstSteps)
        {
            task.ActivateStep();
        }

    }

    void SubscribeLastTask()
    {
        lastStep._stepEnded.AddListener(CompleteCourse);
    }

    void UnsubscribeLastTask()
    {
        lastStep._stepEnded.RemoveListener(CompleteCourse);
    }

    void LinkStepsWithCourse()
    {
        foreach (Task step in courseSteps)
        {
            step._stepEnded.AddListener(HandleStepChange);
        }
    }
    void ResetSteps()
    {
        foreach (Task step in courseSteps)
        {
            step.DeactivateStep();
            step._stepEnded.RemoveListener(HandleStepChange);
        }
    }

    void DeactivateCourse()
    {
        ResetSteps();
    }

    private void OnDisable()
    {
        DeactivateCourse();
    }

    public void EndCourse()
    {
        _courseEnded.Invoke();
        gameObject.SetActive(false);
        foreach(Task step in currentActiveSteps)
        {
            step.DeactivateStep();
        }
        currentActiveSteps.Clear();
        UnsubscribeLastTask();
    }

    public void CompleteCourse()
    {
        _courseSucceed.Invoke();
        EndCourse();
    }

    public void FailCourse()
    {
        _courseFailed.Invoke();
        EndCourse();
    }

    public void BreakCourse()
    {
        _courseBroke.Invoke();
        EndCourse();
    }

    public void AddStepToActiveList(Task step)
    {
        if (currentActiveSteps.Contains(step)) return;
        currentActiveSteps.Add(step);
    }

    public void RemoveStepFromActiveList(Task step)
    {
        currentActiveSteps.Remove(step);
    }


    public void HandleStepChange()
    {
        _stepChanged.Invoke();
    }

}
