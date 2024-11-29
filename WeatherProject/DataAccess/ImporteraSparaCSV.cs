using System;
using System.Globalization;
using System.IO;
using Väderdata.Core;
using WeatherProject.Core;

namespace WeatherProject.DataAccess;

public static class ImporteraSparaCSV
{
    public static void ImporteraCSV(WeatherContext context, string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var culture = CultureInfo.InvariantCulture; // För punkt som decimaltecken

        foreach (var line in lines.Skip(1)) // Hoppa över rubriker
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue; // Hoppa över tomma rader
            }

            var values = line.Split(',');

            if (values.Length != 4) // Kontrollera att rätt antal kolumner finns
            {
                continue; 
            }

            try
            {
                // Tolkning av varje kolumn från CSV och skapa ett Väder-objekt
                var väder = new Väder
                {
                    Datum = DateTime.Parse(values[0], culture), 
                    Plats = values[1], 
                    Temperatur = decimal.Parse(values[2], culture), 
                    Luftfuktighet = decimal.Parse(values[3], culture), 
                };

                // Lägg till väderobjekt i databasen
                context.VäderData.Add(väder);
            }
            catch (Exception ex)
            {
                // Här hanteras eventuella fel.
            }
        }

    }

    public static void SparaTillCSV(Väder väder, string filePath)
    {
        try
        {
            bool fileExists = File.Exists(filePath);

            using (var writer = new StreamWriter(filePath, append: true))
            {
                // Om filen inte existerar, lägg till rubriker
                if (!fileExists)
                {
                    writer.WriteLine("Datum,Plats,Temperatur,Luftfuktighet");
                }

                // Kontrollera att data är korrekt innan skrivning
                string datum = väder.Datum.ToString("yyyy-MM-dd");
                string plats = väder.Plats ?? ""; // Hantera null-värden för plats
                string temperatur = väder.Temperatur.ToString(CultureInfo.InvariantCulture);
                string luftfuktighet = väder.Luftfuktighet.ToString(CultureInfo.InvariantCulture);

                // Lägg till väderdata till CSV-filen
                writer.WriteLine($"{datum},{plats},{temperatur},{luftfuktighet}");
            }

            Console.WriteLine($"Data har sparats till {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid skrivning till CSV: {ex.Message}");
        }
    }
}

