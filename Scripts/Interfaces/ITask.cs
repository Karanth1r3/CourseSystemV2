namespace course
{
    public interface ITask
    {
        public delegate void ITask();
        public event ITask onProceeded;
        public event ITask onFailed;

        // Make task interactable
        public void Activate();
        // Make task
        public void Deactivate();
        public void Fail();

        public void Proceed();

    }
}

