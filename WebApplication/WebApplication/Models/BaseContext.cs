namespace WebApplication.Models
{
    using System.Xml;
    using System.Xml.Linq;
    using System.Collections.Generic;
    using System;
    using System.Web;
    using System.Linq;
    using Dto;

    public class BaseContext
    {
        private static string path = HttpContext.Current.Server.MapPath("../Models/DataBaseXML.xml");
        private int UserId;
        public BaseContext(int UserId)
        {
            XDocument xdoc = XDocument.Load(path);
            var user = xdoc.Element("Database").Element("Users").Elements("User").FirstOrDefault(u => int.Parse(u.Element("Id").Value) == UserId);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            UserId = int.Parse(user.Element("Id").Value);
        }
        public IEnumerable<XElement> GetComputers()
        {
            XDocument xdoc = XDocument.Load(path);
            return xdoc.Element("Database").Element("Computers").Elements("Computer").Where(u => int.Parse(u.Element("UserId").Value) == UserId);
        }
        public void AddComputer(Computer a)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement comp = xDoc.CreateElement("Computer");
            XmlElement name = xDoc.CreateElement("Name");
            XmlElement id = xDoc.CreateElement("Id");
            XmlElement mac = xDoc.CreateElement("MacAddress");
            XmlElement ip = xDoc.CreateElement("IpAddress");
            XmlText nameText = xDoc.CreateTextNode(a.Name);
            XmlText iptext = xDoc.CreateTextNode(a.IpAddress);
            XmlText mactext = xDoc.CreateTextNode(a.MacAddress);
            XmlText idtext = xDoc.CreateTextNode(Guid.NewGuid().ToString());
            id.AppendChild(idtext);
            name.AppendChild(nameText);
            ip.AppendChild(iptext);
            mac.AppendChild(mactext);
            comp.AppendChild(name);
            comp.AppendChild(id);
            comp.AppendChild(ip);
            comp.AppendChild(mac);
            xRoot.AppendChild(comp);
            xDoc.Save(path);
        }
        private static void Add(Guid ida, string namea, string maca, string ipa)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement comp = xDoc.CreateElement("Computer");
            XmlElement name = xDoc.CreateElement("Name");
            XmlElement id = xDoc.CreateElement("Id");
            XmlElement mac = xDoc.CreateElement("MacAddress");
            XmlElement ip = xDoc.CreateElement("IpAddress");
            XmlText nameText = xDoc.CreateTextNode(namea);
            XmlText iptext = xDoc.CreateTextNode(ipa);
            XmlText mactext = xDoc.CreateTextNode(maca);
            XmlText idtext = xDoc.CreateTextNode(ida.ToString());
            id.AppendChild(idtext);
            name.AppendChild(nameText);
            ip.AppendChild(iptext);
            mac.AppendChild(mactext);
            comp.AppendChild(name);
            comp.AppendChild(id);
            comp.AppendChild(ip);
            comp.AppendChild(mac);
            xRoot.AppendChild(comp);
            xDoc.Save(path);
        }

        public void SaveChanges(List<XElement> temp)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            xRoot.RemoveAll();
            xDoc.Save(path);
            foreach (var u in temp)
            {
                if (u.Element("Id") != null)
                    Add(Guid.Parse(u.Element("Id").Value), u.Element("Name").Value, u.Element("MacAddress").Value, u.Element("IpAddress").Value);
            }
        }
        public static void Delete(int id)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            xDoc.Save(path);
        }
    }
}