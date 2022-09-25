namespace Example.Infrastruct.Data;

public class StoredEventMap : IEntityTypeConfiguration<StoredEvent>
{
    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        builder.Property(a => a.Timestamp).HasColumnName("CreateDate");
        builder.Property(a => a.MessageType).HasColumnName("Action").HasColumnType("varchar(100)");
    }
}
