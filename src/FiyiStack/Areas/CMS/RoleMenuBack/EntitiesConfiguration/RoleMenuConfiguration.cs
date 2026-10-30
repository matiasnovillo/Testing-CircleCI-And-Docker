using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FiyiStack.Areas.CMS.RoleMenuBack.Entities;

/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.CMS.RoleMenuBack.EntitiesConfiguration
{
    public class RoleMenuConfiguration : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> entity)
        {
            //RoleMenuId
            entity.HasKey(e => e.RoleMenuId);
            entity.Property(e => e.RoleMenuId)
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

            //MenuId
            entity.Property(e => e.MenuId)
                .HasColumnType("bigint")
                .IsRequired(true);

            //RoleId
            entity.Property(e => e.RoleId)
                .HasColumnType("bigint")
                .IsRequired(true);

            
        }
    }
}