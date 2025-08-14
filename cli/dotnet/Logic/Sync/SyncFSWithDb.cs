namespace Exercises.Logic.Sync;

using CatalogParser.Model;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;
using Repository.Models;
using Scenarios.Book.AddNew;
using Scenarios.Book.Find;
using Scenarios.Book.UpdateBook;
using Scenarios.Chapter.Add;
using Scenarios.Chapter.Find;
using Scenarios.Chapter.Update;
using Scenarios.Exercise.UpdateOrInsert;
using Scenarios.Section.Add;
using Scenarios.Section.Find;
using Scenarios.Topic.Add;
using Scenarios.Topic.Find;

public class SyncFsWithDb(
    DbContextOptions<ExercisesContext> dbContextOptions,
    FindTopicByNameAndReferenceScenario findTopicByNameAndReferenceScenario,
    FindBookByTopicIdAndReferenceScenario findBookByTopicIdAndReferenceScenario,
    AddNewBookByTopicIdAndParsedBook addNewBookByTopicIdAndParsedBook,
    FindChapterByReferenceScenario findChapterByReferenceScenario,
    AddNewTopicScenario addNewTopicScenario,
    UpdateBookScenario updateBookScenario,
    FindChapterByBookIdAndReferenceScenario findChapterByBookIdAndReferenceScenario,
    AddNewChapterScenario addNewChapterScenario,
    FindBookByReferenceScenario findBookByReferenceScenario,
    UpdateChapterScenario updateChapterScenario,
    FindSectionByChapterIdAndSectionNumberScenario findSectionByChapterIdAndSectionNumberScenario,
    AddNewSectionScenario addNewSectionScenario,
    GetAllTopicsScenario getAllTopicsScenario,
    FindTopicByReferenceScenario findTopicByReferenceScenario,
    UpdateOrInsertExerciseScenario updateOrInsertExerciseScenario
)
{
    public Either<ExerciseError, Unit> Execute(
        ExerciseRecord exerciseRecord
    )
    {
        using ExercisesContext ctx = new(dbContextOptions);
        IDbContextTransaction transaction = ctx.Database.BeginTransaction();
        Either<ExerciseError, Unit> result = from syncTopicsResult in SyncTopics(exerciseRecord, ctx)
                .Do((_) => { Console.WriteLine("=== Topic synced."); })
            from syncBooksResult in SyncBooks(exerciseRecord, ctx)
                .Do((_) => { Console.WriteLine("=== Books synced."); })
            from syncChaptersResult in SyncChapters(exerciseRecord, ctx)
                .Do((_) => { Console.WriteLine("=== Chapters synced."); })
            from syncSectionsResult in SyncSections(exerciseRecord, ctx)
                .Do((_) => { Console.WriteLine("=== Sections synced."); })
            from syncExercisesResult in SyncExercises(ctx)
            select syncExercisesResult;
        return result.Match(
            nope =>
            {
                Console.WriteLine($"transaction rollbacked: {nope.Message}");
                transaction.Rollback();
                return Unit.Default;
            },
            yup =>
            {
                Console.WriteLine("transaction completed");
                transaction.Commit();
                return Unit.Default;
            }
        );
    }

    private Either<ExerciseError, Unit> SyncChapters(
        ExerciseRecord exerciseRecord,
        ExercisesContext ctx
    ) => toSeq(exerciseRecord.Chapters).FoldWhile(
        Either<ExerciseError, Unit>.Right(Unit.Default),
        (
            state,
            parsedChapter
        ) =>
        {
            return from bookEntityOption in findBookByReferenceScenario.Execute(parsedChapter.BookReference, ctx)
                from bookEntity in bookEntityOption.ToEither(() => new ExerciseError("There is no book"))
                from doesExist in findChapterByBookIdAndReferenceScenario.Execute(
                    bookEntity.Id,
                    parsedChapter.Reference,
                    ctx
                )
                from _ in doesExist.Match(
                    _ => Unit.Default,
                    () => from result in addNewChapterScenario.Execute(parsedChapter, bookEntity.Id, ctx)
                        select Unit.Default
                )
                select Unit.Default;
        },
        state => state.State.IsRight
    );

    private Either<ExerciseError, Unit> SyncExercises(
        ExercisesContext ctx
    )
    {
        Either<ExerciseError, List<TopicEntity>> topicsResult = getAllTopicsScenario.Execute(ctx);
        if (topicsResult.IsLeft)
        {
            return Left(new ExerciseError($"The database is empty."));
        }

        List<TopicEntity> t = null;
        topicsResult.IfRight(res => t = res);
        Seq<TopicEntity> topicSeq = toSeq(t);

        return topicSeq.FoldWhile(
            Right<ExerciseError, Unit>(unit),
            (
                acc,
                topic
            ) =>
            {
                Seq<BookEntity> bookSeq = toSeq(topic.Books);
                return bookSeq.FoldWhile(
                    Right<ExerciseError, Unit>(unit),
                    (
                        acc1,
                        book
                    ) =>
                    {
                        Seq<ChapterEntity> chapterSeq = toSeq(book.Chapters);
                        return chapterSeq.FoldWhile(
                            Right<ExerciseError, Unit>(unit),
                            (
                                acc2,
                                chapter
                            ) =>
                            {
                                Seq<SectionEntity> sectionSeq = toSeq(chapter.Sections);
                                return sectionSeq.FoldWhile(
                                    Right<ExerciseError, Unit>(unit),
                                    (
                                        acc3,
                                        section
                                    ) =>
                                    {
                                        return from appExercisesResult in UpdateOrInsertExercises(
                                                topic.Id,
                                                book.Id,
                                                chapter.Id,
                                                0,
                                                section.Id,
                                                section.SectionNumber,
                                                section.ApplicationQuestionsIntervalStart,
                                                section.ApplicationQuestionsIntervalEnd,
                                                ExerciseType.Application,
                                                ctx
                                            )
                                            from conceptExercisesResult in UpdateOrInsertExercises(
                                                topic.Id,
                                                book.Id,
                                                chapter.Id,
                                                0,
                                                section.Id,
                                                section.SectionNumber,
                                                section.ConceptQuestionsIntervalStart,
                                                section.ConceptQuestionsIntervalEnd,
                                                ExerciseType.Concept,
                                                ctx
                                            )
                                            from skillExercisesResult in UpdateOrInsertExercises(
                                                topic.Id,
                                                book.Id,
                                                chapter.Id,
                                                0,
                                                section.Id,
                                                section.SectionNumber,
                                                section.SkillQuestionsIntervalStart,
                                                section.SkillQuestionsIntervalEnd,
                                                ExerciseType.Skill,
                                                ctx
                                            )
                                            from discussExercisesResult in UpdateOrInsertExercises(
                                                topic.Id,
                                                book.Id,
                                                chapter.Id,
                                                0,
                                                section.Id,
                                                section.SectionNumber,
                                                section.DiscussionQuestionsIntervalStart,
                                                section.DiscussionQuestionsIntervalEnd,
                                                ExerciseType.Discussion,
                                                ctx
                                            )
                                            select discussExercisesResult;
                                    },
                                    _ => true
                                );
                            },
                            _ => true
                        );
                    },
                    _ => true
                );
            },
            _ => true
        );
    }

    private Either<ExerciseError, Unit> UpdateOrInsertExercises(
        long topicId,
        long bookId,
        long chapterId,
        long chapterIdInTheBook,
        long sectionId,
        double sectionIdInTheBook,
        int intervalStart,
        int intervalEnd,
        ExerciseType questionTypeEnum,
        ExercisesContext ctx
    )
    {
        for (int exerciseNumberInTheBook = intervalStart;
             exerciseNumberInTheBook < intervalEnd;
             exerciseNumberInTheBook++)
        {
            Either<ExerciseError, ExerciseEntity> operationResult = updateOrInsertExerciseScenario.Execute(
                topicId,
                bookId,
                chapterId,
                chapterIdInTheBook,
                sectionId,
                sectionIdInTheBook,
                exerciseNumberInTheBook,
                questionTypeEnum,
                ctx
            );
            if (operationResult.IsLeft)
            {
                ExerciseError error = null;
                operationResult.IfLeft(leftResult => error = leftResult);
                return Left(error);
            }
        }

        return Either<ExerciseError, Unit>.Right(Unit.Default);
    }

    private Either<ExerciseError, Unit> SyncSections(
        ExerciseRecord exerciseRecord,
        ExercisesContext ctx
    ) => toSeq(exerciseRecord.Sections).FoldWhile(
        Either<ExerciseError, Unit>.Right(Unit.Default),
        (state, parsedSection) =>
        {
            return from chapterFound in findChapterByReferenceScenario.Execute(parsedSection.ChapterReference, ctx)
                from chapter in chapterFound.ToEither(() =>
                    new ExerciseError($"Something is wrong with the chapter by reference")
                )
                from createdSection in addNewSectionScenario.Execute(parsedSection, chapter.Id, ctx)
                select Unit.Default;
        },
        parsedTopicState => parsedTopicState.State.IsRight
    );

    private Either<ExerciseError, Unit> SyncBooks(
        ExerciseRecord exerciseRecord,
        ExercisesContext ctx
    ) =>
        from books in toSeq(exerciseRecord.Books).FoldWhile(
            Either<ExerciseError, Unit>.Right(Unit.Default),
            (state, singleBook) =>
            {
                return from topicInDb in findTopicByReferenceScenario.Execute(singleBook.TopicReference, ctx)
                    from topicEntity in topicInDb.ToEither(() =>
                        new ExerciseError($"No topic with topic reference {singleBook.TopicReference}")
                    )
                    from bookFindingResult in findBookByTopicIdAndReferenceScenario.Execute(
                        topicEntity.Id,
                        singleBook.Reference,
                        ctx
                    )
                    from _ in bookFindingResult.Match(
                        _ => Right(Unit.Default),
                        () => from result in addNewBookByTopicIdAndParsedBook.Execute(
                                topicEntity.Id,
                                singleBook,
                                ctx
                            )
                            select Right(Unit.Default)
                    )
                    select Unit.Default;
            },
            state => state.State.IsRight
        )
        select Unit.Default;


    private Either<ExerciseError, Unit> SyncTopics(
        ExerciseRecord exerciseRecord,
        ExercisesContext ctx
    ) =>
        toSeq(exerciseRecord.Topics).FoldWhile(
            Either<ExerciseError, Unit>.Right(Unit.Default),
            (
                    _,
                    parsedTopic
                ) => from foundTopicOptional in findTopicByNameAndReferenceScenario.Execute(
                    parsedTopic.Name,
                    parsedTopic.Reference,
                    ctx
                )
                from __ in foundTopicOptional
                    .Match(
                        topic => Unit.Default,
                        () => from recordedTopic in addNewTopicScenario.Execute(parsedTopic, ctx)
                            select Unit.Default
                    )
                select Unit.Default,
            state => state.State.IsRight
        );
}