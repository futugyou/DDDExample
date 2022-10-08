﻿namespace Example.Domain;

/// <summary>
/// 定义领域对象 Customer
/// </summary>
public class Customer : AggregateRoot
{
    private const int MinNameLenght = 5;
    private const int MaxNameLenght = 100;

    protected Customer() { }

    public Customer(Guid id, string name, string email, DateTime birthDate)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }
        Id = id;

        CustomerNameCheck(name);
        Name = name;

        Email = email ?? throw new ArgumentNullException(nameof(email));
        BirthDate = birthDate;
        Address = new Address();

        AddDomainEvent(new CustomerRegisterEvent(Id, Name, Email, BirthDate));
    }

    private void CustomerNameCheck(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new CustomerNameCheckException(nameof(name));
        }
        if (name?.Length < MinNameLenght)
        {
            throw new CustomerNameCheckException("name is too short");
        }
        if (name?.Length > MaxNameLenght)
        {
            throw new CustomerNameCheckException("name is too long");
        }
    }

    public Address Address { get; set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }

    public void Apply(CustomerRegisterEvent ev)
    {
        Id = ev.AggregateId;
        Name = ev.Name;
        Email = ev.Email;
        BirthDate = ev.BirthDate;
    }

    public void ChangeName(string newName, long originalVersion)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new AggregateException(nameof(newName));
        }

        if (newName?.Length < MinNameLenght)
        {
            throw new AggregateException("name is too short");
        }

        if (newName?.Length > MaxNameLenght)
        {
            throw new AggregateException("name is too long");
        }

        AddDomainEvent(new CustomerChangeNameEvent(Id, newName), originalVersion);
    }

    public void Apply(CustomerChangeNameEvent ev)
    {
        Id = ev.AggregateId;
        Name = ev.Name;
    }
}
