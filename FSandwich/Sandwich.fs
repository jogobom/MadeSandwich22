namespace FSandwich

open System
open Newtonsoft.Json

module Sandwich =
    type Ingredient = {Name:string; PriceInPence:int}
    type Sandwich = {Bread:Ingredient; Ingredients:Ingredient list}
    type BreadAdvice = {Advice:string}

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
        random_item "bread.json" rand

    let private random_dairy rand =
        random_item "dairy.json" rand

    let private random_extras rand =
        random_item "extras.json" rand

    let private random_meat rand =
        random_item "meat.json" rand

    let private random_nonfilling rand =
        random_item "nonfilling.json" rand

    let private random_sauce rand =
        random_item "sauce.json" rand

    let private random_seafood rand =
        random_item "seafood.json" rand

    let private random_veg rand =
        random_item "veg.json" rand

    let private choose_ingredients rand =
        [random_meat rand]
    
    let private add_bread filling rand =
        {Bread = random_bread rand; Ingredients = filling}

    let make_sandwich rand =
        let ingredients = choose_ingredients rand
        add_bread ingredients rand

        // var ingredients = new[]
        //     {
        //         RandomChoice((Meats, 53), (Seafood, 37), (NonFilling, 6), (Nothing, 4)),
        //         RandomChoice((Veg, 100)),
        //         RandomChoice((Nothing, 80), (Veg, 20)),
        //         RandomChoice((Dairy, 70), (Nothing, 30)),
        //         RandomChoice((Sauce, 100)),
        //         RandomChoice((Extras, 15), (Nothing, 85))
        //     };

    let describe sandwich rand = 
        let filling = sandwich.Ingredients |> List.map (fun i -> i.Name) |> String.concat ", "
        let bread_advice = bread_advice rand
        $"{filling} ({bread_advice.Advice} {sandwich.Bread.Name}!)"

    let price sandwich =
        sandwich.Bread.PriceInPence + (sandwich.Ingredients |> List.map (fun i -> i.PriceInPence) |> List.sum)
