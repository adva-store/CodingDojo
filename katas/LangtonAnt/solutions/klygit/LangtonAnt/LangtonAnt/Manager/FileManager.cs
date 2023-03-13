using System;
using System.IO;
using System.Threading.Tasks;

namespace LangtonAnt
{
    internal class FileManager : IFileManager
    {
        private static readonly string FILE_NAME__CODE = "code.txt";

        public async Task SaveGameData(string code)
        {
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILE_NAME__CODE);
            await Task.Run(() => File.WriteAllText(fileName, code));
        }

        public async Task<string> LoadGameCode()
        {
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILE_NAME__CODE);
            if (!File.Exists(fileName))
                return null;
            return await Task.Run(() => File.ReadAllText(fileName));
        }
    }
}
