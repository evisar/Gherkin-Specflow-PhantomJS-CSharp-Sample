using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;

namespace Seavus.Framework.SeleniumPageAutomation.Configuration
{
    public class ActionConfiguration: ConfigurationElement
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

        [ConfigurationProperty("return")]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type ReturnType
        {
            get
            {
                return (Type)this["return"];
            }
            set
            {
                this["return"] = value;
            }
        }
    }
}
