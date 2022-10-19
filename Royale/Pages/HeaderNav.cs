using OpenQA.Selenium;

namespace Royale.Pages
{
    public class Header
    {
        public readonly HeaderMap Map;
        public Header(IWebDriver driver)
        {
            Map=new HeaderMap(driver);
        }
        public void GoToCards()
        {
            Map.CardsLink.Click();
        }
    }   

    public class HeaderMap
    {
        IWebDriver _driver;
        public HeaderMap(IWebDriver driver)
        {
            _driver=driver;
        }
        public IWebElement CardsLink =>_driver.FindElement(By.CssSelector("a[href='/cards']"));

    }   

}
