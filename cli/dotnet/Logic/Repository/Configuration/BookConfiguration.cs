namespace Exercises.Logic.Repository.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.ToTable("books");
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.TopicId).HasColumnName("topic_id");
        builder.Property(p => p.Title).HasColumnName("title");
        builder.Property(p => p.Authors).HasColumnName("authors");
        builder.Property(p => p.PageStart).HasColumnName("page_start");
        builder.Property(p => p.PageEnd).HasColumnName("page_end");
        builder.Property(p => p.Reference).HasColumnName("reference");

        builder
            .HasMany(m => m.Chapters)
            .WithOne(one => one.Book)
            .HasForeignKey(key => key.BookId);
    }
}