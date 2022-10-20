using Framework.Models;
using OpenQA.Selenium;

namespace Royale.Pages
{
    public class CardDetailsPage : PageBase
    {
        public readonly CardDetailsPageMap Map;
        public CardDetailsPage(IWebDriver driver) : base(driver)
        {
            Map = new CardDetailsPageMap(driver);
        }

        public (string Type, string Arena) GetCardType()
        {
            var types = Map.CardTypes.Text.Split(",");
            return (types[0].Trim(), types[1].Trim());
        }

        public Card GetBaseCard()
        {
            var(category,arena)=GetCardType();

            return new Card
            {
                Name=Map.CardName.Text,
                Rarity=Map.CardRarity.Text.Split('\n').Last(),
                Type=category,
                Arena=arena
            };
        }

    }

    public class CardDetailsPageMap
    {
        IWebDriver _driver;
        public CardDetailsPageMap(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement CardName => _driver.FindElement(By.CssSelector(".card__cardName"));
        public IWebElement CardTypes => _driver.FindElement(By.CssSelector(".card__rarity"));
        public IWebElement CardRarity => _driver.FindElement(By.CssSelector(".card__common"));

    }

}