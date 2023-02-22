namespace LangtonAnt.Backend
{
  public class Spielfeld
  {
    public SpielfeldFarbe SpielfeldFarbe { get; set; }
    public Blickrichtung AmeisenBlickrichtung { get; set; } // default = None; wenn die Ameise sich auf diesem Feld befindet wird einfach nur die Blickrichtung angegeben
    public int PositionX;
    public int PositionY;

    public Spielfeld(int x, int y, Blickrichtung ameisenBlickrichtung = Blickrichtung.None)
    {
      this.SpielfeldFarbe = SpielfeldFarbe.Weiß;
      this.PositionX = x;
      this.PositionY = y;
      this.AmeisenBlickrichtung = ameisenBlickrichtung;
    }

    public override string ToString()
    {
      string ameise = "";
      switch (AmeisenBlickrichtung)
      {
        case Blickrichtung.Norden:
          ameise = "n";
          break;
        case Blickrichtung.Süden:
          ameise = "s";
          break;
        case Blickrichtung.Westen:
          ameise = "w";
          break;
        case Blickrichtung.Osten:
          ameise = "o";
          break;
      }

      string farbe = SpielfeldFarbe == SpielfeldFarbe.Weiß ? "w" : "s";

      return ameise + farbe;
    }
  }
}
