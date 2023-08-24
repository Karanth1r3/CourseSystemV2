
namespace course
{
    public interface IEnvironmentChanger
    {
        // Start is called before the first frame update
        public void DoCommand<T>();

        public void Undo();
    }
}

