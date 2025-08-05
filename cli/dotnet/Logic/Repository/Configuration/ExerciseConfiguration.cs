namespace Exercises.Logic.Repository.Configuration;

using Exercises.Logic.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExerciseConfiguration : IEntityTypeConfiguration<ExerciseEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.ToTable("exercises");
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.IdInTheBook).HasColumnName("id_in_the_book");
        builder.Property(p => p.SectionId).HasColumnName("section_id");
        builder.Property(p => p.SectionIdInThebook).HasColumnName("section_id_in_the_book");
        builder.Property(p => p.ChapterId).HasColumnName("chapter_id");
        builder.Property(p => p.ChapterIdInTheBook).HasColumnName("chapter_id_in_the_book");
        builder.Property(p => p.BookId).HasColumnName("book_id");
        builder.Property(p => p.TopicId).HasColumnName("topic_id");
        builder.Property(p => p.ExerciseType).HasColumnName("exercise_type")
            .HasConversion<string>();
    }
}