using System;
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

            // Spara i databasen
            context.VäderData.Add(nyttVäder);
            context.SaveChanges();

            
            Console.WriteLine("En väderpost har sparats i databasen!");
            Console.WriteLine($"Mögelrisk: {riskVärde} ({riskBeskrivning})");
        }
    }
}