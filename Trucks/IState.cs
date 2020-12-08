namespace Trucks
{
    public interface IState
    {
        void ChangeDriver();
        void StartRun();
        void StartRepair();
        string ToString();
    }
}