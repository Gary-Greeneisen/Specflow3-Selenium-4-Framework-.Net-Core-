using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;
using AcceptanceTests.Common.Library;
using SeleniumExtras.WaitHelpers;

namespace AcceptanceTests.Steps
{
    [Binding]
    public class GoogleSearchExampleSteps
    {
        //create a class var
        public IWebDriver browser = null;
        public string browserType;


        [Given(@"I Open a ""(.*)"" to ""(.*)""")]
        public void GivenIOpenATo(string browserx, string page)
        {
            browserType = browserx.ToUpper();

            switch (browserType)
            {
                case "CHROME":
                    //Headless Browser Testing
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("headless");
                    //browser = new ChromeDriver(chromeOptions);

                    //Standard web driver instance
                    browser = new ChromeDriver();
                    break;

                case "FIREFOX":
                    //Headless Browser Testing
                    var options = new FirefoxOptions();
                    options.AddArguments("--headless");     //Note the --headless syntax
                    //browser = new FirefoxDriver(options);

                    //Standard web driver instance
                    // Implement FireFox using geckdriver file
                    browser = new FirefoxDriver();
                    break;

                case "EDGE":
                    //Headless Browser Testing
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments("headless");
                    //browser = new EdgeDriver(edgeOptions);

                    //Standard web driver instance
                    browser = new EdgeDriver();
                    break;

                default:
                    //Headless Browser Testing
                    var chromeOptions2 = new ChromeOptions();
                    chromeOptions2.AddArguments("headless");
                    //browser = new ChromeDriver(chromeOptions);

                    //Standard web driver instance
                    browser = new ChromeDriver();
                    break;
            }

            //Set implicit wait
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Navigate to web page
            var url = "http://" + page;
            browser.Navigate().GoToUrl(url);

        }

        [Given(@"I search for ""(.*)""")]
        public void GivenISearchFor(string searchItem)
        {
            //Find the Search text box UI Element
            IWebElement element = browser.FindElement(By.Name("q"));

            //Search
            element.SendKeys(searchItem);

            // this sends an Enter to the element
            element.SendKeys(Keys.Enter);

            //Wait for page to load
            //System.Threading.Thread.Sleep(5 * 1000); //Wait 5-sec
            new WebDriverWait(browser, TimeSpan.FromSeconds(5)).Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            //Example wait for page element is displayed using driver.FindElement
            WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(20));
            wait.Until(drv => drv.FindElement(By.Id("main")));

            //Test if main page displayed
            element = browser.FindElement(By.Id("main"));

        }

        //[When(@"I click on the first Amazon link")]
        //public void WhenIClickOnTheFirstAmazonLink()
        //[When(@"I click on (.*)")]
        //public void WhenIClickOntheLink(string linkText)
        [When(@"I click on the link ""(.*)""")]
        public void WhenIClickOnTheLink(string linkText)
        {

            //***********************************
            // For Chrome browser
            //Wait for link to be displayed
            //WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(20));
            //IWebElement myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(By.PartialLinkText(linkText)));

            //browser.FindElement(By.PartialLinkText(linkText)).Click();

            //****************************************************************************
            // Had lots of issuses with Firefox browser finding the link text
            // For Firefox browser
            //IWebElement element = browser.FindElement(By.ClassName("LC20lb DKV0Md"));
            //Locate by Amazon class name
            //IWebElement element = browser.FindElement(By.CssSelector("h3.LC20lb DKV0Md"));
            //element.Click();

            //Solution was to comment out the orginal step definition [When(@"I click on (.*)")]
            //and re-generate the feature step When I click on the link "<LinkText>"

            //Wait for link to be displayed
            WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(20));
            var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText(linkText)));

            IWebElement element = browser.FindElement(By.PartialLinkText(linkText));

            //Highligh the element
            Library.HighLightElement(browser, element);

            //This will scroll the page till the element is found	
            IJavaScriptExecutor jscript = (IJavaScriptExecutor)browser;
            jscript.ExecuteScript("arguments[0].scrollIntoView();", element);

            //************************************************************************************
            //Trying to use the selenium driver.Click() method
            //Because there are (2)-references to the Href text, the following exception displayed
            //OpenQA.Selenium.ElementClickInterceptedException
            //HResult = 0x80131500
            //Message = element click intercepted: Element
            //< a href = "https://www.amazon.com/books-used-books-textbooks/b?ie=UTF8&amp;node=283155"
            //data - ved = "2ahUKEwjWkPzcwO3zAhXZQs0KHdYIBosQFnoECBIQAQ" ping = "/url?sa=t&amp;source=web&amp;rct=j&amp;
            //url = https://www.amazon.com/books-used-books-textbooks/b%3Fie%3DUTF8%26node%3D283155&amp;
            //ved = 2ahUKEwjWkPzcwO3zAhXZQs0KHdYIBosQFnoECBIQAQ" style="border - width: 4px; border - style: solid; border - color: blue; ">...</ a > is not clickable at point(30, 11).
            //Other element would receive the click: < div style = "margin-top:-20px" class="sfbg"></div>
            //
            //Workaround is to comment out the orginal element.Click() method, use IJavaScriptExecutor 
            //to click the located Href link
            //element.Click();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)browser;
            jse.ExecuteScript("arguments[0].click()", element);
                       

        }


        [Then(@"the number of page images are (.*)")]
        public void ThenTheNumberOfPageImagesAre(int count)
        {

            //Read #images on the page
            var listImages = browser.FindElements(By.TagName("img"));

            //Close the browser, then check the results
            //browser.Close() did not close the browser
            //call quit, instead of close
            browser.Quit();

            //Check the #Page Images and Count
            if (listImages.Count != count)
            {
                throw new Exception("The Expected #Images = " + count
                                    + " The Actual #Images = " + listImages.Count);

            }

        }
    }
}
