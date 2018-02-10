namespace WebApplication.Models
{
    using System.Xml;
    using System.Xml.Linq;
    using System.Collections.Generic;
    using System;
    using System.Web;
    using System.Linq;
    using Dto;
    using Utils;

    public class BaseContext
    {
        private static string path = HttpContext.Current.Server.MapPath("../Models/DataBaseXML.xml");
        public Guid UserId { get; }

        public BaseContext(string Login, Guid Password)
        {
            XDocument xdoc = XDocument.Load(path);

            var user = xdoc.Element("Database").Element("Users").Elements("User").FirstOrDefault(u => u.Element("Login").Value == Login && Guid.Parse(u.Element("Password").Value) == Password);

            if (user == null)
            {
                throw new Exception("Не верный логин или пароль!");
            }

            UserId = Guid.Parse(user.Element("Id").Value);
        }

        public void DeleteComputer(Guid id)
        {
            XDocument xdoc = XDocument.Load(path);
            var computer = xdoc.Element("Database").Element("Computers").Elements("Computer").FirstOrDefault(u => Guid.Parse(u.Element("UserId").Value) == UserId && id == Guid.Parse(u.Element("Id").Value));
            if (computer != null)
            {
                computer.Remove();
                xdoc.Save(path);
            }
        }

        public BaseContext(Guid id)
        {
            XDocument xdoc = XDocument.Load(path);
            var user = xdoc.Element("Database").Element("Users").Elements("User").FirstOrDefault(u => Guid.Parse(u.Element("Id").Value) == id);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            UserId = Guid.Parse(user.Element("Id").Value);
        }

        public XElement GetComputer(Guid id)
        {
            XDocument xdoc = XDocument.Load(path);
            return xdoc.Element("Database").Element("Computers").Elements("Computer").FirstOrDefault(u => Guid.Parse(u.Element("UserId").Value) == UserId && id == Guid.Parse(u.Element("Id").Value));
        }

        /// <summary>
        /// Получить компьютеры пользователя
        /// </summary>
        /// <returns></returns>
        public List<XElement> GetComputers()
        {
            XDocument xdoc = XDocument.Load(path);
            return xdoc.Element("Database").Element("Computers").Elements("Computer").Where(u => Guid.Parse(u.Element("UserId").Value) == UserId).ToList();
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            var users = xDoc.LastChild.LastChild;
            var newElement = xDoc.CreateElement("User");

            XmlElement idElem = xDoc.CreateElement("Id");
            idElem.InnerText = Guid.NewGuid().ToString();

            XmlElement loginElem = xDoc.CreateElement("Login");
            loginElem.InnerText = user.Login;

            XmlElement passwordElem = xDoc.CreateElement("Password");
            passwordElem.InnerText = HashPassword.ConvertToMd5HashGUID(user.Password).ToString();

            newElement.AppendChild(idElem);
            newElement.AppendChild(loginElem);
            newElement.AppendChild(passwordElem);

            users.AppendChild(newElement);
            xDoc.Save(path);
        }

        public void EditComputer(Guid id, string name, string ipAddress, string macAddress)
        {
            XDocument xdoc = XDocument.Load(path);
            var computer = xdoc.Element("Database").Element("Computers").Elements("Computer").FirstOrDefault(u => Guid.Parse(u.Element("UserId").Value) == UserId && id == Guid.Parse(u.Element("Id").Value));
            if (computer != null)
            {
                computer.SetElementValue("Name", name);
                computer.SetElementValue("IpAddress", ipAddress);
                computer.SetElementValue("MacAddress", macAddress);
                xdoc.Save(path);
            }
        }

        public void AddComputer(Computer computer)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            var computers = xDoc.LastChild.FirstChild;
            var newElement = xDoc.CreateElement("Computer");

            XmlElement idElem = xDoc.CreateElement("Id");
            idElem.InnerText = Guid.NewGuid().ToString();

            XmlElement nameElem = xDoc.CreateElement("Name");
            nameElem.InnerText = computer.Name;

            XmlElement ipElem = xDoc.CreateElement("IpAddress");
            ipElem.InnerText = computer.IpAddress;

            XmlElement macElem = xDoc.CreateElement("MacAddress");
            macElem.InnerText = computer.IpAddress;

            XmlElement userIdElem = xDoc.CreateElement("UserId");
            userIdElem.InnerText = UserId.ToString();

            newElement.AppendChild(idElem);
            newElement.AppendChild(nameElem);
            newElement.AppendChild(ipElem);
            newElement.AppendChild(macElem);
            newElement.AppendChild(userIdElem);

            computers.AppendChild(newElement);
            xDoc.Save(path);
        }
        private static void Add(string id, string name, string macAddress, string ipAddress, string userid)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement comp = xDoc.CreateElement("Computer");
            XmlElement nameElem = xDoc.CreateElement("Name");
            XmlElement idElem = xDoc.CreateElement("Id");
            XmlElement macElem = xDoc.CreateElement("MacAddress");
            XmlElement ipElem = xDoc.CreateElement("IpAddress");
            XmlElement userIdElem = xDoc.CreateElement("UserId");

            XmlText nameText = xDoc.CreateTextNode(name);
            XmlText iptext = xDoc.CreateTextNode(ipAddress);
            XmlText mactext = xDoc.CreateTextNode(macAddress);
            XmlText idtext = xDoc.CreateTextNode(id);
            XmlText userIdText = xDoc.CreateTextNode(userid);

            idElem.AppendChild(idtext);
            nameElem.AppendChild(nameText);
            ipElem.AppendChild(iptext);
            macElem.AppendChild(mactext);
            userIdElem.AppendChild(userIdText);

            comp.AppendChild(nameElem);
            comp.AppendChild(idElem);
            comp.AppendChild(ipElem);
            comp.AppendChild(macElem);
            comp.AppendChild(userIdElem);

            xRoot.AppendChild(comp);
            xDoc.Save(path);
        }
    }
}