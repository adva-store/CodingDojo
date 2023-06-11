
namespace LangtonAntFrontend.Classes
{
    public struct LangtonTile
    {
        public bool IsBlack = false;
        public int PosX = 0;
        public int PosY = 0;

        public LangtonTile(int x, int y, bool isBlack = false)
        {
            PosX = x;
            PosY = y;
            IsBlack = isBlack;
        }
    }
}
