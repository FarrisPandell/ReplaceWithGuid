namespace ReplaceWithGuid; // Note: actual namespace depends on the project name.

internal static class Program
{
    public static void Main(string[] args)
    {
        var path = args[0];
        Console.WriteLine($"Path: {path}");
        var text = File.ReadAllText(path);
        var extension = Path.GetExtension(path).ToLowerInvariant();
        var (count, updatedText) = extension switch
        {
            ".sql" => ReplaceGuidsInSql(text),
            ".cs" => ReplaceGuidsInCSharp(text),
            _ => (0, string.Empty)
        };
        Console.WriteLine($"Replaced: {count}");
        File.WriteAllText(path, updatedText);
    }
        
    private static (int, string) ReplaceGuidsInSql(string text) => 
        Replace(text, token: "NEWID()", replaceFunc: () => $"'{Guid.NewGuid().ToString().ToUpper()}'");

    private static (int, string) ReplaceGuidsInCSharp(string text) => 
        Replace(text, token: "Guid.NewGuid()", replaceFunc: () => $"new Guid(\"{Guid.NewGuid().ToString().ToUpper()}\")");

    private static (int, string) Replace(string text, string token, Func<string> replaceFunc)
    {
        var index = text.IndexOf(token, StringComparison.OrdinalIgnoreCase);
        var count = 0;
        while (index != -1)
        {
            text = text.Remove(index, token.Length);
            text = text.Insert(index, replaceFunc());
            index = text.IndexOf(token, StringComparison.OrdinalIgnoreCase);
            count++;
        }

        return (count, text);
    }
}