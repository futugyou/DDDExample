﻿namespace Example.Domain;

/// <summary>
/// 定义领域对象 Customer
/// </summary>
public class Customer : Entity, IAggregateRoot
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

        if (name?.Length < MinNameLenght)
        {
            throw new AggregateException("name is too short");
        }

        if (name?.Length > MaxNameLenght)
        {
            throw new AggregateException("name is too long");
        }

        Name = name;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        BirthDate = birthDate;
        Address = new Address();

        AddDomainEvent(new CustomerRegisterEvent(Id, Name, Email, BirthDate));
    }
    public Address Address { get; set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
}
