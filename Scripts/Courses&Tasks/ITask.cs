namespace course
{
    public interface ITask
    {
        //public delegate void ITask();
        //public event ITask onProceeded;
        //public event ITask onFailed;
        //public event ITask onDone;

        // Make task interactable
        public void ActivateStep();
        // Make task
        public void DeactivateStep();
        public void FailStep();

        public void EndStep();
        public void CompleteStep();

    }
}

