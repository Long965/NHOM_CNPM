using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
namespace AutomationTest_Selenium
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Khởi tạo ChromeDriver
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            // Đóng trình duyệt sau mỗi test
            driver.Quit();
        }
    }
}