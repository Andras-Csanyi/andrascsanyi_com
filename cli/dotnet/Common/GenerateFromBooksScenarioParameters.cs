namespace Exercises.Common;

public record GenerateFromBooksScenarioParameters(
    int SkillQuestionVolume,
    int ApplicationQuestionVolume,
    int ConceptQuestionVolume,
    int DiscussionQuestionVolume,
    string Books
);