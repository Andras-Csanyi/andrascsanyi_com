namespace Exercises.Logic.Repository.Configuration;

using Exercises.Logic.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SectionConfiguration : IEntityTypeConfiguration<SectionEntity>
{
    public void Configure(EntityTypeBuilder<SectionEntity> builder)
    {
        builder.ToTable("sections");
        builder.HasKey(topic => topic.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.ChapterId).HasColumnName("chapter_id");
        builder.Property(p => p.Title).HasColumnName("title");
        builder.Property(p => p.SectionNumber).HasColumnName("section_number");
        builder.Property(p => p.PageStart).HasColumnName("page_start");
        builder.Property(p => p.PageExercisesStart).HasColumnName("page_exercises_start");
        builder.Property(p => p.ConceptQuestionsIntervalStart).HasColumnName("concept_questions_interval_start");
        builder.Property(p => p.ConceptQuestionsIntervalEnd).HasColumnName("concept_questions_interval_end");
        builder.Property(p => p.SkillQuestionsIntervalStart).HasColumnName("skill_questions_interval_start");
        builder.Property(p => p.SkillQuestionsIntervalEnd).HasColumnName("skill_questions_interval_end");
        builder.Property(p => p.ApplicationQuestionsIntervalStart).HasColumnName("application_questions_interval_start");
        builder.Property(p => p.ApplicationQuestionsIntervalEnd).HasColumnName("application_questions_interval_end");
        builder.Property(p => p.DiscussionQuestionsIntervalStart).HasColumnName("discussion_questions_interval_start");
        builder.Property(p => p.DiscussionQuestionsIntervalEnd).HasColumnName("discussion_questions_interval_end");
        builder.Property(p => p.PageEnd).HasColumnName("page_end");

        builder
            .HasMany(many => many.Exercises)
            .WithOne(one => one.Section)
            .HasForeignKey(foreignKey => foreignKey.SectionId);
    }
}