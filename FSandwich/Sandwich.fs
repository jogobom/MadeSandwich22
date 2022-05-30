namespace FSandwich

open System
open Newtonsoft.Json

module Sandwich =
    type Ingredient = {Name:string; PriceInPence:int}
    type Sandwich = {Bread:Ingredient; Ingredients:Ingredient seq}
    type BreadAdvice = {Advice:string}

    let private random_caps (rand:Random) ingredient =
        match rand.Next 100 with
        | r when r <= 5 -> { Name = ingredient.Name.ToUpper(); PriceInPence = ingredient.PriceInPence }
        | r when r <= 40 -> { Name = ingredient.Name.ToLower(); PriceInPence = ingredient.PriceInPence }
        | _ -> ingredient

    let private read_from_json<'T> path =
        let json_text = IO.File.ReadAllText path
        JsonConvert.DeserializeObject<'T list> json_text

    let private random_item json_file (rand:Random) =
        let lst = read_from_json json_file
        let item_chooser = List.length lst |> rand.Next |> List.item
        item_chooser lst

    let private bread_advice rand =
        random_item "bread_advice.json" rand

    let private random_bread rand =
        random_item "bread.json" rand |> random_caps rand

    let private random_dairy rand =
        random_item "dairy.json" rand |> random_caps rand

    let private random_extras rand =
        random_item "extras.json" rand |> random_caps rand

    let private random_meat rand =
        random_item "meat.json" rand |> random_caps rand

    let private random_nonfilling rand =
        random_item "nonfilling.json" rand |> random_caps rand

    let private random_sauce rand =
        random_item "sauce.json" rand |> random_caps rand

    let private random_seafood rand =
        random_item "seafood.json" rand |> random_caps rand

    let private random_veg rand =
        random_item "veg.json" rand |> random_caps rand

    let private meat_layer (rand:Random) =
        match rand.Next 100 with
        | r when r <= 53 -> Some(random_meat rand)
        | r when r <= 90 -> Some(random_seafood rand)
        | r when r <= 96 -> Some(random_nonfilling rand)
        | _ -> None

    let private veg_layer1 rand =
        Some(random_veg rand)

    let private veg_layer2 (rand:Random) =
        match rand.Next 100 with
        | r when r <= 20 -> Some(random_veg rand)
        | _ -> None

    let private dairy_layer (rand:Random) =
        match rand.Next 100 with
        | r when r <= 70 -> Some(random_dairy rand)
        | _ -> None

    let private sauce_layer rand =
        Some(random_sauce rand)

    let private extras_layer (rand:Random) =
        match rand.Next 100 with
        | r when r <= 15 -> Some(random_extras rand)
        | _ -> None

    let private choose_ingredients rand =
        seq {
            (meat_layer rand)
            (veg_layer1 rand)
            (veg_layer2 rand)
            (dairy_layer rand)
            (sauce_layer rand)
            (extras_layer rand)
        }
        |> Seq.choose id
    
    let private add_bread filling rand =
        {Bread = random_bread rand; Ingredients = filling}

    let make_sandwich rand =
        let ingredients = choose_ingredients rand
        add_bread ingredients rand

    let describe sandwich rand = 
        let filling = sandwich.Ingredients |> Seq.map (fun i -> i.Name) |> String.concat ", "
        let bread_advice = bread_advice rand
        $"{filling} ({bread_advice.Advice} {sandwich.Bread.Name}!)"

    let price sandwich =
        sandwich.Bread.PriceInPence + (sandwich.Ingredients |> Seq.map (fun i -> i.PriceInPence) |> Seq.sum)
