namespace WeatherProject.Core
{
    public static class MögelRiskBeräkning
    {
        public static (decimal RiskVärde, string RiskBeskrivning) Beräkna(decimal temperatur, decimal luftfuktighet)
        {
            decimal riskVärde;
            string riskBeskrivning;

            if (luftfuktighet < 70 || temperatur < 5)
            {
                riskVärde = 1.0m;
                riskBeskrivning = "Låg risk";
            }
            else if (luftfuktighet >= 70 && luftfuktighet <= 85 && temperatur >= 5 && temperatur <= 15)
            {
                riskVärde = 2.5m;
                riskBeskrivning = "Medium risk";
            }
            else if (luftfuktighet > 85 && temperatur > 15)
            {
                riskVärde = 4.0m;
                riskBeskrivning = "Hög risk";
            }
            else
            {
                riskVärde = 0.5m;
                riskBeskrivning = "Mycket låg risk";
            }

            return (riskVärde, riskBeskrivning);
        }
    }
}