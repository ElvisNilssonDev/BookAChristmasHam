using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spectre.Console;

namespace BookAChristmasHam.Service
{
    public static class JsonService
    {
        // Generisk JsonService klass för hantering av JSON-data.

        // statick generisk metod för att läsa JSON-data från fil
        public static List<T> ReadFromJsonFile<T>(string filePath)
        {
            //  relative sökväg för bättre läsbarhet, kan man ändra senare
            var displayPath = PathService.GetRelativePath(filePath);

            if (!File.Exists(filePath))
            {
                AnsiConsole.MarkupLine($"[yellow]The file cannot be found:[/] {displayPath}");
                return new List<T>();
            }

            // läs JSON-innehållet från filen
            var json = File.ReadAllText(filePath);

            // deserialisera JSON till lista av objekt av typ T
            var items = JsonSerializer.Deserialize<List<T>>(json);

            return items ?? new List<T>(); // kollar om items är null, returnerar tom lista om så är fallet

            // fixar till try catch senare med feedback till användaren
        }
        public static void SaveToJsonFile<T>(List<T> items, string filePath)
        {
            var displayPath = PathService.GetRelativePath(filePath);

            // behövs för UserType printa ut sträng
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = true 
            };

            // serialisera listan av objekt till JSON-format
            var json = JsonSerializer.Serialize(items, options);

            // skriv JSON-innehållet till filen
            File.WriteAllText(filePath, json); //skriver över filen (fast det gammla ta inte bort!)

            // visar feedback 
            AnsiConsole.MarkupLine($"[green]Data saved in file:[/] {displayPath}");
            // fixar till try catch senare med feedback till användaren
        }

    } // end of class
}
