namespace ApiTemplate.Services.Files;

public interface ICsvService
{
    Task<List<T>> ParseStream<T>(Stream stream) where T : class, new();
    List<T> ParseText<T>(string text) where T : class, new();
}

public class CsvService : ICsvService
{
    public async Task<List<T>> ParseStream<T>(Stream stream) where T : class, new()
    {
        using var reader = new StreamReader(stream);
        var text = await reader.ReadToEndAsync();

        return !string.IsNullOrEmpty(text) ? ParseText<T>(text) : [];
    }

    public List<T> ParseText<T>(string text) where T : class, new()
    {
        var data = text.Split('\n');
        var columns = data[0].Split(";");
        var lines = data[1..];

        return [.. lines.Select(x => ParseLine<T>(x, columns))];
    }

    private T ParseLine<T>(string line, string[] columns) where T : class, new()
    {
        var result = new T();

        var count = 0;
        foreach (var value in line.Split(";"))
        {
            var prop = typeof(T).GetProperty(SanitizeValue(columns[count]));

            if (prop is not null && prop.CanWrite)
                prop.SetValue(result, Convert.ChangeType(SanitizeValue(value), prop.PropertyType), null);

            count++;
        }

        return result;
    }

    private string SanitizeValue(string value)
    {
        return value.StartsWith('"') && value.EndsWith('"') ? value[1..^1] : value;
    }
}
