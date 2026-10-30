using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FiyiStack.Areas.CMS.UserBack.Entities;

/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.CMS.UserBack.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            //UserId
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId)
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

            //Email
            entity.Property(e => e.Email)
                .HasColumnType("varchar(400)")
                .IsRequired(true);

            //Password
            entity.Property(e => e.Password)
                .HasColumnType("varchar(8000)")
                .IsRequired(true);

            //RoleId
            entity.Property(e => e.RoleId)
                .HasColumnType("bigint")
                .IsRequired(true);

            
        }
    }
}