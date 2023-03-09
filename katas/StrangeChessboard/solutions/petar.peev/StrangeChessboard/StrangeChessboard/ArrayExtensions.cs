namespace StrangeChessboard.StrangeChessboard;
/// <summary>
/// Helper-Klasse für das Ermitteln von Gerade und Ungerade Items einer Liste
/// </summary>
internal static class ArrayExtensions
{
    // Ungerade Items
    internal static int[] GetOddItems(this int[] original)
    {
        return original.Where((iten, index) => index % 2 != 0).ToArray();
    }
    // Gerade Items
    internal static int[] GetRegularItems(this int[] original)
    {
        return original.Where((iten, index) => index % 2 == 0).ToArray();
    }
}
