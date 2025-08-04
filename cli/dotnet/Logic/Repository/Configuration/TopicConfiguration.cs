namespace Exercises.Logic.Repository.Configuration;

using Exercises.Logic.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TopicConfiguration : IEntityTypeConfiguration<TopicEntity>
{
    public void Configure(EntityTypeBuilder<TopicEntity> builder)
    {
        builder.ToTable("topics");
        builder.HasKey(topic => topic.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Name).HasColumnName("name");
        builder.Property(p => p.Reference).HasColumnName("reference");
    }
}