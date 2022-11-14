using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System.Threading;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using CodeJam;

namespace BoostiseE2ETests;

[AllureNUnit]
[AllureSuite("Smoke Tests")]
public class Tests
{
    IWebDriver? driver;
    Actions actions;
    
    [SetUp]
    public void Setup()
    {
        this.driver = new EdgeDriver();
        this.driver.Url = "http://192.168.56.79:5044/";
        this.driver.Manage().Window.Maximize();

        this.actions = new Actions(driver);
    }

    [Test]
    public void FeedbackFormTest() {
        this.driver?.Navigate().GoToUrl("http://192.168.56.79:5044/about");

        Thread.Sleep(2000);

        // fill email
        IWebElement? emailField = this.driver?.FindElement(By.Id("email"));  
        emailField?.Clear();
        emailField.SendKeys("testuser@localnet.ua");

        // fill message text
        IWebElement? messageTextField = this.driver?.FindElement(By.Id("textArea"));  
        messageTextField.SendKeys(" Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in");


        // press Send Button
        IWebElement? sendButton = this.driver?.FindElement(By.XPath("//form/button"));  
        sendButton.Click();
        Thread.Sleep(500);

        // verifying current message
        IWebElement? textElement = this.driver.FindElement(By.XPath("//h2"));
        string textMessage = textElement.Text.ToUpper();

        Code.AssertState(string.Equals(textMessage, "Спасибо! Сообщение отправлено! Мы обязательно свяжемся с вами!".ToUpper()), "textMessage is not valid!");

    }

    [Test]
    public void AddItemTest() {
        this.driver?.Navigate().GoToUrl("http://192.168.56.79:5044/addadv");

        Thread.Sleep(2000);

        // fill first name and last name
        IWebElement? fnlnField = this.driver?.FindElement(By.Id("fnln"));  
        fnlnField.SendKeys("Ivan Kupala");

        // fill phone
        IWebElement? phoneNumberField = this.driver?.FindElement(By.Id("phoneNumber"));  
        phoneNumberField.SendKeys("+380963855262");

        // fill email
        IWebElement? emailField = this.driver?.FindElement(By.Id("email"));  
        emailField.SendKeys("user@localnet.ua");

        // fill text
        IWebElement? advTextField = this.driver?.FindElement(By.Id("textArea"));  
        advTextField.SendKeys("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit");

        // fill age
        IWebElement? ageField = this.driver?.FindElement(By.Id("age"));
        ageField.Click();
        ageField.SendKeys(Keys.Backspace);
        ageField.SendKeys("22");

        // fill priceUSD
        IWebElement? priceUSDField = this.driver?.FindElement(By.Id("priceUSD"));
        priceUSDField.SendKeys(Keys.Backspace);
        priceUSDField.SendKeys("100");
        priceUSDField.SendKeys(Keys.Tab);

        IWebElement? submitButton = this.driver?.FindElement(By.XPath("//form/button"));

        // scroll down to footer
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", 
                    this.driver?.FindElement(By.XPath("//footer")));
        Thread.Sleep(100);

        // save course and priceUAH values for verification

        IWebElement? courseField = this.driver?.FindElement(By.Id("course"));
        IWebElement? priceUAHField = this.driver?.FindElement(By.Id("priceUAH"));  
        float course = float.Parse(courseField.GetAttribute("value"));
        float priceUAH = float.Parse(priceUAHField.GetAttribute("value"));

        // -------------------- ASSERTS --------------------
        Code.AssertState(course == 25.12F, "Course is invalid");
        Code.AssertState(priceUAH == 2512, "priceUAH was calculated wrong"); // 

        // if everything is OK go to list page
        submitButton.Click();

        
        // -------------------- NEW PAGE ------------------
        // verivying list page
        Thread.Sleep(2000);
        IWebElement? listPageHeader = this.driver?.FindElement(By.XPath("//table"));
        // string header = listPageHeader.Text;

        // scroll down to footer
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", 
                    this.driver?.FindElement(By.XPath("//footer")));
        Thread.Sleep(1000);

        IWebElement? tableElement = this.driver?.FindElement(By.XPath("//table/tbody/tr[74]/td[1]"));
        string insertedText = tableElement.Text;


        // -------------------- ASSERTS --------------------
        Code.AssertState(!(listPageHeader is null), "We have not moved to a new page");
        Code.AssertState(string.Equals(insertedText, "Ivan Kupala"), "Item hasn't been added into a database"); 

    }

    [Test]
    public void AddItemWithWrongAgeTest() {
        this.driver?.Navigate().GoToUrl("http://192.168.56.79:5044/addadv");

        Thread.Sleep(2000);

        // fill first name and last name
        IWebElement? fnlnField = this.driver?.FindElement(By.Id("fnln"));  
        fnlnField.SendKeys("Ivan Kupala");

        // fill phone
        IWebElement? phoneNumberField = this.driver?.FindElement(By.Id("phoneNumber"));  
        phoneNumberField.SendKeys("+380963855262");

        // fill email
        IWebElement? emailField = this.driver?.FindElement(By.Id("email"));  
        emailField.SendKeys("user@localnet.ua");

        // fill text
        IWebElement? advTextField = this.driver?.FindElement(By.Id("textArea"));  
        advTextField.SendKeys("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit");

        // fill age
        IWebElement? ageField = this.driver?.FindElement(By.Id("age"));
        ageField.Click();
        ageField.SendKeys(Keys.Backspace);
        ageField.SendKeys("14");

        // fill priceUSD
        IWebElement? priceUSDField = this.driver?.FindElement(By.Id("priceUSD"));
        priceUSDField.SendKeys(Keys.Backspace);
        priceUSDField.SendKeys("100");
        priceUSDField.SendKeys(Keys.Tab);

        IWebElement? submitButton = this.driver?.FindElement(By.XPath("//form/button"));

        // scroll down to footer
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", 
                    this.driver?.FindElement(By.XPath("//footer")));
        Thread.Sleep(100);

        submitButton.Click();
        Thread.Sleep(100);

        IWebElement? alertBlock = this.driver?.FindElement(By.XPath("//div[@class='alert alert-danger']"));

        // -------------------- ASSERTS --------------------
        Code.AssertState(!(alertBlock is null), "Age field is not valid"); 

    }

    [TearDown]

    public void endTests() {
        
        Thread.Sleep(2000);
        this.driver?.Close();
    }
}