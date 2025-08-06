namespace Exercises.Common;

public record GenerateBooksCommandParameters(
        int SkillQuestionVolume,
        int ApplicationQuestionVolume,
        int ConceptQuestionVolume,
        int DiscussionQuestionVolume
        );