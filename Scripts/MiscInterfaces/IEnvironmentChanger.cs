
namespace course
{
    // todo - probably will be needed for going back to previous steps in course/quest
    public interface IEnvironmentChanger
    {
        public void DoCommand<T>();

        public void Undo();
    }
}

