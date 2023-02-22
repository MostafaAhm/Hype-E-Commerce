using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Persistence.Configuration
{
    public class APILogHistoryConfiguartion : IEntityTypeConfiguration<APILogHistory>
    {
        public void Configure(EntityTypeBuilder<APILogHistory> builder)
        {
            builder.ToTable("APILogHistory");

            builder.Property(p => p.CreatedDate)
                   .HasColumnType("datetime");
        }
    }
}
