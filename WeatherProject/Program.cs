using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Väderdata.Core;
using WeatherProject.Core;
using WeatherProject.DataAccess; 

class Program
{
    static void Main(string[] args)
    {
        using (var context = new WeatherContext())
        {
            // Skapa databasen om den inte finns
            if (context.Database.EnsureCreated())
            {
                Console.WriteLine($"Databasen skapades. Tid: {DateTime.Now}");
            }
            else
            {
                Console.WriteLine($"Databasen existerar redan. Tid: {DateTime.Now}");
            }

            // Ta bort all tidigare väderdata innan vi importerar nya data
            context.VäderData.RemoveRange(context.VäderData);
            context.SaveChanges();
            Console.WriteLine($"Tidigare väderdata rensad. Tid: {DateTime.Now}");

            // Importera data från en CSV-fil vid behov
            string importFilePath = "TempFuktData.csv";
            if (File.Exists(importFilePath))
            {
                Console.WriteLine($"Importerar data från CSV... Tid: {DateTime.Now}");
                ImporteraSparaCSV.ImporteraCSV(context, importFilePath);
            }

            // Exempelvärden för att skapa ett nytt väderobjekt
            decimal medelTemperatur = 4m;
            decimal medelLuftfuktighet = 90.2m;
            string plats = "Stockholm"; // Exempel på plats

            // Beräkna mögelrisk
            var (riskVärde, riskBeskrivning) = MögelRiskBeräkning.Beräkna(medelTemperatur, medelLuftfuktighet);

            
            var nyttVäder = new Väder
            {
                Datum = DateTime.Now,
                Plats = plats, 
                Temperatur = medelTemperatur,
                Luftfuktighet = medelLuftfuktighet, 
            };

            // Kontrollera om samma väderpost redan finns baserat på plats och datum
            var existerandeVäder = context.VäderData
                .FirstOrDefault(v => v.Plats == nyttVäder.Plats && v.Datum.Date == nyttVäder.Datum.Date);

            if (existerandeVäder == null)
            {
                
                Console.WriteLine($"Väderobjekt skapat. Tid: {nyttVäder.Datum}");

                // Spara till databasen om den inte finns
                context.VäderData.Add(nyttVäder);
                context.SaveChanges();
                Console.WriteLine($"Väderdata sparat i databasen. Tid: {DateTime.Now}");

                // Exportera till CSV-fil
                ImporteraSparaCSV.SparaTillCSV(nyttVäder, "TempFuktData.csv"); // Använd metoden från klassen ImporteraSparaCSV
                Console.WriteLine($"Väderdata sparat till CSV. Tid: {DateTime.Now}");

                // Visa information
                Console.WriteLine("En väderpost har sparats i databasen och TempFuktData.csv!");
                Console.WriteLine($"Mögelrisk: {riskVärde} ({riskBeskrivning})");

                Process.Start(new ProcessStartInfo("TempFuktData.csv") { UseShellExecute = true });

                // Exempel på sortering och sökning
                var varmasteDag = context.VäderData.OrderByDescending(v => v.Temperatur).FirstOrDefault();
                Console.WriteLine($"Varmaste dagen: {varmasteDag?.Datum:d} med {varmasteDag?.Temperatur}°C");
            }
            else
            {
                Console.WriteLine($"Väderdata för {nyttVäder.Plats} den {nyttVäder.Datum.Date} finns redan. Inget nytt väder sparat.");
            }
        }
    }
}
