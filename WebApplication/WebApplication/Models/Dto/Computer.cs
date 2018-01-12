namespace WebApplication.Models.Dto
{
    using System;
    public class Computer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
    }
}