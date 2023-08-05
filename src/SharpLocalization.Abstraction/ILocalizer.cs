namespace SharpLocalization.Abstraction;

public interface ILocalizer
{
    string this[string name] { get; }
}