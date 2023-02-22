using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangtonAnt.Backend
{
  public class Spielregeln
  {
    public Spielfeld AmeiseSpielfeld { get; set; }
    public Spielfeld[,] Spielbrett { get; private set; }
    public int SpielbrettGröße { get; private set; }
    public List<string> Spielablauf = new List<string>();

    public Spielregeln()
    {
      this.NeuesSpielBrett(3);
    }

    public void NeuesSpielBrett(int spielbrettGröße)
    {
      // Neues Brett generieren
      this.SpielbrettGröße = spielbrettGröße;
      this.Spielbrett = new Spielfeld[spielbrettGröße, spielbrettGröße];
      for (int x = 0; x < spielbrettGröße; x++)
      {
        for (int y = 0; y < spielbrettGröße; y++)
        {
          this.Spielbrett[x, y] = new Spielfeld(x, y);
        }
      }

      // Ameise setzen + Ameisenposition merken
      int spielbrettMitte = spielbrettGröße / 2;
      this.AmeiseSpielfeld = this.Spielbrett[spielbrettMitte, spielbrettMitte];
      this.AmeiseSpielfeld.AmeisenBlickrichtung = Blickrichtung.Norden;
      this.Spielablauf.Clear();
      this.Spielablauf.Add(SpielbrettVerlaufsLog());
    }

    public void AmeiseBewegen()
    {
      var neuesArmeisenfeld = this.GetNeuesSpielfeld(this.AmeiseSpielfeld);

      // aktualisieren
      this.AmeiseSpielfeld.AmeisenBlickrichtung = Blickrichtung.None;
      this.AmeiseSpielfeld.SpielfeldFarbe = this.WechselSpielfeldFarbe(this.AmeiseSpielfeld.SpielfeldFarbe);

      this.AmeiseSpielfeld = Spielbrett[neuesArmeisenfeld.PositionX, neuesArmeisenfeld.PositionY];
      this.AmeiseSpielfeld.AmeisenBlickrichtung = neuesArmeisenfeld.AmeisenBlickrichtung;

      this.Spielablauf.Add(SpielbrettVerlaufsLog());
    }

    public Spielfeld GetNeuesSpielfeld(Spielfeld aktuellesSpielfeld)
    {
      var feldFarbe = aktuellesSpielfeld.SpielfeldFarbe;

      int neueXPos = 0;
      int neueYPos = 0;
      Blickrichtung neueBlickrichtung = Blickrichtung.None;

      switch (aktuellesSpielfeld.AmeisenBlickrichtung)
      {
        case Blickrichtung.Norden:
          // Blick nach Norden:
          // weißes Feld    => Blickrichtung: Osten;  xKoord: +1
          // schwarzes Feld => Blickrichtung: Westen; xKoord: -1
          if (feldFarbe == SpielfeldFarbe.Weiß)
          {
            neueXPos = GetNeueInkrementePosition(aktuellesSpielfeld.PositionX);
            neueBlickrichtung = Blickrichtung.Osten;
          }
          else
          {
            neueXPos = GetNeueDekrementePosition(aktuellesSpielfeld.PositionX);
            neueBlickrichtung = Blickrichtung.Westen;
          }
          neueYPos = aktuellesSpielfeld.PositionY;
          break;

        case Blickrichtung.Süden:
          // Blick nach Süden:
          // weißes Feld    => Blickrichtung: Westen; xKoord: -1
          // schwarzes Feld => Blickrichtung: Osten;  xKoord: +1
          if (feldFarbe == SpielfeldFarbe.Weiß)
          {
            neueXPos = GetNeueDekrementePosition(aktuellesSpielfeld.PositionX);
            neueBlickrichtung = Blickrichtung.Westen;
          }
          else
          {
            neueXPos = GetNeueInkrementePosition(aktuellesSpielfeld.PositionX);
            neueBlickrichtung = Blickrichtung.Osten;
          }
          neueYPos = aktuellesSpielfeld.PositionY;

          break;
        case Blickrichtung.Westen:
          // Blick nach Westen:
          // weißes Feld    => Blickrichtung: Norden; yKoord: -1
          // schwarzes Feld => Blickrichtung: Süden;  yKoord: +1
          if (feldFarbe == SpielfeldFarbe.Weiß)
          {
            neueYPos = GetNeueDekrementePosition(aktuellesSpielfeld.PositionY);
            neueBlickrichtung = Blickrichtung.Norden;
          }
          else
          {
            neueYPos = GetNeueInkrementePosition(aktuellesSpielfeld.PositionY);
            neueBlickrichtung = Blickrichtung.Süden;
          }
          neueXPos = aktuellesSpielfeld.PositionX;
          break;


        case Blickrichtung.Osten:
          // Blick nach Osten:
          // weißes Feld    => Blickrichtung: Süden; yKoord: +1
          // schwarzes Feld => Blickrichtung: Norden;  yKoord: -1
          if (feldFarbe == SpielfeldFarbe.Weiß)
          {
            neueYPos = GetNeueInkrementePosition(aktuellesSpielfeld.PositionY);
            neueBlickrichtung = Blickrichtung.Süden;
          }
          else
          {
            neueYPos = GetNeueDekrementePosition(aktuellesSpielfeld.PositionY);
            neueBlickrichtung = Blickrichtung.Norden;
          }
          neueXPos = aktuellesSpielfeld.PositionX;
          break;
      }

      return new Spielfeld(neueXPos, neueYPos, neueBlickrichtung);
    }

    public int GetNeueInkrementePosition(int aktuellePosition)
    {
      // Überprüft den Rand und geht nicht darüber hinaus
      return aktuellePosition < this.SpielbrettGröße - 1 ? aktuellePosition + 1 : aktuellePosition;
    }

    public int GetNeueDekrementePosition(int aktuellePosition)
    {
      // Überprüft den Rand und geht nicht darüber hinaus
      return aktuellePosition > 0 ? aktuellePosition - 1 : aktuellePosition;
    }

    public SpielfeldFarbe WechselSpielfeldFarbe(SpielfeldFarbe aktuelleFarbe)
    {
      return aktuelleFarbe == SpielfeldFarbe.Schwarz ? SpielfeldFarbe.Weiß : SpielfeldFarbe.Schwarz;
    }

    public string SpielbrettVerlaufsLog()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("(");
      for (int x = 0; x < this.SpielbrettGröße; x++)
      {
        for (int y = 0; y < this.SpielbrettGröße; y++)
        {
          sb.Append(this.Spielbrett[x, y].ToString() + ",");
        }
      }
      sb.Remove(sb.Length - 1, 1); // letztes Komma entfernen
      sb.Append(")");

      return sb.ToString();
    }
  }
}