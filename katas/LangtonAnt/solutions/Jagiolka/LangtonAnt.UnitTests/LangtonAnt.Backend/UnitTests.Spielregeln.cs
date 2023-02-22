using LangtonAnt.Backend;

namespace LangtonAnt.UnitTests
{
  public class UnitTestsSpielregeln
  {
    // Ameise blickt nach Norden
    [Fact]
    public void GetNeuesSpielfeld_AmeiseBlicktNachNorden_FeldIstWeiss()
    {
      // Arrange
      var spiel = new Spielregeln();      
      spiel.AmeiseSpielfeld = new Spielfeld(1, 1) { AmeisenBlickrichtung = Blickrichtung.Norden, SpielfeldFarbe = SpielfeldFarbe.Weiﬂ };
      var expectedSpielfeld = new Spielfeld(2, 1) { AmeisenBlickrichtung = Blickrichtung.Osten, SpielfeldFarbe = SpielfeldFarbe.Weiﬂ };

      // Act
      var result = spiel.GetNeuesSpielfeld(spiel.AmeiseSpielfeld);

      // Assert
      Assert.Equal(expectedSpielfeld.PositionX, result.PositionX);
      Assert.Equal(expectedSpielfeld.PositionY, result.PositionY);
      Assert.Equal(expectedSpielfeld.AmeisenBlickrichtung, result.AmeisenBlickrichtung);
    }
    [Fact]
    public void GetNeuesSpielfeld_AmeiseBlicktNachNorden_FeldIstSchwarz()
    {
      // Arrange
      var spiel = new Spielregeln();
      spiel.AmeiseSpielfeld = new Spielfeld(1, 1) { AmeisenBlickrichtung = Blickrichtung.Norden, SpielfeldFarbe = SpielfeldFarbe.Schwarz };
      var expectedSpielfeld = new Spielfeld(0, 1) { AmeisenBlickrichtung = Blickrichtung.Westen, SpielfeldFarbe = SpielfeldFarbe.Weiﬂ };

      // Act
      var result = spiel.GetNeuesSpielfeld(spiel.AmeiseSpielfeld);

      // Assert
      Assert.Equal(expectedSpielfeld.PositionX, result.PositionX);
      Assert.Equal(expectedSpielfeld.PositionY, result.PositionY);
      Assert.Equal(expectedSpielfeld.AmeisenBlickrichtung, result.AmeisenBlickrichtung);
    }

    // Ameise blickt nach Osten
    [Fact]
    public void GetNeuesSpielfeld_AmeiseBlicktNachOsten_FeldIstWeiss()
    {
      // Arrange
      var spiel = new Spielregeln();
      spiel.AmeiseSpielfeld = new Spielfeld(1, 1) { AmeisenBlickrichtung = Blickrichtung.Osten, SpielfeldFarbe = SpielfeldFarbe.Weiﬂ };
      var expectedSpielfeld = new Spielfeld(1, 2) { AmeisenBlickrichtung = Blickrichtung.S¸den, SpielfeldFarbe = SpielfeldFarbe.Weiﬂ };

      // Act
      var result = spiel.GetNeuesSpielfeld(spiel.AmeiseSpielfeld);

      // Assert
      Assert.Equal(expectedSpielfeld.PositionX, result.PositionX);
      Assert.Equal(expectedSpielfeld.PositionY, result.PositionY);
      Assert.Equal(expectedSpielfeld.AmeisenBlickrichtung, result.AmeisenBlickrichtung);
    }
    [Fact]
    public void GetNeuesSpielfeld_AmeiseBlicktNachOsten_FeldIstSchwarz()
    {
      // Arrange
      var spiel = new Spielregeln();
      spiel.AmeiseSpielfeld = new Spielfeld(1, 1) { AmeisenBlickrichtung = Blickrichtung.Norden, SpielfeldFarbe = SpielfeldFarbe.Schwarz };
      var expectedSpielfeld = new Spielfeld(0, 1) { AmeisenBlickrichtung = Blickrichtung.Westen, SpielfeldFarbe = SpielfeldFarbe.Weiﬂ };

      // Act
      var result = spiel.GetNeuesSpielfeld(spiel.AmeiseSpielfeld);

      // Assert
      Assert.Equal(expectedSpielfeld.PositionX, result.PositionX);
      Assert.Equal(expectedSpielfeld.PositionY, result.PositionY);
      Assert.Equal(expectedSpielfeld.AmeisenBlickrichtung, result.AmeisenBlickrichtung);
    }
  }
}