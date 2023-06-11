using LangtonAntFrontend.Classes;

namespace LangtonAntFrontend.Classes
{
    public class InputFromJson
    {
        public int Width;
        public List<Ant> AntPosistions = new List<Ant>();
        public List<LangtonTile> TilesToChange = new List<LangtonTile>();
    }
}
