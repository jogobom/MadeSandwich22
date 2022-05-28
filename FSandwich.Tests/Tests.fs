module FSandwich.Tests

open Xunit
open FsUnit.Xunit
open FSandwich.Sandwich
open System

[<Theory>]
[<InlineData(666, "Smoked Turkey (delicious in a Wholemeal Stottie!)")>]
[<InlineData(123, "Parma Ham (perfect for Sliced Bread!)")>]
let ``Sandwich description`` seed expectedSandwich =
    let rand = new Random(seed)
    let sandwich = make_sandwich rand
    let description = describe sandwich rand
    description |> should equal expectedSandwich

[<Theory>]
[<InlineData(666, 270)>]
[<InlineData(123, 290)>]
let ``Sandwich price`` seed expectedPrice =
    let rand = new Random(seed)
    let sandwich = make_sandwich rand
    let pence = price sandwich
    pence |> should equal expectedPrice
    