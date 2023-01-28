namespace ConsoleApp;

public class FilePath
{
    public FilePath(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                message: $"{nameof(value)} can't be null, empty or contain only white spaces.",
                nameof(value));
        }

        Value = value;
    }

    public string Value { get; }
}
