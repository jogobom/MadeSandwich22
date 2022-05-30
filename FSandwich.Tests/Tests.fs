module FSandwich.Tests

open Xunit
open FsUnit.Xunit
open FSandwich.Sandwich
open System

[<Theory>]
[<InlineData(666, "Chicken, stuffing, Smoked Cheddar, Pickle (try it in a panini!)")>]
[<InlineData(123, "Smoked Salmon, WATERCRESS, Radish, hot chilli (try it in a Wrap!)")>]
let ``Sandwich description`` seed expectedSandwich =
    let rand = new Random(seed)
    let sandwich = make_sandwich rand
    let description = describe sandwich rand
    description |> should equal expectedSandwich

[<Theory>]
[<InlineData(666, 460)>]
[<InlineData(123, 405)>]
let ``Sandwich price`` seed expectedPrice =
    let rand = new Random(seed)
    let sandwich = make_sandwich rand
    let pence = price sandwich
    pence |> should equal expectedPrice
    