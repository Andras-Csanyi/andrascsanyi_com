// See https://aka.ms/new-console-template for more information

using LanguageExt;
using static LanguageExt.Prelude;

Console.WriteLine("=== LangExt testing");

List<Whatever> init =
[
    new() { Id = 0, Name = "first", },
    new() { Id = 1, Name = "second", },
    new() { Id = 2, Name = "third", },
];
Either<string, List<Whatever>> result = toSeq(init).Fold(
    [],
    (
        List<Whatever> state,
        Whatever item
    ) =>
    {
        if (item.Name.EndsWith("d"))
        {
            state.Add(item);
        }

        return state;
    }
);


result.IfRight(r =>
    {
        Console.WriteLine($"=== Right: {r.Count}");
    }
);
result.IfLeft(l =>
    {
        Console.WriteLine($"=== Left: {l}");
    }
);

public class Whatever
{
    public int Id { get; set; }
    public string Name { get; set; }
}