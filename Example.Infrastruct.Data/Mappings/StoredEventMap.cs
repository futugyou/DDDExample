using Example.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Infrastruct.Data.Mappings
{
    public class StoredEventMap : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(a => a.Timestamp).HasColumnName("CreateDate");
            builder.Property(a => a.MessageType).HasColumnName("Action").HasColumnType("varchar(100)");
        }
    }
}
