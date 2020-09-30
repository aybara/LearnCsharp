using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Xml;

namespace MyClassLibrary
{
    public static class SecureSecrets
    {
        private static readonly string FilePath = ConfigurationManager.AppSettings.Get("MySecretsFilePath");

        public static string GetSecret(string secretName)
        {
            if (File.Exists(FilePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(FilePath);

                IEnumerator secrets = document.GetElementsByTagName("secret").GetEnumerator();
                while (secrets.MoveNext())
                    if (((XmlElement)secrets.Current).Attributes["name"].Value == secretName)
                        return ((XmlElement)secrets.Current).Attributes["value"].Value;
            }
            return string.Empty;
        }

        public static void SetSecret(string secretName, string secretValue)
        {
            if (File.Exists(FilePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(FilePath);

                IEnumerator secrets = document.GetElementsByTagName("secret").GetEnumerator();
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
}
