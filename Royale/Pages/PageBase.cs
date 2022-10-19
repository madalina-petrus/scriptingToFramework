using OpenQA.Selenium;

namespace Royale.Pages
{
    public abstract class PageBase
    {
        public readonly Header Header;

        public PageBase(IWebDriver driver)
        {
            Header = new Header(driver);
        }

    }

}