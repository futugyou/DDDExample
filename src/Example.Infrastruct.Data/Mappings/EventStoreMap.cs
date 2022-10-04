namespace Example.Infrastruct.Data;
public class EventStoreMap : IEntityTypeConfiguration<EventStore>
{
    public void Configure(EntityTypeBuilder<EventStore> builder)
    {
        builder.ToTable("EventStore").HasKey(e => e.Id);
    }
}
