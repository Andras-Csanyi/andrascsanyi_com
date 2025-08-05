namespace Exercises.Logic.Repository;

using Exercises.Logic.Repository.Configuration;
using Exercises.Logic.Repository.Models;
using Microsoft.EntityFrameworkCore;

public class ExercisesContext : DbContext
{
    public ExercisesContext(DbContextOptions options) : base(options)
    {
    }

    protected ExercisesContext()
    {
    }

    public DbSet<TopicEntity> Topics { get; set; }
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<ChapterEntity> Chapters { get; set; }
    public DbSet<SectionEntity> Sections { get; set; }
    public DbSet<ExerciseEntity> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TopicConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new ChapterConfiguration());
        modelBuilder.ApplyConfiguration(new SectionConfiguration());
    }
}