using UnityEngine;
using UnityEngine.Events;

namespace course
{
    public abstract class AbstractTask : MonoBehaviour, ITask
    {

        // if not true - no signs of this task should be visible in the scene or smth like that
        public bool Interactable { get; set; }
        // intended to send to outer systems to process which steps handle after this step is completed
        [SerializeField] protected TaskWithOptions[] tasksAfterValid, tasksAfterFail;
        // events to subscribe custom behaviour on different results from this action
        public UnityEvent proceededEvent, failedEvent;
        // wtf
        public event ITask.ITask onProceeded;
        public event ITask.ITask onFailed;

        public void Fail()
        {
            onFailed?.Invoke();
        }

        public void Proceed()
        {
            onProceeded?.Invoke();
        }

        public void Activate()
        {
            Interactable = true;
        }


        public void Deactivate()
        {
            Interactable = false;
        }

    }
}

