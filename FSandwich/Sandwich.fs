namespace FSandwich

open System
open Newtonsoft.Json

module Sandwich =
    type Ingredient = {Name:string; PriceInPence:int}
    type Sandwich = {Bread:Ingredient; Ingredients:Ingredient seq}
    type BreadAdvice = {Advice:string}

    let private random_caps (rand:Random) ingredient =
        match rand.Next 100 with
        | r when r <= 50 -> { Name = ingredient.Name.ToLower(); PriceInPence = ingredient.PriceInPence }
        | _ -> ingredient

    let private read_from_json<'T> path json_reader =
        // let reader = IO.File.ReadAllText
        let json_text = json_reader path
        JsonConvert.DeserializeObject<'T list> json_text

    let private random_item json_file (rand:Random) json_reader =
        let lst = read_from_json json_file json_reader
        let item_chooser =
            let length = List.length lst
            let chosen = rand.Next length
            List.item chosen
        item_chooser lst

    let private bread_advice rand json_reader =
        random_item "bread_advice.json" rand json_reader

    let private random_bread rand json_reader =
        random_item "bread.json" rand json_reader |> random_caps rand

    let private random_dairy rand json_reader =
        random_item "dairy.json" rand json_reader |> random_caps rand

    let private random_extras rand json_reader =
        random_item "extras.json" rand json_reader |> random_caps rand

    let private random_meat rand json_reader =
        random_item "meat.json" rand json_reader |> random_caps rand

    let private random_nonfilling rand json_reader =
        random_item "nonfilling.json" rand json_reader |> random_caps rand

    let private random_sauce rand json_reader =
        random_item "sauce.json" rand json_reader |> random_caps rand

    let private random_seafood rand json_reader =
        random_item "seafood.json" rand json_reader |> random_caps rand

    let private random_veg rand json_reader =
        random_item "veg.json" rand json_reader |> random_caps rand

    let private meat_layer (rand:Random) json_reader =
        match rand.Next 100 with
        | r when r <= 53 -> Some(random_meat rand json_reader)
        | r when r <= 90 -> Some(random_seafood rand json_reader)
        | r when r <= 96 -> Some(random_nonfilling rand json_reader)
        | _ -> None

    let private veg_layer1 rand json_reader =
        Some(random_veg rand json_reader)

    let private veg_layer2 (rand:Random) json_reader =
        match rand.Next 100 with
        | r when r <= 20 -> Some(random_veg rand json_reader)
        | _ -> None

    let private dairy_layer (rand:Random) json_reader =
        match rand.Next 100 with
        | r when r <= 70 -> Some(random_dairy rand json_reader)
        | _ -> None

    let private sauce_layer rand json_reader =
        Some(random_sauce rand json_reader)

    let private extras_layer (rand:Random) json_reader =
        match rand.Next 100 with
        | r when r <= 15 -> Some(random_extras rand json_reader)
        | _ -> None

    let private choose_ingredients rand json_reader =
        seq {
            (meat_layer rand json_reader)
            (veg_layer1 rand json_reader)
            (veg_layer2 rand json_reader)
            (dairy_layer rand json_reader)
            (sauce_layer rand json_reader)
            (extras_layer rand json_reader)
        }
        |> Seq.choose id
    
    let private add_bread filling rand json_reader =
        {Bread = random_bread rand json_reader; Ingredients = filling}

    let make_sandwich rand json_reader =
        let ingredients = choose_ingredients rand json_reader
        add_bread ingredients rand json_reader

    let describe sandwich rand json_reader = 
        let filling = sandwich.Ingredients |> Seq.map (fun i -> i.Name) |> String.concat ", "
        let bread_advice = bread_advice rand json_reader
        $"{filling} ({bread_advice.Advice} {sandwich.Bread.Name}!)"

    let price sandwich =
        sandwich.Bread.PriceInPence + (sandwich.Ingredients |> Seq.map (fun i -> i.PriceInPence) |> Seq.sum)
    