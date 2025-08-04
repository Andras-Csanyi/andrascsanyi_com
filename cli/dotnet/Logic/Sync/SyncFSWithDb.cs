namespace Exercises.Logic.Sync;

using Exercises.Logic.CatalogParser.Model;
using Exercises.Logic.Repository;
using Exercises.Logic.Repository.Models;
using Microsoft.EntityFrameworkCore;

public class SyncFSWithDb
{
    public async Task Execute(StudyTree studyTree, DbContextOptions<ExercisesContext> dbContextOptions)
    {
        await SyncTopicsAsync(studyTree, dbContextOptions).ConfigureAwait(false);
        await SyncBooksAsync(studyTree, dbContextOptions).ConfigureAwait(false);
        await SyncChaptersAsync(studyTree, dbContextOptions).ConfigureAwait(false);
        await SyncSectionsAsycn(studyTree, dbContextOptions).ConfigureAwait(false);
    }

    private async Task SyncSectionsAsycn(StudyTree studyTree, DbContextOptions<ExercisesContext> dbContextOptions)
    {
        using var ctx = new ExercisesContext(dbContextOptions);
        studyTree.Topics.ForEach(async topic =>
        {
            topic.Books.ForEach(async book =>
            {
                book.Chapters.ForEach(async chapter =>
                {
                    ChapterEntity chapter_in_db = await ctx.Chapters.FirstOrDefaultAsync(c => c.Reference == chapter.Reference).ConfigureAwait(false);

                    chapter.Sections.ForEach(async section =>
                    {
                        SectionEntity? existingSectionEntity = await ctx.Sections
                        .FirstOrDefaultAsync(s => s.Title == section.Title)
                        .ConfigureAwait(false);
                        if (existingSectionEntity == null)
                        {
                            SectionEntity newSectionEntity = new()
                            {
                                Title = section.Title,
                                PageStart = section.PageStart,
                                PageExercisesStart = section.PageExercisesStart,
                                ApplicationQuestionsIntervalStart = section.ApplicationQuestionsIntervalStart,
                                ApplicationQuestionsIntervalEnd = section.ApplicationQuestionsIntervalEnd,
                                ConceptQuestionsIntervalStart = section.ConceptQuestionsIntervalStart,
                                ConceptQuestionsIntervalEnd = section.ConceptQuestionsIntervalEnd,
                                DiscussionQuestionsIntervalStart = section.DiscussionQuestionsIntervalStart,
                                DiscussionQuestionsIntervalEnd = section.DiscussionQuestionsIntervalEnd,
                                SkillQuestionsIntervalStart = section.SkillQuestionsIntervalStart,
                                SkillQuestionsIntervalEnd = section.SkillQuestionsIntervalEnd,
                                SectionNumber = section.SectionNumber,
                                PageEnd = section.PageEnd,
                                ChapterId = chapter_in_db!.Id
                            };
                            await ctx.Sections.AddAsync(newSectionEntity).ConfigureAwait(false);
                            await ctx.SaveChangesAsync().ConfigureAwait(false);
                        }
                    });
                });
            });
        });
    }

    private async Task SyncChaptersAsync(StudyTree studyTree, DbContextOptions<ExercisesContext> dbContextOptions)
    {

        using var ctx = new ExercisesContext(dbContextOptions);
        studyTree.Topics.ForEach(async topic =>
        {
            topic.Books.ForEach(async book =>
            {
                BookEntity book_in_db = await ctx.Books.FirstOrDefaultAsync(b => b.Reference == book.Reference).ConfigureAwait(false);
                book.Chapters.ForEach(async chapter =>
                {
                    ChapterEntity? existingEntity = await ctx.Chapters.FirstOrDefaultAsync(c => c.Reference == chapter.Reference).ConfigureAwait(false);
                    if (existingEntity == null)
                    {
                        ChapterEntity newEntity = new()
                        {
                            Title = chapter.Title,
                            Reference = chapter.Reference,
                            PageStart = chapter.PageStart,
                            PageEnd = chapter.PageEnd,
                            BookId = book_in_db!.Id
                        };
                        await ctx.Chapters.AddAsync(newEntity).ConfigureAwait(false);
                        await ctx.SaveChangesAsync().ConfigureAwait(false);
                    }
                });
            });
        });
    }

    private async Task SyncBooksAsync(StudyTree studyTree, DbContextOptions<ExercisesContext> dbContextOptions)
    {
        using var ctx = new ExercisesContext(dbContextOptions);
        studyTree.Topics.ForEach(async topic =>
        {
            TopicEntity topic_in_db = await ctx.Topics
            .FirstOrDefaultAsync(t => t.Name == topic.Name && t.Reference == topic.Reference)
            .ConfigureAwait(false);

            topic.Books.ForEach(async book =>
            {
                BookEntity? existingBook = await ctx.Books.FirstOrDefaultAsync(b =>
                        b.Reference == book.Reference)
                .ConfigureAwait(false);
                if (existingBook == null)
                {
                    BookEntity newBook = new()
                    {
                        Title = book.Title,
                        Authors = book.Authors,
                        PageStart = book.PageStart,
                        PageEnd = book.PageEnd,
                        Reference = book.Reference,
                        TopicId = topic_in_db!.Id
                    };
                    await ctx.Books.AddAsync(newBook).ConfigureAwait(false);
                    await ctx.SaveChangesAsync().ConfigureAwait(false);
                }
            });
        });
    }

    private async Task SyncTopicsAsync(StudyTree studyTree, DbContextOptions<ExercisesContext> dbContextOptions)
    {
        using var ctx = new ExercisesContext(dbContextOptions);
        studyTree.Topics.ForEach(async topic =>
        {
            TopicEntity? existingTopic = await ctx.Topics.FirstOrDefaultAsync(
                    t => t.Name == topic.Name && t.Reference == topic.Reference)
            .ConfigureAwait(false);
            if (existingTopic == null)
            {
                // validation!
                TopicEntity newTopic = new() { Name = topic.Name, Reference = topic.Reference };
                await ctx.Topics.AddAsync(newTopic).ConfigureAwait(false);
                await ctx.SaveChangesAsync().ConfigureAwait(false);
            }
        });
    }
}