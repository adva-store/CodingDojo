using System.Threading.Tasks;

namespace LangtonAnt
{
    public interface IFileManager
    {
        Task SaveGameData(string code);

        Task<string> LoadGameCode();
    }
}
