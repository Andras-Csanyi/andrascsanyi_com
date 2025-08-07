namespace Exercises.Logic.Sync;

using CatalogParser.Model;
using Exercises.Logic.Repository.Book;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Models;

public class SyncFsWithDb(
        DbContextOptions<ExercisesContext> dbContextOptions,
            BookRepository bookRepository
        )
{
    public async Task Execute(StudyTree studyTree)
    {
        await SyncTopicsAsync(studyTree).ConfigureAwait(false);
        await SyncBooksAsync(studyTree).ConfigureAwait(false);
        await SyncChaptersAsync(studyTree).ConfigureAwait(false);
        await SyncSectionsAsync(studyTree).ConfigureAwait(false);
        await SyncExercisesAsync().ConfigureAwait(false);
    }

    private async Task SyncExercisesAsync()
    {
        await using ExercisesContext outsideCtx = new(dbContextOptions);
        List<TopicEntity> topics = await outsideCtx.Topics
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
                        await using ExercisesContext ctx = new(dbContextOptions);
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

                        for (int skillExerciseNumber = section.SkillQuestionsIntervalStart;
                            skillExerciseNumber <= section.SkillQuestionsIntervalEnd;
                            skillExerciseNumber++)
                        {
                            ExerciseEntity? eSkillFound = await ctx.Exercises.FirstOrDefaultAsync(i =>
                                    i.TopicId == topic.Id
                                    && i.BookId == book.Id
                                    && i.ChapterId == chapter.Id
                                    && i.SectionId == section.Id
                                    && i.SectionIdInThebook == section.SectionNumber
                                    && i.ExerciseType == ExerciseType.Skill
                                    && i.IdInTheBook == skillExerciseNumber)
                                .ConfigureAwait(false);
                            if (eSkillFound == null)
                            {
                                ExerciseEntity newSkill = new()
                                {
                                    IdInTheBook = skillExerciseNumber,
                                    SectionId = section.Id,
                                    SectionIdInThebook = section.SectionNumber,
                                    ChapterId = chapter.Id,
                                    BookId = book.Id,
                                    TopicId = topic.Id,
                                    ExerciseType = ExerciseType.Concept,
                                };
                                await ctx.Exercises.AddAsync(newSkill).ConfigureAwait(false);
                                await ctx.SaveChangesAsync().ConfigureAwait(false);
                            }
                        }

                        for (int discussionExerciseNumber = section.DiscussionQuestionsIntervalStart;
                                discussionExerciseNumber <= section.DiscussionQuestionsIntervalEnd;
                                discussionExerciseNumber++)
                        {
                            ExerciseEntity? eDiscussionFound = await ctx.Exercises.FirstOrDefaultAsync(i =>
                                    i.TopicId == topic.Id
                                    && i.BookId == book.Id
                                    && i.ChapterId == chapter.Id
                                    && i.SectionId == section.Id
                                    && i.SectionIdInThebook == section.SectionNumber
                                    && i.ExerciseType == ExerciseType.Skill
                                    && i.IdInTheBook == discussionExerciseNumber)
                                .ConfigureAwait(false);
                            if (eDiscussionFound == null)
                            {
                                ExerciseEntity newSkill = new()
                                {
                                    IdInTheBook = discussionExerciseNumber,
                                    SectionId = section.Id,
                                    SectionIdInThebook = section.SectionNumber,
                                    ChapterId = chapter.Id,
                                    BookId = book.Id,
                                    TopicId = topic.Id,
                                    ExerciseType = ExerciseType.Discussion,
                                };
                                await ctx.Exercises.AddAsync(newSkill).ConfigureAwait(false);
                                await ctx.SaveChangesAsync().ConfigureAwait(false);
                            }
                        }
                    });
                });
            });
        });
    }

    private async Task SyncSectionsAsync(StudyTree studyTree)
    {
        studyTree.Topics.ForEach(async topic =>
        {
            topic.Books.ForEach(async book =>
            {
                book.Chapters.ForEach(async chapter =>
                {
                    using ExercisesContext chapterCtx = new(dbContextOptions);
                    ChapterEntity chapter_in_db = await chapterCtx.Chapters
                        .FirstOrDefaultAsync(c => c.Reference == chapter.Reference)
                        .ConfigureAwait(false);

                    chapter.Sections.ForEach(async section =>
                    {
                        using ExercisesContext sectionCtx = new(dbContextOptions);
                        SectionEntity? existingSectionEntity = await sectionCtx.Sections
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
                            await sectionCtx.Sections.AddAsync(newSectionEntity).ConfigureAwait(false);
                            await sectionCtx.SaveChangesAsync().ConfigureAwait(false);
                        }
                    });
                });
            });
        });
    }

    private async Task SyncChaptersAsync(StudyTree studyTree)
    {
        studyTree.Topics.ForEach(async topic =>
        {
            topic.Books.ForEach(async book =>
            {
                using ExercisesContext bookCtx = new(dbContextOptions);
                BookEntity book_in_db = await bookCtx.Books.FirstOrDefaultAsync(b => b.Reference == book.Reference)
                    .ConfigureAwait(false);
                book.Chapters.ForEach(async chapter =>
                {
                    using ExercisesContext chapterCtx = new(dbContextOptions);
                    ChapterEntity? existingEntity = await chapterCtx.Chapters
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
                        await chapterCtx.Chapters.AddAsync(newEntity).ConfigureAwait(false);
                        await chapterCtx.SaveChangesAsync().ConfigureAwait(false);
                    }
                });
            });
        });
    }

    private async Task SyncBooksAsync(StudyTree studyTree)
    {
        studyTree.Topics.ForEach(async topic =>
        {
            using ExercisesContext topicCtx = new(dbContextOptions);
            TopicEntity topic_in_db = await topicCtx.Topics
                .FirstOrDefaultAsync(t => t.Name == topic.Name && t.Reference == topic.Reference)
                .ConfigureAwait(false);

            topic.Books.ForEach(async book =>
            {
                var result = await from book in bookRepository.FindBook(book.Reference)
                                   from _ in bookRepository.AddNewBook(book)
                                   select Unit.Default;
            });
        });
    }

    private async Task SyncTopicsAsync(StudyTree studyTree)
    {
        Console.WriteLine($"hell yeah.... topic volume: {studyTree.Topics.Count}");
        studyTree.Topics.ForEach(async topic =>
        {
            using ExercisesContext ctx = new(dbContextOptions);
            Console.WriteLine($"searching... {topic.Name}");
            TopicEntity? existingTopic = await ctx.Topics
                .FirstOrDefaultAsync(t => t.Name == topic.Name && t.Reference == topic.Reference)
                .ConfigureAwait(false);
            if (existingTopic == null)
            {
                // validation!
                TopicEntity newTopic = new() { Name = topic.Name, Reference = topic.Reference, };
                await ctx.Topics.AddAsync(newTopic).ConfigureAwait(false);
                await ctx.SaveChangesAsync().ConfigureAwait(false);
                Console.WriteLine("writing to db...");
            }
        });
    }
}