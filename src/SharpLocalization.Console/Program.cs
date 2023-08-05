// See https://aka.ms/new-console-template for more information

using eXtensionSharp;
using SharpLocalization.Console;
using SharpLocalization.Console.Abstract;

#if DEBUG
var excelPath = "D:\\multi-language.xlsx";
var outputPath = "D:\\";
#else
var excelPath = args[0];
var outputPath = args[1];
#endif

if(excelPath.xIsEmpty()) throw new Exception("excel path is empty");
if(outputPath.xIsEmpty()) throw new Exception("output path is empty");

Console.WriteLine("generator start");
var generators = new List<IGenerator>()
{
    new LabelGenerator(),
    new MessageGenerator(),
    new ButtonGenerator()
};
foreach (IGenerator generator in generators)
{
    generator.Generate(excelPath, outputPath);    
}
Console.WriteLine("generator end");
