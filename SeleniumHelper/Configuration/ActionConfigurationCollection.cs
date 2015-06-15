using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Seavus.Framework.SeleniumPageAutomation.Configuration
{
    public class ActionConfigurationCollection: ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ActionConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ActionConfiguration).Name;
        }
    }
}
