using Example.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Events.Customer
{
    public class CustomerRegisterEvent : Event
    {
        public CustomerRegisterEvent(Guid id, string name, string email, DateTime brithDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = brithDate;
        }
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
    }
}
