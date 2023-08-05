namespace SharpLocalization.Console.Abstract;

public interface IGenerator
{
    void Generate(string excelPath, string outputPath);
}