namespace v3lab;

public class BaseTest
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
    public void EmptyTest()
    {
        BeforeTest();

        Assert.Empty(CatalogService.getCatalog());
    }

    [Fact]
    public void OneTrackTest()
    {
        BeforeTest();

        CatalogService.AddComposition("polina", "rockstar");
        Assert.Single(CatalogService.getCatalog());
    }

    [Fact]
    public void SearchTrackTest()
    {
        BeforeTest();

        LoadTrackTest();

        Assert.Empty(CatalogService.SearchComposition("OG buda - Карма"));
        Assert.Single(CatalogService.SearchComposition("Алла Пугачёва - Айсберг"));
        Assert.Single(CatalogService.SearchComposition("Шура - Ты не верь слезам"));
    }

    [Fact]
    public void DelTrackTest()
    {
        BeforeTest();

        LoadTrackTest();
        Assert.Equal(5, CatalogService.getCatalog().Count);
        
        CatalogService.DelComposition("Алла Пугачёва - Айсберг");
        Assert.Equal(4, CatalogService.getCatalog().Count);
    }
}