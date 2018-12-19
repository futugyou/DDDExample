using System;
using Christ.Domain.Core.Models;

namespace Example.Domain.Models
{
    /// <summary>
    /// 定义领域对象 Customer
    /// </summary>
    public class Customer : Entity
    {
        protected Customer() { }
        public Customer(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Address = new Address();
        } 
        public Address Address { get; set; }     
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
    }
}
