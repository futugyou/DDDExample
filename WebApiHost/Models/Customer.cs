using System;

namespace WebApiHost.Models
{
    /// <summary>
    /// Customer 领域对象
    /// </summary>
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}