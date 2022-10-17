using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Royale.Tests;

public class CardTests
{
    IWebDriver driver;

    [SetUp]
    public void BeforeEach()
    {
        driver=new ChromeDriver(Path.GetFullPath(@"../../../../"+"_drivers"));
    }

    [TearDown]
    public void AfterEach()
    {
        driver.Quit();
    }

    [Test]
    public void IceSpiritIsOnCardsPage()
    {
        driver.Url="https://statsroyale.com";
        Thread.Sleep(10000);
        driver.FindElement(By.CssSelector("button[title='Accept']")).Click();
        driver.Manage().Window.Maximize();
        //Thread.Sleep(5000);
        driver.FindElement(By.CssSelector("a[href='/cards']")).Click();
        var iceSpirit=driver.FindElement(By.CssSelector("a[href*='Ice+Spirit']"));
        Assert.That(iceSpirit.Displayed);
    }

    [Test]
    public void IceSpiritDetailPageIsCorrect()
    {
        driver.Url="https://statsroyale.com";
        driver.FindElement(By.CssSelector("a[href='/cards']")).Click();
        driver.FindElement(By.CssSelector("a[href*='Ice+Spirit']")).Click();
        var cardName=driver.FindElement(By.CssSelector(".card__cardName")).Text;
        var cardTypes=driver.FindElement(By.CssSelector("..card__rarity")).Text.Split(",");
        var cardRarity=driver.FindElement(By.CssSelector(".card__common")).Text;

        Assert.AreEqual("Ice Spirit",cardName);
        Assert.AreEqual("Troop",cardTypes[0]);
        Assert.AreEqual("Arena 8",cardTypes[1]);
        Assert.AreEqual("Common",cardRarity);
    }
}