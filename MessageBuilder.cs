using System;
using System.Globalization;

namespace jogobom.MadeSandwich
{
    public static class MessageBuilder
    {
        private static readonly string[] PossibleIntros = {"Sotd.", "Sotd:", "Sotd", "SOTD", "SOTD:", "SOTD."};

        public static string Build(Sandwich sandwich)
        {
            var specificCulture = CultureInfo.CreateSpecificCulture("en-GB");

            return $"{GetRandomIntro()} {sandwich.ContentDescription} {sandwich.BreadAdvice} {sandwich.Price.ToString("C", specificCulture)}";
        }

        private static string GetRandomIntro()
        {
            return PossibleIntros[new Random().Next(PossibleIntros.Length)];
        }
    }
}
