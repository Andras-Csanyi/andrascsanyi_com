namespace Exercises.Logic.CatalogParser.Model;

using Common;
using LanguageExt;
using Repository.Models;
using YamlDotNet.Serialization;
using static LanguageExt.Prelude;

public class Section
{
    public string? Title { get; set; }

    [YamlMember(Alias = "section_number", ApplyNamingConventions = false)]
    public double SectionNumber { get; set; }

    [YamlMember(Alias = "page_start", ApplyNamingConventions = false)]
    public int PageStart { get; set; }

    [YamlMember(Alias = "page_exercises_start", ApplyNamingConventions = false)]
    public int PageExercisesStart { get; set; }

    [YamlMember(Alias = "concepts_questions_interval_start", ApplyNamingConventions = false)]
    public int ConceptQuestionsIntervalStart { get; set; }

    [YamlMember(Alias = "concepts_questions_interval_end", ApplyNamingConventions = false)]
    public int ConceptQuestionsIntervalEnd { get; set; }

    [YamlMember(Alias = "skills_questions_interval_start", ApplyNamingConventions = false)]
    public int SkillQuestionsIntervalStart { get; set; }

    [YamlMember(Alias = "skills_questions_interval_end", ApplyNamingConventions = false)]
    public int SkillQuestionsIntervalEnd { get; set; }

    [YamlMember(Alias = "applications_questions_interval_start", ApplyNamingConventions = false)]
    public int ApplicationQuestionsIntervalStart { get; set; }

    [YamlMember(Alias = "applications_questions_interval_end", ApplyNamingConventions = false)]
    public int ApplicationQuestionsIntervalEnd { get; set; }

    [YamlMember(Alias = "discussion_questions_interval_start", ApplyNamingConventions = false)]
    public int DiscussionQuestionsIntervalStart { get; set; }

    [YamlMember(Alias = "discussion_questions_interval_end", ApplyNamingConventions = false)]
    public int DiscussionQuestionsIntervalEnd { get; set; }

    [YamlMember(Alias = "page_end", ApplyNamingConventions = false)]
    public int PageEnd { get; set; }
}

public static class SectionExtensions
{
    public static Either<ExerciseError, SectionEntity> ToEntity(
        this Section parsedSection
    )
    {
        try
        {
            SectionEntity result = new()
            {
                Title = parsedSection.Title,
                SectionNumber = parsedSection.SectionNumber,
                PageStart = parsedSection.PageStart,
                PageExercisesStart = parsedSection.PageExercisesStart,
                ConceptQuestionsIntervalStart = parsedSection.ConceptQuestionsIntervalStart,
                ConceptQuestionsIntervalEnd = parsedSection.ConceptQuestionsIntervalEnd,
                SkillQuestionsIntervalStart = parsedSection.SkillQuestionsIntervalStart,
                SkillQuestionsIntervalEnd = parsedSection.SkillQuestionsIntervalEnd,
                ApplicationQuestionsIntervalStart = parsedSection.ApplicationQuestionsIntervalStart,
                ApplicationQuestionsIntervalEnd = parsedSection.ApplicationQuestionsIntervalEnd,
                DiscussionQuestionsIntervalStart = parsedSection.DiscussionQuestionsIntervalStart,
                DiscussionQuestionsIntervalEnd = parsedSection.DiscussionQuestionsIntervalEnd,
                PageEnd = parsedSection.PageEnd,
            };
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError(
                    $"Error while mapping {nameof(Section)} to {nameof(SectionEntity)}. Error: {e.Message}"));
        }
    }
}