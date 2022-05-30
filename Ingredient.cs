namespace jogobom.MadeSandwich;

public class Ingredient
{
    public Ingredient(string name, int priceInPence)
    {
        Name = name;
        PriceInPence = priceInPence;
    }

    public string Name { get;}
    public int PriceInPence { get;}
}    