using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;

namespace appium;


public class SelectProductMDA
{

    public static string SAUCE_USERNAME = Environment.GetEnvironmentVariable("SAUCE_USERNAME");

    public static string SAUCE_ACCESS_KEY = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY");

    public Uri URI = new Uri($"https://{SAUCE_USERNAME}:{SAUCE_ACCESS_KEY}@ondemand.us-west-1.saucelabs.com:443/wd/hub");
    public AndroidDriver<AndroidElement> driver { get; set; }

    [SetUp]
    public void MobileBaseSetup()
    {
        var options = new AppiumOptions();
        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
        options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
        options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Samsung Galaxy S9 FHD GoogleAPI Emulator");
        options.AddAdditionalCapability(MobileCapabilityType.App, "storage:filename=mda-2.0.0-21.apk");
        options.AddAdditionalCapability("appPackage", "com.saucelabs.mydemoapp.android");
        options.AddAdditionalCapability("appActivity", "com.saucelabs.mydemoapp.android.view.activities.SplashActivity");
        options.AddAdditionalCapability("newCommandTimeout", 30);

        driver = new AndroidDriver<AndroidElement>(remoteAddress: URI, driverOptions: options, commandTimeout: TimeSpan.FromSeconds(120));
    }

    [TearDown]
    public void TearDown()
    {
        if (driver == null) return;
        driver.Quit();
    }

    [TestCase]
    public void SelecionarProdutoMDA()
    {
        Assert.That(driver.FindElement(MobileBy.AccessibilityId("App logo and name")).Displayed, Is.True);

        driver.FindElement(MobileBy.AccessibilityId("Sauce Labs Backpack")).Click();

        String tituloProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/productTV")).Text;
        Assert.That(tituloProduto, Is.EqualTo("Sauce Labs Backpack"));

        String precoProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/priceTV")).Text;
        Assert.That(precoProduto, Is.EqualTo("$ 29.99"));

        TouchAction actionOne = new TouchAction(driver);
        actionOne.Press(500, 1400);
        actionOne.MoveTo(500, 700);
        actionOne.Release();
        actionOne.Perform();

        driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/cartBt")).Click();

        String numeroCarrinho = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/cartTV")).Text;
        Assert.That(numeroCarrinho, Is.EqualTo("1"));

    }


}