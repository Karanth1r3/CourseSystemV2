using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace course
{
    // main function of this class is managing tasks changes and progression through course,
    // but some additional info is processed via Hub
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

        //todo - probably will be needed to handle this outside of onenable/ondisable scenarios
        private void OnEnable()
        {
            ActivateCourse();
        }

        void ActivateCourse()
        {
            LinkWithStepHub();
            LinkTasksWithHub();
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
            lastStep._OnStepEnded.AddListener(CompleteCourse);
        }

        // if this function is not executed on course end, may lead to undesired effects (or not)
        void UnsubscribeLastTask()
        {
            lastStep._OnStepEnded.RemoveListener(CompleteCourse);
        }

        // info is sent to hub for easier access to information about tasks/course => gathered in one place
        void LinkWithStepHub()
        {
            if (stepHub == null) stepHub = transform.parent.GetComponent<TasksHub>();

            if (stepHub == null) { Debug.LogError("No Step Hub found in scene"); return; }

            stepHub.activeTaskAdded.AddListener(AddStepToActiveList);
            stepHub.activeTaskRemoved.AddListener(RemoveStepFromActiveList);
        }

        // for managing active/inactive tasks with the Hub
        private void LinkTasksWithHub()
        {
            foreach (Task task in courseSteps)
            {
                stepHub.SubscribeTask(task);
            }
        }

        private void UnlinkTasksFromHub()
        {
            foreach (Task task in courseSteps)
            {
                stepHub.UnsubscribeTask(task);
            }
        }
        //.............................................
        public void HandleStepChange()
        {
            _stepChanged.Invoke();
        }
        void LinkStepsWithCourse()
        {
            foreach (Task step in courseSteps)
            {
                step._OnStepEnded.AddListener(HandleStepChange);
            }
        }

        // for debugging purposes i guess
        public void AddStepToActiveList(Task step)
        {
            if (currentActiveSteps.Contains(step)) return;
            currentActiveSteps.Add(step);
        }

        public void RemoveStepFromActiveList(Task step)
        {
            currentActiveSteps.Remove(step);
        }
        //..................................................................................................................

        // these handle different scenarios of ending the course
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
        public void EndCourse()
        {
            _courseEnded.Invoke();
            gameObject.SetActive(false);
            foreach (Task step in currentActiveSteps)
            {
                step.DeactivateStep();
            }
            currentActiveSteps.Clear();
            UnsubscribeLastTask();
        }
        void DeactivateCourse()
        {
            ResetSteps();
            UnlinkTasksFromHub();
        }

        void ResetSteps()
        {
            foreach (Task step in courseSteps)
            {
                step.DeactivateStep();
                step._OnStepEnded.RemoveListener(HandleStepChange);
            }
        }

        private void OnDisable()
        {
            DeactivateCourse();
        }
        //................
    }

}
