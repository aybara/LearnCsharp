using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyClassLibrary
{
    public static class SecureKeys
    {
        private static string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SecureKeys/secrets.xml");

        public static string GetSecret(string secretName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(FilePath);

            var secrets = document.GetElementsByTagName("secret").GetEnumerator();
            while (secrets.MoveNext())
                if(((XmlElement)secrets.Current).Attributes["name"].Value == secretName)
                    return ((XmlElement)secrets.Current).Attributes["value"].Value;

            return string.Empty;
        }

        public static void SetSecret(string secretName, string secretValue)
        {
            XmlDocument document = new XmlDocument();
            document.Load(FilePath);

            var secrets = document.GetElementsByTagName("secret").GetEnumerator();
            while (secrets.MoveNext())
                if (((XmlElement)secrets.Current).Attributes["name"].Value == secretName)
                    return;

            XmlNode secretsNode = document.FirstChild.FirstChild;
            XmlElement newElement = document.CreateElement("secret");
            XmlAttribute name = document.CreateAttribute("name");
            name.Value = secretName;
            XmlAttribute value = document.CreateAttribute("value");
            value.Value = secretValue;
            newElement.Attributes.Append(name);
            newElement.Attributes.Append(value);
            secretsNode.AppendChild(newElement);
            document.Save(FilePath);
        }
    }
}
