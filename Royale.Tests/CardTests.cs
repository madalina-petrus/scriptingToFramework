using Framework.Models;
using Framework.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Royale.Pages;

namespace Royale.Tests;

public class CardTests
{
    IWebDriver driver;

    [SetUp]
    public void BeforeEach()
    {
        driver = new ChromeDriver(Path.GetFullPath(@"../../../../" + "_drivers"));
        driver.Url = "https://statsroyale.com";
    }

    [TearDown]
    public void AfterEach()
    {
        driver.Quit();
    }

    [Test]
    public void IceSpiritIsOnCardsPage()
    {
        //driver.FindElement(By.CssSelector("button[title='Accept']")).Click();
        driver.Manage().Window.Maximize();
        var cardsPage = new CardsPage(driver);
        var iceSpirit = cardsPage.GoTo().GetCardByName("Ice Spirit");
        Assert.That(iceSpirit.Displayed);
    }

    [Test]
    public void IceSpiritDetailPageIsCorrect()
    {
        driver.Manage().Window.Maximize();
        new CardsPage(driver).GoTo().GetCardByName("Ice Spirit").Click();
        var cardDetails = new CardDetailsPage(driver);

        var (type, arena) = cardDetails.GetCardType();
        var cardName = cardDetails.Map.CardName.Text;
        var cardRarity = cardDetails.Map.CardRarity.Text;

        Assert.AreEqual("Ice Spirit", cardName);
        Assert.AreEqual("Troop", type);
        Assert.AreEqual("Arena 8", arena);
        Assert.AreEqual("Common", cardRarity);
    }

    static string[] cardNames={"Ice Spirit","Mirror"};

    [Test,Category("cards")]
    [TestCaseSource("cardNames")]
    [Parallelizable(ParallelScope.Children)]
    public void CardDetailPageIsCorrect(string cardName)
    {
        driver.Manage().Window.Maximize();
        new CardsPage(driver).GoTo().GetCardByName(cardName).Click();
        var cardDetails = new CardDetailsPage(driver);

        var cardOnPage=cardDetails.GetBaseCard();
        var card=new InMemoryCardService().GetCardByName(cardName);

        Assert.AreEqual(card.Name, cardOnPage.Name);
        Assert.AreEqual(card.Type, cardOnPage.Type);
        Assert.AreEqual(card.Arena, cardOnPage.Arena);
        Assert.AreEqual(card.Rarity, cardOnPage.Rarity);
    }
}