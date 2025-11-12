using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using BookAChristmasHam.UI.Helper;
using Spectre.Console;

namespace BookAChristmasHam.Service
{
    public static class JsonService
    {
        // Generisk JsonService-klass för hantering av JSON-data

        // Statisk generisk metod för att läsa JSON-data från fil
        public static List<T> ReadFromJsonFile<T>(string filePath)
        {
            // Relativ sökväg för bättre läsbarhet, kan ändras senare
            var displayPath = PathService.GetRelativePath(filePath);

            List<T> items = new(); // Förbered lista att fylla med data

            // Visar spinner medan data laddas
            LoadingUI.RunWithSpinner($"Laddar data från: {displayPath}", () =>
            {
                // Kontrollera om filen finns, om inte skapa tom JSON-array
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "[]");
                    AnsiConsole.MarkupLine($"[yellow]File not found:[/] {displayPath}");
                    items = new List<T>(); // Returnera tom lista
                    return;
                }

                try
                {
                    // Läs JSON-innehållet från filen
                    var json = File.ReadAllText(filePath);

                    // Kontrollera om filen är tom
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        AnsiConsole.MarkupLine($"[yellow]File is empty:[/] {displayPath}");
                        items = new List<T>(); // Returnera tom lista
                        return;
                    }

                    // Försök deserialisera JSON till lista av objekt av typ T
                    var deserialized = JsonSerializer.Deserialize<List<T>>(json);

                    // Kontrollera om deserialiseringen misslyckades
                    if (deserialized == null)
                    {
                        AnsiConsole.MarkupLine($"[red]Could not parse JSON format:[/] {displayPath}");
                        items = new List<T>(); // Returnera tom lista
                        return;
                    }

                    items = deserialized; // Sätt laddade objekt
                }
                catch (Exception ex)
                {
                    // Fel vid läsning av JSON
                    AnsiConsole.MarkupLine($"[red]Error reading JSON:[/] {ex.Message}");
                    items = new List<T>(); // Returnera tom lista
                }
            });

            // Bekräfta antal laddade objekt efter spinnern
            AnsiConsole.MarkupLine($"[green]Loaded {items.Count} items from:[/] {displayPath}");
            return items;

        }


        // Statisk metod för att spara lista till JSON-fil
        public static void SaveToJsonFile<T>(List<T> items, string filePath)
        {
            var displayPath = PathService.GetRelativePath(filePath);

            // Visar spinner medan data sparas
            LoadingUI.RunWithSpinner($"Sparar data till: {displayPath}", () =>
            {
                try
                {
                    // Behövs för att skriva ut enum-värden som strängar
                    var options = new JsonSerializerOptions
                    {
                        Converters = { new JsonStringEnumConverter() },
                        WriteIndented = true
                    };

                    // Serialisera listan till JSON-format
                    var json = JsonSerializer.Serialize(items, options);

                    // Skriv JSON till fil (skriver över, men tar inte bort gammal fil)
                    File.WriteAllText(filePath, json);

                    // Bekräfta att data sparats
                    AnsiConsole.MarkupLine($"[green]Data saved to file:[/] {displayPath}");
                }
                catch (Exception ex)
                {
                    // Fel vid sparning
                    AnsiConsole.MarkupLine($"[red]Error saving to file:[/] {ex.Message}");
                }
            });
        }




    }
}
