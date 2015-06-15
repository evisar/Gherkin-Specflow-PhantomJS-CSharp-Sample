using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Seavus.Framework.SeleniumPageAutomation.Configuration
{
    public class ElementConfiguration: ConfigurationElement
    {
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

        [ConfigurationProperty("by", DefaultValue=FindBy.Name)]
        public FindBy FindBy
        {
            get
            {
                return (FindBy)this["by"];
            }
            set
            {
                this["by"] = value;
            }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
    }
}
