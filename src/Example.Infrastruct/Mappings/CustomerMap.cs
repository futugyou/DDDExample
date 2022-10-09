namespace Example.Infrastruct;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Name)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();
        builder.OwnsOne(p => p.Address);

        builder.Ignore(b => b.DomainEvents);
    }
}
