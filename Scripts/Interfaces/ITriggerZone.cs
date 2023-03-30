namespace course
{
    public interface ITriggerZone
    {
        public delegate void ITriggerZone();
        public event ITriggerZone onEnter;
        public event ITriggerZone onExit;

        // Start is called before the first frame update
        public void HandleEnter();

        public void HandleExit();

        public void HandleStay();
    }
}

