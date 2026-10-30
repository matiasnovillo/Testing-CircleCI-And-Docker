using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FiyiStack.Areas.CMS.RoleBack.Entities;

/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.CMS.RoleBack.EntitiesConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            //RoleId
            entity.HasKey(e => e.RoleId);
            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd();

            //DateTimeCreation
            entity.Property(e => e.DateTimeCreation)
                .HasColumnType("datetime")
                .IsRequired(true);

            //DateTimeLastModification
            entity.Property(e => e.DateTimeLastModification)
                .HasColumnType("datetime")
                .IsRequired(true);

            //UserCreationId
            entity.Property(e => e.UserCreationId)
                .HasColumnType("bigint")
                .IsRequired(true);

            //UserLastModificationId
            entity.Property(e => e.UserLastModificationId)
                .HasColumnType("bigint")
                .IsRequired(true);

            //Active
            entity.Property(e => e.Active)
                .HasColumnType("tinyint")
                .IsRequired(true);

            //Name
            entity.Property(e => e.Name)
                .HasColumnType("varchar(100)")
                .IsRequired(true);

            
        }
    }
}