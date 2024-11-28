using System;
using System.Diagnostics;
using Väderdata.Core;
using WeatherProject.Core;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new WeatherContext())
        {
            // Exempelvärden, ändra gärna :)
            decimal medelTemperatur = 16m;
            decimal medelLuftfuktighet = 90.2m;

            // Beräkna mögelrisk
            var (riskVärde, riskBeskrivning) = MögelRiskBeräkning.Beräkna(medelTemperatur, medelLuftfuktighet);

            // Skapa nytt väderobjekt
            var nyttVäder = new Väder
            {
                Datum = DateTime.Now,
                Medeltemperatur = medelTemperatur,
                Medelluftfuktighet = medelLuftfuktighet,
                MögelRisk = riskVärde,
                Vinter = "5 december 2023",
                Höst = "22 september 2024"
            };

            
            context.VäderData.Add(nyttVäder);
            context.SaveChanges();

            
            string csvFilePath = "TempFuktData.csv";

            // Kontrollera om filen redan existerar
            bool fileExists = File.Exists(csvFilePath);

            
            using (var writer = new StreamWriter(csvFilePath, append: true))
            {
                // Om filen inte existerar
                if (!fileExists)
                {
                    writer.WriteLine("Datum,Medeltemperatur,Medelluftfuktighet,MögelRisk,Vinter,Höst");
                }

                
                writer.WriteLine($"{nyttVäder.Datum},{nyttVäder.Medeltemperatur},{nyttVäder.Medelluftfuktighet},{nyttVäder.MögelRisk},{nyttVäder.Vinter},{nyttVäder.Höst}");
            }

            Console.WriteLine("En väderpost har sparats i databasen och TempFuktData.csv!");
            Console.WriteLine($"Mögelrisk: {riskVärde} ({riskBeskrivning})");

            
            Process.Start(new ProcessStartInfo(csvFilePath) { UseShellExecute = true });
        }
    }
}