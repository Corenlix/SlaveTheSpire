namespace Infrastructure.Progress
{
    public interface IProgressService
    {
        void AddClient(IProgressClient progressClient);
        void Save();
        void Load();
    }
}