using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;

namespace Seavus.Framework.SeleniumPageAutomation.Configuration
{
    public class PageObjectConfiguration: ConfigurationElement
    {
        protected internal Dictionary<string, object> Values { get; set; }

        public PageObjectConfiguration()
        {
            this.Values = new Dictionary<string, object>();
        }

        [ConfigurationProperty("name", IsKey= true)]
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

        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("type", IsRequired=true)]
        public Type Type
        {
            get
            {
                return (Type)this["type"];
            }
            set
            {
                this["type"] = value;
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

        [ConfigurationProperty("Elements", IsDefaultCollection=true)]
        [ConfigurationCollection(typeof(ElementConfiguration), AddItemName="Element")]
        public ElementConfigurationCollection Elements
        {
            get
            {
                return (ElementConfigurationCollection)this["Elements"];
            }
            set
            {
                this["Elements"] = value;
            }
        }


        [ConfigurationProperty("Actions", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ActionConfiguration), AddItemName = "Action")]
        public ActionConfigurationCollection Actions
        {
            get
            {
                return (ActionConfigurationCollection)this["Actions"];
            }
            set
            {
                this["Actions"] = value;
            }
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader)
        {
            try
            {
                object destinationValue = null;
                PropertyInfo pi = Type.GetProperty(elementName);
                if (pi.PropertyType.IsPrimitive || pi.PropertyType == typeof(string))
                {
                    string value = reader.ReadElementContentAsString();
                    destinationValue = Convert.ChangeType(value, pi.PropertyType);
                }
                else
                {
                    XmlSerializer ser = new XmlSerializer(pi.PropertyType);
                    destinationValue = ser.Deserialize(reader);
                }
                this.Values.Add(elementName, destinationValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
