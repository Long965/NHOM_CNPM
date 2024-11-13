using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutomationTest
{
    public class TestsBase
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
        [Test]
        public void TestUserRegistration()
        {
            driver.Navigate().GoToUrl("URL của trang đăng ký");

            // Nhập thông tin vào các trường
            driver.FindElement(By.Id("username")).SendKeys("testuser");
            driver.FindElement(By.Id("email")).SendKeys("test@example.com");
            driver.FindElement(By.Id("password")).SendKeys("Password123!");
            driver.FindElement(By.Id("confirm_password")).SendKeys("Password123!");

            // Chọn vai trò
            driver.FindElement(By.Id("saler")).Click();

            // Nhấn nút đăng ký
            driver.FindElement(By.CssSelector(".butt")).Click();

            // Kiểm tra kết quả
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var message = driver.FindElement(By.Id("registerMessage")).Text;
            Assert.AreEqual("Đăng ký thành công!", message);
        }




        [TearDown]
        public void TearDown()
        {
            // Đóng trình duyệt sau mỗi test
            driver?.Dispose();
            driver?.Quit();
        }
    }
}