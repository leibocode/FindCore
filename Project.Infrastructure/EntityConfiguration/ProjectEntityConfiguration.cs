using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectEntity = Project.Domain.AggregatesModel.Project;
namespace Project.Infrastructure.EntityConfiguration
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.ToTable("projects").HasKey(p => p.Id);

        }
    }
}
