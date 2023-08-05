namespace SharpLocalization.Abstract;

public interface ILocalizer
{
    string this[string name] { get; }
}