namespace LangtonAnt.backend;
/// <summary>
/// Die Langton Ameise
/// Hilfsklasse für File-IO
/// Projekt: Langton's Ant Assessment
/// Autor: Petar Peev i.A. von Oliver Arentzen, advastore SE
/// </summary>
internal static class FileHelper
{
    /// <summary>
    /// Lädt den Inhalt einer Textdatei
    /// </summary>
    /// <param name="filePath">Path zu der Datei</param>
    /// <returns></returns>
    public static async Task<string> ReadTextFile(string filePath)
    {
        using Stream fileStream = System.IO.File.OpenRead(filePath);
        using StreamReader reader = new StreamReader(fileStream);
        return await reader.ReadToEndAsync();
    }
    /// <summary>
    /// Speichert ein Text-Konten als Text-Datei
    /// </summary>
    /// <param name="filePath">Path zu der Datei</param>
    /// <param name="content">Text-Kontent</param>
    /// <returns></returns>
    public static async Task WriteTextFile(string filePath, string content)
    {
        using Stream fileStream = System.IO.File.OpenWrite(filePath);
        using StreamWriter writer = new StreamWriter(fileStream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();
    }
    /// <summary>
    /// Check ob eine Datei existiert
    /// </summary>
    /// <param name="filePath">Path zu der Datei</param>
    /// <returns>Treu fals die Datei existiert</returns>
    public static bool FileExists(string filePath)
    {
        return System.IO.File.Exists(filePath);
    }
}

