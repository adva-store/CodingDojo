namespace bvJagiolka.UnitTests
{
  using bvJagiolka.Shared;

  public class UnitTest1
  {
    [Theory]
    [InlineData("4012345123456", true)]
    [InlineData("4102430015329", true)]
    [InlineData("4020753015706", true)]
    [InlineData("70278358219069", false)]
    [InlineData("8001841920573", true)]
    [InlineData("8000500003787", true)]
    [InlineData("50001593", false)]
    [InlineData("541343", false)]
    [InlineData("9002804150203", true)]
    [InlineData("9010054115002", true)]
    [InlineData("4711471147114711", false)]
    [InlineData("5060108450515", true)]
    [InlineData("5054903836766", true)]
    [InlineData("7322540790146", true)]
    [InlineData("20515713", false)]
    [InlineData("27131336", false)]
    [InlineData("20017224", false)]
    [InlineData("27264096", false)]
    [InlineData("28170280", false)]
    [InlineData("90087066", false)]
    [InlineData("4711", false)]
    [InlineData("4020753015707", false)]     // <= 13-stellig mit falscher Prüfnummer hinzugefügt
    public void Test_IsGTIN(string testBarcode, bool expectedResult)
    {
      // Arrange

      // Act

      // Assert
      Assert.Equal(expectedResult, BarcodeListManager.IsGTIN(testBarcode));
    }

    [Theory]
    [InlineData("4012345123456", false)]
    [InlineData("4102430015329", false)]
    [InlineData("4020753015706", false)]
    [InlineData("70278358219069", false)]
    [InlineData("8001841920573", false)]
    [InlineData("8000500003787", false)]
    [InlineData("50001593", true)]
    [InlineData("541343", false)]
    [InlineData("9002804150203", false)]
    [InlineData("9010054115002", false)]
    [InlineData("4711471147114711", false)]
    [InlineData("5060108450515", false)]
    [InlineData("5054903836766", false)]
    [InlineData("7322540790146", false)]
    [InlineData("20515713", true)]
    [InlineData("27131336", true)]
    [InlineData("20017224", true)]
    [InlineData("27264096", true)]
    [InlineData("28170280", true)]
    [InlineData("90087066", true)]
    [InlineData("4711", false)]
    public void Test_IsEan8(string testBarcode, bool expectedResult)
    {
      // Arrange

      // Act

      // Assert
      Assert.Equal(expectedResult, BarcodeListManager.IsEan8(testBarcode));
    }

    [Theory]
    [InlineData("4012345123456", 64)]
    public void Test_CalculateGtinPrüfsumme(string testBarcode, int expectedResult)
    {
      // Arrange

      // Act

      // Assert
      Assert.Equal(expectedResult, BarcodeListManager.CalculateGtinPrüfsumme(testBarcode));
    }

    [Theory]
    [InlineData(64, 70)]
    public void Test_CalculateGtinAufgerundetePrüfsumme(int prüfziffer, int expectedResult)
    {
      // Arrange

      // Act

      // Assert
      Assert.Equal(expectedResult, BarcodeListManager.CalculateGtinAufgerundetePrüfsumme(prüfziffer));
    }

    [Fact]
    public void Test_GetValideBarcodes()
    {
      // Arrange
      var testDataBarcodeList = TestData.GetBarcodeList_TestData();
      int expectedResultCount = 17;

      // Act
      var valideBarcodes = BarcodeListManager.GetValideBarcodes(testDataBarcodeList);

      // Assert
      Assert.Equal(expectedResultCount, valideBarcodes.Count);
    }
  }
}