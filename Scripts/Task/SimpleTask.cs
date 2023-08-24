//using UnityEngine;
//using UnityEngine.Events;

//namespace course
//{
//    public class SimpleTask : MonoBehaviour, ITask
//    {
//        [Header("√лавное описание шага и побочное")]
//        [SerializeField] private string stepDescription, optionalStepDescription;
//        private Course course;
//        [SerializeField] private bool isDone = false; // serialized for debugging
//        public bool Interactable { get; private set; }
//        public UnityEvent _proceeded, _failed, _done, _unDone, _activated, _deactivated;
//        [Header(" акие шаги активируютс€ при правильном или неправильном выполнении")]
//        [SerializeField] private SimpleTask[] nextStepsAfterSuccess, nextStepsAfterFail;
//        [Header("ƒействи€, св€занные с текущим шагом")]
//        [SerializeField] TaskAction[] linkedActions;

//        public SimpleTask[] GetNextStepsAfterSuccess()
//        {
//            return nextStepsAfterSuccess;
//        }

//        public string GetDescription()
//        {
//            return stepDescription;
//        }

//        public SimpleTask[] GetNextStepsAfterFail()
//        {
//            return nextStepsAfterFail;
//        }


//        void LinkActions()
//        {
//            foreach(TaskAction action in linkedActions)
//            {
//                action.LinkTask(this);
//            }
//        }

//        void UnlinkActions()
//        {
//            foreach (TaskAction action in linkedActions)
//            {
//                action.RemoveTask(this);
//            }
//        }

//        public void SetActiveCourse(Course newCourse)
//        {
//            course = newCourse;
//        }

//        public void ResetActiveCourse()
//        {
//            course = null;
//        }


//        public bool ReturnDoneState()
//        {
//            return isDone;
//        }

//        public virtual void Fail()
//        {
//            SetDone();
//            _failed.Invoke();
//        }

//        public virtual void Proceed()
//        {
//            SetDone();
//            _proceeded.Invoke();
//        }

//        public void SetDone()
//        {
//            isDone = true;
//            _done.Invoke();
//        }

//        public void SetUndone()
//        {
//            isDone = false;
//            _unDone.Invoke();
//        }

//        public virtual void Activate()
//        {
//            Debug.Log(gameObject.name);
//            Interactable = true;
//            LinkActions();
//            SetUndone();
//            _activated.Invoke();
//        }


//        public virtual void Deactivate()
//        {
//            UnlinkActions();
//            Interactable = false;
//            _deactivated.Invoke();
//        }

//    }
//}

