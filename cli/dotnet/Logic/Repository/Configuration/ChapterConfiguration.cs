namespace Exercises.Logic.Repository.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class ChapterConfiguration : IEntityTypeConfiguration<ChapterEntity>
{
    public void Configure(EntityTypeBuilder<ChapterEntity> builder)
    {
        builder.ToTable("chapters");
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.BookId).HasColumnName("book_id");
        builder.Property(p => p.Title).HasColumnName("title");
        builder.Property(p => p.Reference).HasColumnName("reference");
        builder.Property(p => p.PageStart).HasColumnName("page_start");
        builder.Property(p => p.PageEnd).HasColumnName("page_end");

        builder
            .HasMany(many => many.Sections)
            .WithOne(one => one.Chapter)
            .HasForeignKey(key => key.ChapterId);
    }
}