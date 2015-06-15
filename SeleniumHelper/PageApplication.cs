using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Configuration;
using Seavus.Framework.SeleniumPageAutomation.Configuration;
using System.Dynamic;

namespace Seavus.Framework.SeleniumPageAutomation
{
    public class PageApplication : DynamicObject
    {
        readonly static Dictionary<string, PageObject> _pages = new Dictionary<string, PageObject>();
        protected internal static string Url { get; private set; }
        protected internal static IWebDriver Driver {get; private set;}

        static PageApplication()
        {            
            ApplicationObjectConfiguration config = ApplicationObjectConfiguration.Instance;
            Url = config.Url.OriginalString;
            Driver = Activator.CreateInstance(config.BrowserType) as IWebDriver;

            foreach (PageObjectConfiguration pageConfig in config.Pages)
            {
                string url = config.Url.OriginalString + (pageConfig.Url==null ? "" : pageConfig.Url.OriginalString);
                PageObject pageObject = Activator.CreateInstance(pageConfig.Type) as PageObject;
                foreach (var kp in pageConfig.Values)
                {
                    pageConfig.Type.GetProperty(kp.Key).SetValue(pageObject, kp.Value, null);
                }
                _pages.Add(pageConfig.Name, pageObject);
            }
        }

        public PageObject this[string name]
        {
            get
            {
                if (_pages.ContainsKey(name))
                {
                    return _pages[name];
                }
                else
                {
                    return null;
                }
            }
        }

        public virtual void Quit()
        {
            Driver.Quit();
        }


        public virtual void Wait(int milliseconds)
        {
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(milliseconds));
        }
        
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (_pages.ContainsKey(binder.Name))
            {
                result = _pages[binder.Name];
                return true;
            }
            return false;
        }
    }
    

}
