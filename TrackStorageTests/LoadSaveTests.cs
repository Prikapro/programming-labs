namespace v3lab;

public class LoadSaveTests
{
    private void BeforeTest()
    {
        CatalogService.getCatalog().Clear();
    }

    private void LoadTrackTest()
    {
        CatalogService.AddComposition("polina", "rockstar");
        CatalogService.AddComposition("Imagine Dragons", "Dream");
        CatalogService.AddComposition("Андрея Челентано", "Конфессо");
        CatalogService.AddComposition("Алла Пугачёва", "Айсберг");
        CatalogService.AddComposition("Шура", "Ты не верь слезам");
    }

    [Fact]
    public void JsonTest()
    {
        BeforeTest();
        LoadTrackTest();
        Assert.Equal(5, CatalogService.getCatalog().Count);
        CatalogService.getCatalog()
            .AddRange(CatalogService.JsonToTracks(CatalogService.TracksToJson(CatalogService.getCatalog())));
        Assert.Equal(10, CatalogService.getCatalog().Count);
    }

    [Fact]
    public void XmlTest()
    {
        BeforeTest();
        LoadTrackTest();
        Assert.Equal(5, CatalogService.getCatalog().Count);
        CatalogService.getCatalog()
            .AddRange(CatalogService.XmlToTracks(CatalogService.TracksToXml(CatalogService.getCatalog())));
        Assert.Equal(10, CatalogService.getCatalog().Count);
    }
}