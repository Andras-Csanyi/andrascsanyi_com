// See https://aka.ms/new-console-template for more information

using LanguageExt;
using static LanguageExt.Prelude;

Console.WriteLine("=== LangExt testing");

int flag = 1;
Unit what = match(
    flag == 0 ? Either<string, int>.Left("not zero") : Either<string, int>.Right(flag),
    nopes => Left("not zero"),
    yolo => Right(1)
);