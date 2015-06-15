using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using Seavus.Framework.SeleniumPageAutomation.Configuration;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Seavus.Framework.SeleniumPageAutomation
{

    /// <summary>
    /// Generic Abstract Page Object Pattern with Singleton support
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PageObject<T>: PageObject
        where T: PageObject, new()
    {
        static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }


    /// <summary>
    /// Abstract Page Object pattern
    /// </summary>
    public abstract class PageObject
    {

        protected string Url { get; private set; }
        protected IWebDriver Driver {get; private set;}
        readonly Dictionary<string, Func<string, IWebElement>> _cachedQueries = new Dictionary<string,Func<string,IWebElement>>(StringComparer.InvariantCultureIgnoreCase);
        readonly Dictionary<string, Delegate> _cachedActions = new Dictionary<string, Delegate>(StringComparer.InvariantCultureIgnoreCase);

        public PageObject()
        {
            ApplicationObjectConfiguration config = ApplicationObjectConfiguration.Instance;
            PageObjectConfiguration currentPage = ApplicationObjectConfiguration.Instance.Pages.OfType<PageObjectConfiguration>().Where(p=> p.Type == this.GetType()).FirstOrDefault();
            Url = config.Url + (currentPage.Url != null ? currentPage.Url.OriginalString : "");
            Driver = Activator.CreateInstance(config.BrowserType) as IWebDriver;
            if(currentPage!=null)
            {
                foreach(ElementConfiguration ec in currentPage.Elements)
                {
                    string value = ec.Value;
                    FindBy findBy = ec.FindBy;
                    Func<string, IWebElement> finder = (str)=> FindElementBy(value, findBy);
                    _cachedQueries.Add(ec.Name, finder);
                }

                foreach (var kp in currentPage.Values)
                {
                    currentPage.Type.GetProperty(kp.Key).SetValue(this, kp.Value, null);
                }

                foreach (PropertyInfo pi in this.GetType().GetProperties())
                {
                    if (currentPage.Values.ContainsKey(pi.Name))
                    {
                        pi.SetValue(this, currentPage.Values[pi.Name], null);
                    }
                }
            }            
        }

        private IWebElement FindElementBy(string query, FindBy findBy= FindBy.Name )
        {
            //query = HttpUtility.HtmlEncode(query);
            switch(findBy)
            {
                case FindBy.Id:
                    return Driver.FindElement(By.Id(query));
                case FindBy.Name:
                    return Driver.FindElement(By.Name(query));
                case FindBy.XPath:
                    return Driver.FindElement(By.XPath(query));
                case FindBy.Href:
                    return Driver.FindElement(By.XPath(string.Format("//*[@href='{0}']", query)));
                case FindBy.Value:
                    return Driver.FindElement(By.XPath(string.Format("//*[@value='{0}']", query)));
                default:
                    throw new NotSupportedException();
            }
        }

        public virtual void Open()
        {
            Driver.Navigate().GoToUrl(Url);
        }

        public virtual void Close()
        {
            Driver.Close();
        }

        public virtual void Quit()
        {
            Driver.Quit();
        }


        protected virtual bool IsCurrent()
        {
            return Regex.IsMatch(Driver.Url, Url);
        }

        public virtual void Wait(int milliseconds)
        {
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(milliseconds));
        }

       
        public virtual IWebElement Find(string name)
        {
            IWebElement elem = null;
            if (_cachedQueries.ContainsKey(name))
            {
                elem = _cachedQueries[name](name);
            }
            return elem;
        }

        public virtual void Click(string name)
        {
            IWebElement elem = Find(name);
            elem.Click();
        }

        public virtual void SendKeys(string name, string value)
        {
            IWebElement elem = Find(name);
            elem.SendKeys(value);
        }

        public virtual void Clean(string name)
        {
            IWebElement elem = Find(name);
            elem.Clear();
        }

        public virtual string GetText(string name)
        {
            IWebElement elem = Find(name);
            return elem.GetAttribute("value");
        }
    }
}
