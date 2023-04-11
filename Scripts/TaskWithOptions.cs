using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace course
{

//to do 
//* revert to single script with choosing
// interaction through enum, probably there won't be need for
// many additional inherited task types

//* perhaps add state behaviour to prevent input or smth like this
    public class TaskWithOptions : AbstractTask, I_InteractableInput, ITriggerZone
    {
        // if it is necessary for player to stay inside trigger area - input will be blocked when outside or smth like dat
        public bool RequiresTrigger { get; }
        // if true - unblocks input
        public bool Triggered { get; set; }
        // structure to separate input with correct or incorrect options
        [Serializable]
        public struct TaskInputOption
        {
            public KeyCode key;
            public bool isBindedActionCorrect;
            public string description;
        }

        [SerializeField] TaskInputOption[] options;

        // events to link scene interactions
        public UnityEvent triggeredEvent, untriggeredEvent;
        //inherited crap
        public event ITriggerZone.ITriggerZone onEnter;
        public event ITriggerZone.ITriggerZone onExit;

        #region main funcs
        // for interactions with custom conditions checks
        public void HandleCondition(bool state)
        {
            if (state) Proceed(); else Fail();
        }

        public void HandleCondition()
        {
            Proceed();
        }
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

        public string FormAvailableOptionsDescriptions()
        {
            string temp = string.Empty;
            foreach(TaskInputOption o in options)
            {
                temp += o.key.ToString() + " - " + o.description + System.Environment.NewLine;
            }
            return temp;
        }

        void InitializeEvents()
        {
            onProceeded += proceededEvent.Invoke;
            onFailed += failedEvent.Invoke;
            onEnter += triggeredEvent.Invoke;
            onExit += untriggeredEvent.Invoke;
        }

        void InitializeAwakeState()
        {
            if (!RequiresTrigger)
                Triggered = true;
        }

        public TaskWithOptions[] GetNextValidSteps()
        {
            return tasksAfterValid;
        }

        public TaskWithOptions[] GetNextFailSteps()
        {
            return tasksAfterFail;
        }

        void UnsubscribeEvents()
        {
            onProceeded -= proceededEvent.Invoke;
            onFailed -= failedEvent.Invoke;
            onEnter -= triggeredEvent.Invoke;
            onExit -= untriggeredEvent.Invoke;
        }
        #endregion

        #region trigger funcs
        private void OnTriggerEnter(Collider other)
        {
            HandleEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            HandleExit();
        }

        void SetTriggeredState(bool state)
        {
            if (!RequiresTrigger) return;
            Triggered = state;
        }

        public void HandleEnter()
        {
            SetTriggeredState(true);
        }

        public void HandleExit()
        {
            SetTriggeredState(false);
        }

        public void HandleStay()
        {
            throw new NotImplementedException();
        }
        #endregion
        // Start is called before the first frame update
        #region monobehaviour funcs
        void Start()
        {
            InitializeEvents();
            InitializeAwakeState();
        }

        private void OnDisable()
        {
            Deactivate();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void Update()
        {
            HandleInput();
        }
        #endregion

        #region Input funcs
        // Check if selected option is valid in current task context
        void HandleKey(TaskInputOption opt)
        {
            if(opt.isBindedActionCorrect)
            {
                Proceed();
            }
            else
            {
                Fail();
            }
        }

        // Handle which option is choosed based on button pressed (may be remade to be more universal)
        public void HandleInput()
        {
            if (!Interactable || !Triggered) return;

            foreach(TaskInputOption o in options)
            {
                if (Input.GetKeyDown(o.key))
                {
                    HandleKey(o);
                }
            }
        }
        #endregion
    }
}

