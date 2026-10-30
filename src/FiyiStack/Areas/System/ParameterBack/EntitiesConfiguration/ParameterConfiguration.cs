using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FiyiStack.Areas.System.ParameterBack.Entities;

/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.System.ParameterBack.EntitiesConfiguration
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> entity)
        {
            //ParameterId
            entity.HasKey(e => e.ParameterId);
            entity.Property(e => e.ParameterId)
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

            //Value
            entity.Property(e => e.Value)
                .HasColumnType("varchar(8000)")
                .IsRequired(true);

            //IsPrivate
                entity.Property(e => e.IsPrivate)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                
        }
    }
}