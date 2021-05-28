This repository contains a small experiment to check whether we can use `WebView2` control to test WEB UI application. 

The ultimate idea is to avoid running three separate processes (selenium test, web server and chrome browser) to test UI of ASP.NET application. 
If WebView2 will demonstrate its ability, then, in theory we can join all three processes in one. 

The experiment consists of a small test case that open a google search form, then searches a keyword and validates that the keyword appears in the first page of the results. 

A short step-by-step story how test was written can be found [here](https://github.com/nikolaygekht/webview2_as_web_ui_test/wiki/Step-by-step-integration-of-the-WebView2-into-tests)
