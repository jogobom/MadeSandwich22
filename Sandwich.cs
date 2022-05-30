using System;
using System.Globalization;
using System.Linq;

namespace jogobom.MadeSandwich;

public class Sandwich
{
    private readonly Ingredient bread;
    private readonly Ingredient[] ingredients;

    private static readonly string[] BreadAdviceOptions = {"try it in", "delicious in", "perfect for", "we recommend"};
    private static readonly Random Random = new();

    public Sandwich(Ingredient bread, Ingredient[] ingredients)
    {
        this.bread = bread;
        this.ingredients = ingredients;
    }

    private static string RandomBreadAdvice()
    {
        return BreadAdviceOptions[Random.Next(BreadAdviceOptions.Length)];
    }

    public double Price => (bread.PriceInPence + ingredients.Sum(i => i.PriceInPence)) / 100.0;

    public string ContentDescription
    {
        get
        {
            var specificCulture = CultureInfo.CreateSpecificCulture("en-GB");

            var ingredientNames = ingredients.Select(i => Random.Next(2) == 0 ? i.Name : i.Name.ToLower(specificCulture)).Where(i => !string.IsNullOrWhiteSpace(i));
            var contentDescription = string.Join(", ", ingredientNames);
            return contentDescription;
        }
    }

    public string BreadAdvice => $"({RandomBreadAdvice()} {bread.Name}!)";
}    