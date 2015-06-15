using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using OpenQA.Selenium;
using System.ComponentModel;
using System.Diagnostics;

namespace Seavus.Framework.SeleniumPageAutomation.Configuration
{
    public class ApplicationObjectConfiguration: ConfigurationSection
    {
        #region Singleton Instance

        readonly static string SECTION_NAME = "Application";

        static readonly object _sync = new object();
        static volatile ApplicationObjectConfiguration _instance;

        public static ApplicationObjectConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        if (_instance == null)
                        {
                            _instance = (ApplicationObjectConfiguration)ConfigurationManager.GetSection(SECTION_NAME);
                        }
                    }
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        #endregion

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("browser")]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type BrowserType
        {
            get
            {
                return (Type)this["browser"];
            }
            set
            {
                this["browser"] = value;
            }
        }

        [ConfigurationProperty("url")]
        public Uri Url
        {
            get
            {
                return (Uri)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(PageObjectConfiguration), AddItemName="Page")]
        public PageObjectConfigurationCollection Pages
        {
            get
            {
                return (PageObjectConfigurationCollection)this[""];
            }
            set
            {
                this[""] = value;
            }
        }
    }
}
