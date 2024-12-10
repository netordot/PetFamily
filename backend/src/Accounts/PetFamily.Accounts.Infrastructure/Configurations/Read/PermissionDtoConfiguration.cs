//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using PetFamily.Core.Dtos.Accounts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PetFamily.Accounts.Infrastructure.Configurations.Read
//{
//    public class PermissionDtoConfiguration : IEntityTypeConfiguration<PermissionDto>
//    {
//        public void Configure(EntityTypeBuilder<PermissionDto> builder)
//        {
//            builder.ToTable("permissions");

//            builder.Property(p => p.Code)
//                .HasColumnName("code");
//        }
//    }
//}
