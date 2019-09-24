using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace Killers
{
    public class LIKiller
    {
        public string ChromeDriverRelativePath = (Directory.GetCurrentDirectory().Split(new string[] { "repos" }, StringSplitOptions.None))[0] + @"repos\Killers\chromedriver_win32";
        public List<string> ApplicationLinks { get; set; } = new List<string>();
        public List<string> CompletedLinks { get; set; } = new List<string>();
        public List<string> ErrorLinks { get; set; } = new List<string>();
        public IWebDriver Driver { get; set; }



        public LIKiller()
        {
            GoogleDriveService googleService = new GoogleDriveService();
            ApplicationLinks = googleService.getEasyJobsFromLISheet();
            setupDriver();
        }


        public int[] applyToJobs()
        {
            foreach(string url in ApplicationLinks)
            {
                string result = applyToJob(url);
                if (result == "success") CompletedLinks.Add(url);
                else ErrorLinks.Add(url);
                ApplicationLinks.Remove(url);
            }
            return new int[] { CompletedLinks.Count, ErrorLinks.Count};
        }


        public string applyToJob(string url)
        {
            Driver.Navigate().GoToUrl(url);
            //TODO: Find & click the apply button
            //TODO: Enter the appropriate information into the pop up
            //TODO: Find and click the submitt button
            //TODO: Look for the success message and return the result
            return "success";
        }


        public void setupDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incognito");
            Driver = new ChromeDriver(ChromeDriverRelativePath, options);
        }
    }
}
