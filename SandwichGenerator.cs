using System;
using System.Collections.Generic;
using System.Linq;

namespace jogobom.MadeSandwich;

public class SandwichGenerator
{
    private readonly Random random = new();

    private static List<Ingredient> Bread => new()
    {
        new Ingredient("a White Stottie", 200),
        new Ingredient("a Wholemeal Stottie", 200),
        new Ingredient("a Seeded Stottie", 200),
        new Ingredient("a Baguette", 200),
        new Ingredient("a Panini", 200),
        new Ingredient("Sliced Bread", 200)
    };

    private static List<Ingredient> Meats => new()
    {
        new Ingredient("Ham", 50),
        new Ingredient("Beef", 50),
        new Ingredient("Cumberland Sausage", 50),
        new Ingredient("Bacon", 50),
        new Ingredient("Brussels Pate", 50),
        new Ingredient("Smoked Ham", 60),
        new Ingredient("Italian Salami", 60),
        new Ingredient("Chorizo", 60),
        new Ingredient("Pastrami", 70),
        new Ingredient("Smoked Turkey", 70),
        new Ingredient("Chicken", 70),
        new Ingredient("Chicken Tikka", 70),
        new Ingredient("Turkey", 70),
        new Ingredient("Parma Ham", 90)
    };

    private static List<Ingredient> Seafood => new()
    {
        new Ingredient("Tuna", 70),
        new Ingredient("Tuna Mayo", 70),
        new Ingredient("Creamy Crab", 60),
        new Ingredient("Anchovies", 60),
        new Ingredient("King Prawns", 100),
        new Ingredient("Smoked Salmon", 100),
        new Ingredient("Small Prawns", 100)
    };

    private static List<Ingredient> Dairy => new()
    {
        new Ingredient("Grated Cheese", 50),
        new Ingredient("Cottage Cheese", 50),
        new Ingredient("Cream Cheese", 50),
        new Ingredient("Egg Mayo", 60),
        new Ingredient("Sliced Egg", 50),
        new Ingredient("Cheese Savoury", 60),
        new Ingredient("Mature Cheddar", 70),
        new Ingredient("Feta", 60),
        new Ingredient("Brie", 60),
        new Ingredient("Mozzarella", 60),
        new Ingredient("Goats Cheese", 70),
        new Ingredient("Emmental", 70),
        new Ingredient("Smoked Cheddar", 70),
        new Ingredient("Stilton", 70),
        new Ingredient("Parmesan", 70)
    };

    private static List<Ingredient> Veg => new()
    {
        new Ingredient("Red Onion", 5),
        new Ingredient("Watercress", 15),
        new Ingredient("Salad Cress", 15),
        new Ingredient("Rocket", 15),
        new Ingredient("Spinach", 15),
        new Ingredient("Radish", 15),
        new Ingredient("Sweetcorn", 30),
        new Ingredient("Grated Carrot", 30),
        new Ingredient("Beetroot", 30),
        new Ingredient("Olives", 40),
        new Ingredient("Grapes", 30),
        new Ingredient("Apple", 30),
        new Ingredient("Banana", 30),
        new Ingredient("Buttered Leaks", 30),
        new Ingredient("Pease Pudding", 40),
        new Ingredient("Stuffing", 50),
        new Ingredient("Raw Peppers", 40),
        new Ingredient("Coleslaw", 50),
        new Ingredient("Hummus", 60),
        new Ingredient("Sun Blushed Tomato", 50),
        new Ingredient("Roast Tomato", 35),
        new Ingredient("Mushrooms", 40),
        new Ingredient("Avocado", 50),
        new Ingredient("Roast Med Veg", 50),
        new Ingredient("Roast Peppers", 50),
        new Ingredient("Baked beans", 50),
        new Ingredient("Pomegranate", 50)
    };

    private static List<Ingredient> Sauce => new()
    {
        new Ingredient("Mayo", 0),
        new Ingredient("Lemon Mayo", 15),
        new Ingredient("Garlic Mayo", 15),
        new Ingredient("Coronation Mayo", 25),
        new Ingredient("Salad Cream", 15),
        new Ingredient("Brown Sauce", 15),
        new Ingredient("Ketchup", 15),
        new Ingredient("English Mustard", 15),
        new Ingredient("Wholegrain Mustard", 15),
        new Ingredient("Dijon Mustard", 15),
        new Ingredient("Vulture mustard", 20),
        new Ingredient("Pickle", 20),
        new Ingredient("Sweet Chilli", 20),
        new Ingredient("Hot Chilli", 20),
        new Ingredient("Hoi Sin", 20),
        new Ingredient("Marie Rose", 20),
        new Ingredient("Horseradish", 20),
        new Ingredient("Raita", 25),
        new Ingredient("Caesar", 25),
        new Ingredient("Cranberry Sauce", 25),
        new Ingredient("Onion Marmalade", 25),
        new Ingredient("Tomato Chutney", 25),
        new Ingredient("Apple Chutney", 25),
        new Ingredient("Mango Chutney", 35),
        new Ingredient("Mint & Garlic Yoghurt", 25),
        new Ingredient("BBQ Sauce", 25),
        new Ingredient("Tomato Pesto", 35),
        new Ingredient("Basil Pesto", 35)
    };

    private static List<Ingredient> Extras => new()
    {
        new Ingredient("Croutons", 20),
        new Ingredient("Flax Seeds", 20),
        new Ingredient("Sesame Seeds", 20),
        new Ingredient("Gherkins", 30),
        new Ingredient("Capers", 30),
        new Ingredient("Jalapenos", 30),
        new Ingredient("Walnuts", 40),
        new Ingredient("Pinenuts", 50),
        new Ingredient("Mixed Leaf", 50),
        new Ingredient("Mixed Bean", 50),
        new Ingredient("Cous Cous", 50),
        new Ingredient("New Potato", 50),
        new Ingredient("Pasta", 50)
    };

    private static List<Ingredient> NonFilling => new()
    {
        new Ingredient("Houmous", 50),
        new Ingredient("Onion Bhaji", 75),
        new Ingredient("Veg Samosa", 75),
        new Ingredient("Meat Samosa", 80)
    };

    private static readonly Ingredient NullIngredient = new("", 0);

    private static List<Ingredient> Nothing => new();

    private Ingredient RandomChoice(params (List<Ingredient>, int)[] listOfProbabilities)
    {
        var totalOfProbabilities = listOfProbabilities.Sum(p => p.Item2);
        var totalIgnoredSoFar = 0;

        var chosen = random.Next(totalOfProbabilities);

        foreach (var (a, probability) in listOfProbabilities)
        {
            if (chosen < totalIgnoredSoFar + probability)
            {
                return a.Count > 0 ? a[random.Next(a.Count)] : NullIngredient;
            }

            totalIgnoredSoFar += probability;
        }

        return default;
    }

    public Sandwich Generate()
    {
        var ingredients = new[]
        {
            RandomChoice((Meats, 53), (Seafood, 37), (NonFilling, 6), (Nothing, 4)),
            RandomChoice((Veg, 100)),
            RandomChoice((Nothing, 80), (Veg, 20)),
            RandomChoice((Dairy, 70), (Nothing, 30)),
            RandomChoice((Sauce, 100)),
            RandomChoice((Extras, 15), (Nothing, 85))
        };

        var bread = RandomChoice((Bread, 100));

        return new Sandwich(bread, ingredients);
    }
}
