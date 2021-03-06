#Gehtsoft .NET Web Test Toolkit

This repository contains an open-source set of the libraries that facilitates creation of the tests for different levels of applications.

**`Gehtsoft.WebTest.Spellchecker`**

The library offers an extension for `FluentAssertions` standard
string assertions and allows to spellcheck any string. 

In case any word in the string is not correct, assertion will be raised
with the list of all words that does not spell correctly. 

Example:
```C#
//using default en-US dictionary
myTextToSpell.Should().BeSpelledCorrectly();

//using default en-US dictionary and specifying exception words 
myTextToSpell.Should().BeSpelledCorrectly(exemptions: "gehtsoft,omsk");

//using default ru-RU dictionary
myTextToSpell.Should().BeSpelledCorrectly("ru-RU");
```

The dictionaries are loaded from <https://docs.gehtsoftusa.com>. Currently the 
site contains dictionaries for en-US, de-DE, fr-FR and ru-RU. 

**`Gehtsoft.HtmlAgilityPack.FluentAssertions`**

The library offers fluent assertions and basic extensions
for `System.Text.Json` and `HtmlAgilityPack` packages and 
improves integration testing of Web application by 
direct access to web server (without browser) in order
to validate the web page content and json answers on AJAX 
requests.

**`Gehtsoft.WebView2.Uitest`**

The library provides the infrastructure to use a `WebView2` control to test
web application trough the browser. Comparing Selenium that approach offer faster 
to develop, more stable, and faster to run way to test. 

Note: Don't forget to install a `WebView2` control before using this library as
described [at Microsoft website](https://docs.microsoft.com/en-us/microsoft-edge/webview2/get-started/win32)

To use `WebView2` in your tests create an instance of `WebBrowserDriver` class.

```c#
[Fact]
public void Test()
{
    using var driver = new WebBrowserDriver();
    driver.Start();
    driver.Show(true);
    f.Navigate("https://www.google.com");
    ...
}
```

To access and manipulate the elements use the properties `ByName`, `ById` and `ByXPath`:
```c#
[Fact]
public void Test()
{
    ...
    //fill form 
    f.Navigate("https://www.google.com");

    Assert.IsNotNull(f.ByName["q"]);
    Assert.Equals("input", f.ByName["q"].TagName);

    f.ByName["q"].Value = "gehtsoft";
    f.ByName["btnK"].Click();
    //wait until request completes
    f.WaitFor(d => d.Location.StartsWith("https://www.google.com/search"), 5);
    //and 
    Assert.IsTrue(f.XPath("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").AsBoolean);
}
```

You can also make the code more user readable using **`Gehtsoft.WebView2.Uitest.FluentAssertions`** package, e.g. 

```c#
[Fact]
public void Test()
{
    ...
    f.ByName["q"]
        .Should().Exist()
        .And.BeHtmlTag("input");
    ...
    f.XPath("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").Should().BeTrue();
}
```

The complete documentation on the packages is located as [Gehtsoft's documentation website](https://docs.gehtsoftusa.com/webtest).
