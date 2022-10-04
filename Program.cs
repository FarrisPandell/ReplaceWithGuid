using System.Xml;

namespace ReplaceWithGuid // Note: actual namespace depends on the project name.
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var path = args[0];
            Console.WriteLine($"Path: {path}");
            var text = File.ReadAllText(path) ?? string.Empty;
            const string token = "NEWID()";
            var index = text.IndexOf(token, StringComparison.OrdinalIgnoreCase);
            var count = 0;
            while (index != -1)
            {
                text = text.Remove(index, token.Length);
                text = text.Insert(index, $"'{Guid.NewGuid()}'".ToUpper());
                index = text.IndexOf(token, StringComparison.OrdinalIgnoreCase);
                count++;
            }
            Console.WriteLine($"Replaced: {count}");
            File.WriteAllText(path, text);
        }
    }
}