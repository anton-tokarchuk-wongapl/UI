using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;

namespace Tests
{
    public class Tests
    {
        private IWebDriver driver  = new ChromeDriver(System.Environment.CurrentDirectory);     // Chrome driver
        private const string URL = "https://app.fluxday.io/users/sign_in";                      // login page
        private const string email_field = "user_email";                                        // email field
        private const string pass_filed = "user_password";                                      // password field
        private const string login_button = "#new_user > div.set > div.field-login > button";   // login button
        private const string logout = "body > div.row.ep-tracker > div.large-2.columns.pane1.show-for-large-up > ul.user-links.side-nav.sidebar-links > li:nth-child(2) > a"; // logout button

        public void Action (string button, string data)                                         // method for quick action (click button, clean field) and send data to this field
        {
            driver.FindElement(By.Id(button)).Click();
            driver.FindElement(By.Id(button)).Clear();
            driver.FindElement(By.Id(button)).SendKeys(data);
        }
        [SetUp]
        public void Setup()
        {
            driver.Navigate().GoToUrl(URL);
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(10);
        }

        [Test]
        public void Test1()
        {
            Action(email_field, "emp1@fluxday.io");             // fill emal
            Action(pass_filed, "password");                     // fill password
            driver.FindElement(By.CssSelector(login_button)).Click();   // click login button
            Assert.AreEqual("Employee 1", driver.FindElement(By.CssSelector("body > div.row.ep-tracker > div.large-2.columns.pane1.show-for-large-up > ul.user-links.side-nav.sidebar-links > li:nth-child(1) > a")).Text);
        }
        [TearDown]
        public void Close()
        {
            driver.FindElement(By.CssSelector(logout)).Click();     // logout before quit browser
            driver.Quit();
        }
    }
}