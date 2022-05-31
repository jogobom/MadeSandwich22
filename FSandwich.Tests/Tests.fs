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
    let json_reader = IO.File.ReadAllText
    let sandwich = make_sandwich rand json_reader
    let description = describe sandwich rand json_reader
    description |> should equal expectedSandwich

[<Theory>]
[<InlineData(666, 460)>]
[<InlineData(123, 405)>]
let ``Sandwich price`` seed expectedPrice =
    let rand = new Random(seed)
    let json_reader = IO.File.ReadAllText
    let sandwich = make_sandwich rand json_reader
    let pence = price sandwich
    pence |> should equal expectedPrice
    