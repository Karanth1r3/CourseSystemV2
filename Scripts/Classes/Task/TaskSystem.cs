using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace course
{
    // to do - check system structure and the way this one is going to interact 
    // with tasks before touching this
    public class TaskSystem : MonoBehaviour
    {
        [SerializeField] TaskWithOptions[] currentTasks;
        [SerializeField] TaskWithOptions currentTask;
    // Start is called before the first frame update
        public static void SwitchTask(AbstractTask[] active, AbstractTask[] inactive)
        {
            ChangeTaskArrayState(inactive, false);
            ChangeTaskArrayState(active, true);
        }

        public static void ChangeTaskArrayState(AbstractTask[] tasks, bool state)
        {
            foreach(AbstractTask at in tasks)
            {
                if (state) at.Activate(); else at.Deactivate();
            }
        }

        public void CompleteCourse()
        {

        }

        public void FailCourse()
        {

        }

        public void StartCourse()
        {

        }
    }
}


