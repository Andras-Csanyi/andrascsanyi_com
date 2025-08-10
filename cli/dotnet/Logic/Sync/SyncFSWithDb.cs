namespace Exercises.Logic.Sync;

using CatalogParser.Model;
using Common;
using LanguageExt;
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
using static LanguageExt.Prelude;

public class SyncFsWithDb(
    DbContextOptions<ExercisesContext> dbContextOptions,
    FindTopicByNameAndReferenceScenario findTopicByNameAndReferenceScenario,
    FindBookByTopicIdAndReferenceScenario findBookByTopicIdAndReferenceScenario,
    AddNewBookByTopicIdAndParsedBook addNewBookByTopicIdAndParsedBook,
    AddNewTopicScenario addNewTopicScenario,
    UpdateBookScenario updateBookScenario,
    FindChapterByBookIdAndReferenceScenario findChapterByBookIdAndReferenceScenario,
    AddNewChapterScenario addNewChapterScenario,
    UpdateChapterScenario updateChapterScenario,
    FindSectionByChapterIdAndSectionNumberScenario findSectionByChapterIdAndSectionNumberScenario,
    AddNewSectionScenario addNewSectionScenario,
    GetAllTopicsScenario getAllTopicsScenario,
    UpdateOrInsertExerciseScenario updateOrInsertExerciseScenario
)
{
    private Either<ExerciseError, Unit> SyncChapters(
        StudyTree studyTree,
        ExercisesContext ctx
    )
    {
        foreach (Topic parsedTopic in studyTree.Topics)
        {
            Either<ExerciseError, TopicEntity> topicEntityFoundInDb = findTopicByNameAndReferenceScenario
                .Execute(parsedTopic.Name, parsedTopic.Reference, ctx);
            if (topicEntityFoundInDb.IsLeft)
            {
                continue;
            }

            TopicEntity? topicEntityInDb = null;
            topicEntityFoundInDb.IfRight(res => topicEntityInDb = res);

            foreach (Book parsedBook in parsedTopic.Books)
            {
                Either<ExerciseError, BookEntity> bookEntityFoundInDb = findBookByTopicIdAndReferenceScenario
                    .Execute(topicEntityInDb!.Id, parsedBook.Reference, ctx);
                if (bookEntityFoundInDb.IsLeft)
                {
                    continue;
                }

                BookEntity? bookEntityInDb = null;
                bookEntityFoundInDb.IfRight(res => bookEntityInDb = res);

                foreach (Chapter parsedChapter in parsedBook.Chapters)
                {
                    Either<ExerciseError, ChapterEntity> chapterEntityFoundInDb =
                        findChapterByBookIdAndReferenceScenario
                            .Execute(bookEntityInDb!.Id, parsedChapter.Reference, ctx);
                    if (chapterEntityFoundInDb.IsLeft)
                    {
                        return addNewChapterScenario.Execute(parsedChapter, bookEntityInDb.Id, ctx)
                            .Match(
                                Right: _ => Right(Unit.Default),
                                Left: error =>
                                    Left(new ExerciseError(
                                        $"Error happened while recording {nameof(ChapterEntity)}. Error: {error.Message}")));
                    }

                    return updateChapterScenario.Execute(parsedChapter, bookEntityInDb.Id, ctx)
                        .Match(
                            Right: _ => Right(Unit.Default),
                            Left: error =>
                                Left(new ExerciseError(
                                    $"Error happened while updating {nameof(ChapterEntity)}. Error: {error.Message}")));
                }
            }
        }

        return Right<ExerciseError, Unit>(Unit.Default);
    }

    public void Execute(
        StudyTree studyTree
    )
    {
        using ExercisesContext ctx = new(dbContextOptions);
        IDbContextTransaction transaction = ctx.Database.BeginTransaction();
        Console.WriteLine("transaction started");
        Either<ExerciseError, Unit> result = from syncTopicsResult in SyncTopics(studyTree, ctx)
                .Do(failedSyncTopics =>
                {
                    transaction.Rollback();
                    Console.WriteLine($"Failed to sync topics. Error: {failedSyncTopics}");
                })
            from syncBooksResult in SyncBooks(studyTree, ctx)
                .Do(failedSyncBooks =>
                {
                    transaction.Rollback();
                    Console.WriteLine($"Failed to sync books. Error: {failedSyncBooks}");
                })
            from syncChaptersResult in SyncChapters(studyTree, ctx)
                .Do(failedSyncChapters =>
                {
                    transaction.Rollback();
                    Console.WriteLine($"Failed to sync chapters. Error: {failedSyncChapters}");
                })
            from syncSectionsResult in SyncSections(studyTree, ctx)
                .Do(failedSyncSections =>
                {
                    transaction.Rollback();
                    Console.WriteLine($"Failed to sync sections. Error: {failedSyncSections}");
                })
            from syncExercisesResult in SyncExercises(ctx)
                .Do(failedSyncExercises =>
                {
                    transaction.Rollback();
                    Console.WriteLine($"Failed to sync exercises. Error: {failedSyncExercises}");
                })
            select syncExercisesResult;
        Console.WriteLine("transaction committed");
        transaction.Commit();
    }

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

        return topicSeq.FoldWhile(Right<ExerciseError, Unit>(unit), (
            acc,
            topic
        ) =>
        {
            Seq<BookEntity> bookSeq = toSeq(topic.Books);
            return bookSeq.FoldWhile(Right<ExerciseError, Unit>(unit), (
                acc1,
                book
            ) =>
            {
                Seq<ChapterEntity> chapterSeq = toSeq(book.Chapters);
                return chapterSeq.FoldWhile(Right<ExerciseError, Unit>(unit), (
                    acc2,
                    chapter
                ) =>
                {
                    Seq<SectionEntity> sectionSeq = toSeq(chapter.Sections);
                    return sectionSeq.FoldWhile(Right<ExerciseError, Unit>(unit), (
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
                    }, _ => true);
                }, _ => true);
            }, _ => true);
        }, _ => true);
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
        StudyTree studyTree,
        ExercisesContext ctx
    )
    {
        foreach (Topic parsedTopic in studyTree.Topics)
        {
            Either<ExerciseError, TopicEntity> topicEntityFoundInDb = findTopicByNameAndReferenceScenario
                .Execute(parsedTopic.Name, parsedTopic.Reference, ctx);
            if (topicEntityFoundInDb.IsLeft)
            {
                continue;
            }

            TopicEntity? topicEntityInDb = null;
            topicEntityFoundInDb.IfRight(res => topicEntityInDb = res);

            foreach (Book parsedBook in parsedTopic.Books)
            {
                Either<ExerciseError, BookEntity> bookEntityFoundInDb = findBookByTopicIdAndReferenceScenario
                    .Execute(topicEntityInDb!.Id, parsedBook.Reference, ctx);
                if (bookEntityFoundInDb.IsLeft)
                {
                    continue;
                }

                BookEntity? bookEntityInDb = null;
                bookEntityFoundInDb.IfRight(res => bookEntityInDb = res);

                foreach (Chapter parsedChapter in parsedBook.Chapters)
                {
                    Either<ExerciseError, ChapterEntity> chapterEntityFoundInDb =
                        findChapterByBookIdAndReferenceScenario
                            .Execute(bookEntityInDb!.Id, parsedChapter.Reference, ctx);
                    if (chapterEntityFoundInDb.IsLeft)
                    {
                        continue;
                    }

                    ChapterEntity? chapterEntityInDb = null;
                    chapterEntityFoundInDb.IfRight(res => chapterEntityInDb = res);

                    foreach (Section parsedSection in parsedChapter.Sections)
                    {
                        Either<ExerciseError, SectionEntity> sectionEntityFoundInDb =
                            findSectionByChapterIdAndSectionNumberScenario.Execute(
                                chapterEntityInDb!.Id, parsedSection.SectionNumber, ctx);
                        if (sectionEntityFoundInDb.IsLeft)
                        {
                            addNewSectionScenario.Execute(parsedSection, chapterEntityInDb.Id, ctx)
                                .IfLeft(e => Left(new ExerciseError(e.Message)));
                        }
                    }
                }
            }
        }

        return Either<ExerciseError, Unit>.Right(Unit.Default);
    }

    private Either<ExerciseError, Unit> SyncBooks(
        StudyTree studyTree,
        ExercisesContext ctx
    )
    {
        Seq<Topic> topicsInStudyTree = toSeq(studyTree.Topics);
        return topicsInStudyTree.FoldWhile(
            Either<ExerciseError, Unit>(unit),
            (
                something,
                topicInSeq
            ) =>
            {
                Either<ExerciseError, TopicEntity> topicId =
                    from topicInDb in findTopicByNameAndReferenceScenario.Execute(
                        topicInSeq.Name,
                        topicInSeq.Reference,
                        ctx)
                    from t in topicInDb.ToEither(() => new ExerciseError(
                        $"At this point the {nameof(TopicEntity)} with " +
                        $"name: {topicInSeq.Name} " +
                        $"reference: {topicInSeq.Reference} should " +
                        $"have value."))
                    select t;

                topicInSeq.Books.ForEach(bookFromStudyTree =>
                {
                    Either<ExerciseError, Unit> o =
                        from book in findBookByTopicIdAndReferenceScenario.Execute(
                                foundTopic.Id,
                                bookFromStudyTree.Reference, ctx)
                            .Do(notFound =>
                            {
                                updateBookScenario.Execute(foundTopic.Id, bookFromStudyTree, ctx);
                            })
                        from _ in addNewBookByTopicIdAndParsedBook.Execute(foundTopic.Id, bookFromStudyTree, ctx)
                        select Unit.Default;
                });
            }, _ => true);
    }


    private Either<ExerciseError, Unit> SyncTopics(
        StudyTree studyTree,
        ExercisesContext ctx
    )
    {
        Seq<Topic> topics = toSeq(studyTree.Topics);
        return topics.FoldWhile(Either<ExerciseError, Unit>(unit), (
            err,
            parsedTopic
        ) =>
        {
            return from foundTopicOptional in findTopicByNameAndReferenceScenario.Execute(
                        parsedTopic.Name,
                        parsedTopic.Reference,
                        ctx)
                    .Match(
                        Right: topicOptional =>
                        {
                            return topicOptional.Match(
                                Some: _ => Right<ExerciseError, Unit>(Unit.Default),
                                None: () =>
                                {
                                    return addNewTopicScenario.Execute(parsedTopic, ctx)
                                        .Match(
                                            Right: Right<ExerciseError, Unit>(Unit.Default),
                                            Left: error => Left<ExerciseError, Unit>(error));
                                });
                        },
                        Left: error => Left(error))
                select Unit.Default;


            // Either<ExerciseError, Option<TopicEntity>> topicInDb =
            //     findTopicByNameAndReferenceScenario.Execute(parsedTopic.Name, parsedTopic.Reference, ctx);
            // if (topicInDb.IsLeft)
            // {
            //     ExerciseError operationError = null;
            //     topicInDb.IfLeft(e => operationError = e);
            //     return Left(operationError);
            // }
            //
            // bool isSome = false;
            // topicInDb.IfRight(r =>
            // {
            //     if (r.IsSome) { isSome = true; }
            // });
            // if (isSome)
            // {
            //     return Right<ExerciseError, Unit>(Unit.Default);
            // }
            //
            // Either<ExerciseError, TopicEntity> addingResult = addNewTopicScenario.Execute(parsedTopic, ctx);
            // if (addingResult.IsLeft)
            // {
            //     return addingResult.IfLeft(error => Left(new ExerciseError(error.Message)));
            // }
            //
            // return Right<ExerciseError, Unit>(Unit.Default);

            // from operationResult in findTopicByNameAndReferenceScenario
            //         .Execute(parsedTopic.Name, parsedTopic.Reference, ctx)
            //         .Match(
            //             Right: topicInDbOption =>
            //             {
            //                 return topicInDbOption.Match<Pure<Unit>>(
            //                     Some: topicInDb => Right(Unit.Default),
            //                     None: () => Left<ExerciseError, TopicEntity>(new ExerciseError(""))
            //                 );
            //             },
            //             Left: error => Left(error)
            //         )
            //     select Unit.Default;
        }, _ => true);
    }
}