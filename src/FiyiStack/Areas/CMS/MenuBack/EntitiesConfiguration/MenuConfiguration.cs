using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FiyiStack.Areas.CMS.MenuBack.Entities;

/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.CMS.MenuBack.EntitiesConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> entity)
        {
            //MenuId
            entity.HasKey(e => e.MenuId);
            entity.Property(e => e.MenuId)
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

            //MenuFatherId
            entity.Property(e => e.MenuFatherId)
                .HasColumnType("bigint")
                .IsRequired(true);

            //URLPath
            entity.Property(e => e.URLPath)
                .HasColumnType("varchar(2048)")
                .IsRequired(false);

            //IconURLPath
            entity.Property(e => e.IconURLPath)
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            //Order
            entity.Property(e => e.Order)
                .HasColumnType("bigint")
                .IsRequired(true);

            
        }
    }
}