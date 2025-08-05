namespace Exercises.Logic.Sync;

using CatalogParser.Model;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Models;

public class SyncFsWithDb
{
    public async Task Execute(StudyTree studyTree, DbContextOptions<ExercisesContext> dbContextOptions)
    {
        await SyncTopicsAsync(studyTree, dbContextOptions).ConfigureAwait(false);
        await SyncBooksAsync(studyTree, dbContextOptions).ConfigureAwait(false);
        await SyncChaptersAsync(studyTree, dbContextOptions).ConfigureAwait(false);
        await SyncSectionsAsync(studyTree, dbContextOptions).ConfigureAwait(false);
        await SyncExercisesAsync(studyTree, dbContextOptions).ConfigureAwait(false);
    }

    private async Task SyncExercisesAsync(StudyTree studyTree, DbContextOptions<ExercisesContext> dbContextOptions)
    {
        await using ExercisesContext ctx = new(dbContextOptions);
        List<TopicEntity> topics = await ctx.Topics
            .Include(topic => topic.Books)
            .ThenInclude(book => book.Chapters)
            .ThenInclude(chapter => chapter.Sections)
            .ToListAsync();

        topics.ForEach(async topic =>
        {
            topic.Books.ForEach(async book =>
            {
                book.Chapters.ForEach(async chapter =>
                {
                    chapter.Sections.ForEach(async void (section) =>
                    {
                        for (int applicationExerciseNumber = section.ApplicationQuestionsIntervalStart;
                             applicationExerciseNumber <= section.ApplicationQuestionsIntervalEnd;
                             applicationExerciseNumber++)
                        {
                            ExerciseEntity? eFound = await ctx.Exercises.FirstOrDefaultAsync(appExercise =>
                                    appExercise.BookId == book.Id
                                    && appExercise.IdInTheBook == applicationExerciseNumber
                                    && appExercise.ChapterId == chapter.Id
                                    && appExercise.SectionId == section.Id
                                    && appExercise.SectionIdInThebook == section.SectionNumber
                                    && appExercise.TopicId == topic.Id
                                    && appExercise.ExerciseType == ExerciseType.Application)
                                .ConfigureAwait(false);
                            if (eFound == null)
                            {
                                ExerciseEntity newE = new()
                                {
                                    BookId = book.Id,
                                    ChapterId = chapter.Id,
                                    SectionId = section.Id,
                                    TopicId = topic.Id,
                                    SectionIdInThebook = section.SectionNumber,
                                    IdInTheBook = applicationExerciseNumber,
                                    ExerciseType = ExerciseType.Application,
                                };
                                await ctx.Exercises.AddAsync(newE).ConfigureAwait(false);
                                await ctx.SaveChangesAsync().ConfigureAwait(false);
                            }
                        }

                        for (int conceptExerciseNumber = section.ConceptQuestionsIntervalStart;
                             conceptExerciseNumber <= section.ConceptQuestionsIntervalEnd;
                             conceptExerciseNumber++)
                        {
                            ExerciseEntity? eConFound = await ctx.Exercises.FirstOrDefaultAsync(i =>
                                    i.TopicId == topic.Id
                                    && i.BookId == book.Id
                                    && i.ChapterId == chapter.Id
                                    && i.SectionId == section.Id
                                    && i.SectionIdInThebook == section.SectionNumber
                                    && i.ExerciseType == ExerciseType.Concept
                                    && i.IdInTheBook == conceptExerciseNumber)
                                .ConfigureAwait(false);
                            if (eConFound == null)
                            {
                                ExerciseEntity newConcept = new()
                                {
                                    IdInTheBook = conceptExerciseNumber,
                                    SectionId = section.Id,
                                    SectionIdInThebook = section.SectionNumber,
                                    ChapterId = chapter.Id,
                                    BookId = book.Id,
                                    TopicId = topic.Id,
                                    ExerciseType = ExerciseType.Concept,
                                };
                                await ctx.Exercises.AddAsync(newConcept).ConfigureAwait(false);
                                await ctx.SaveChangesAsync().ConfigureAwait(false);
                            }
                        }
                    });
                });
            });
        });
    }

    private async Task SyncSectionsAsync(StudyTree studyTree,
        DbContextOptions<ExercisesContext> dbContextOptions)
    {
        using ExercisesContext ctx = new(dbContextOptions);
        studyTree.Topics.ForEach(async topic =>
        {
            topic.Books.ForEach(async book =>
            {
                book.Chapters.ForEach(async chapter =>
                {
                    ChapterEntity chapter_in_db = await ctx.Chapters
                        .FirstOrDefaultAsync(c => c.Reference == chapter.Reference)
                        .ConfigureAwait(false);

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
                                ChapterId = chapter_in_db!.Id,
                            };
                            await ctx.Sections.AddAsync(newSectionEntity).ConfigureAwait(false);
                            await ctx.SaveChangesAsync().ConfigureAwait(false);
                        }
                    });
                });
            });
        });
    }

    private async Task SyncChaptersAsync(StudyTree studyTree,
        DbContextOptions<ExercisesContext> dbContextOptions)
    {
        using ExercisesContext ctx = new(dbContextOptions);
        studyTree.Topics.ForEach(async topic =>
        {
            topic.Books.ForEach(async book =>
            {
                BookEntity book_in_db = await ctx.Books.FirstOrDefaultAsync(b => b.Reference == book.Reference)
                    .ConfigureAwait(false);
                book.Chapters.ForEach(async chapter =>
                {
                    ChapterEntity? existingEntity = await ctx.Chapters
                        .FirstOrDefaultAsync(c => c.Reference == chapter.Reference).ConfigureAwait(false);
                    if (existingEntity == null)
                    {
                        ChapterEntity newEntity = new()
                        {
                            Title = chapter.Title,
                            Reference = chapter.Reference,
                            PageStart = chapter.PageStart,
                            PageEnd = chapter.PageEnd,
                            BookId = book_in_db!.Id,
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
        using ExercisesContext ctx = new(dbContextOptions);
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
                        TopicId = topic_in_db!.Id,
                    };
                    await ctx.Books.AddAsync(newBook).ConfigureAwait(false);
                    await ctx.SaveChangesAsync().ConfigureAwait(false);
                }
            });
        });
    }

    private async Task SyncTopicsAsync(StudyTree studyTree, DbContextOptions<ExercisesContext> dbContextOptions)
    {
        using ExercisesContext ctx = new(dbContextOptions);
        studyTree.Topics.ForEach(async topic =>
        {
            TopicEntity? existingTopic = await ctx.Topics
                .FirstOrDefaultAsync(t => t.Name == topic.Name && t.Reference == topic.Reference)
                .ConfigureAwait(false);
            if (existingTopic == null)
            {
                // validation!
                TopicEntity newTopic = new() { Name = topic.Name, Reference = topic.Reference, };
                await ctx.Topics.AddAsync(newTopic).ConfigureAwait(false);
                await ctx.SaveChangesAsync().ConfigureAwait(false);
            }
        });
    }
}