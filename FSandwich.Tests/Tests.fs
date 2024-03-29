﻿module FSandwich.Tests

open Xunit
open FsUnit.Xunit
open FSandwich.Sandwich
open System

[<Theory>]
[<InlineData(666, "Chicken Tikka, stuffing, Smoked Cheddar, pickle (try it in a panini!)")>]
[<InlineData(123, "Smoked Salmon, watercress, Radish, hot chilli (try it in a Wrap!)")>]
let ``Sandwich description`` seed expectedSandwich =
    let rand = new Random(seed)
    let json_reader = IO.File.ReadAllText
    let sandwich = make_sandwich rand json_reader
    let description = describe sandwich rand json_reader
    description |> should equal expectedSandwich

[<Theory>]
[<InlineData(666, 465)>]
[<InlineData(123, 405)>]
let ``Sandwich price`` seed expectedPrice =
    let rand = new Random(seed)
    let json_reader = IO.File.ReadAllText
    let sandwich = make_sandwich rand json_reader
    let pence = price sandwich
    pence |> should equal expectedPrice

[<Fact>]
let ``Pastrami, beetroot, Roast Peppers, CHEESE SAVOURY, ketchup, Pasta (delicious in a baguette!)`` () =
    let sandwich = {Bread = {Name = "Baguette"; PriceInPence = 240}; Ingredients = [{Name = "Pastrami"; PriceInPence = 80}; {Name = "Beetroot"; PriceInPence = 30}; {Name = "Roast Peppers"; PriceInPence = 60}; {Name = "CHEESE SAVOURY"; PriceInPence = 60}; {Name = "ketchup"; PriceInPence = 15}; {Name = "Pasta"; PriceInPence = 50}]}
    let pence = price sandwich
    pence |> should equal 535

[<Fact>]
let ``King Prawns, Apple, spinach, Parmesan, Brown Sauce (perfect for a Seeded(!!?!) NOT A STOTTIE!)`` () =
    let sandwich = {
        Bread = {
            Name = "a Seeded(!!?!) NOT A STOTTIE"; PriceInPence = 240
        };
        Ingredients = [
            {Name = "King Prawns"; PriceInPence = 115};
            {Name = "Apple"; PriceInPence = 30};
            {Name = "spinach"; PriceInPence = 15};
            {Name = "Parmesan"; PriceInPence = 70};
            {Name = "Brown Sauce"; PriceInPence = 15}
        ]
    }
    let pence = price sandwich
    pence |> should equal 485
