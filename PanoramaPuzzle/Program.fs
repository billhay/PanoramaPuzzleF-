open Library
open Puzzle10

[<EntryPoint>]
let main argv =

    let fn = fun f p -> f p

    let persons = 
        firstNames 
            |> List.map (fun x -> newPerson x)
            |> outerProduct fn lastNames 
            |> outerProduct fn jackets 
            |> outerProduct fn shoes 

    let filteredList = persons |> List.filter rules
    let addPerson = buildlist filteredList >> List.filter personTest >> distinct personListToTupple

    let l0 = filteredList |> List.map (fun p -> [p])

    let solution =   l0 |> addPerson |> addPerson |> addPerson
    solution |> printSolutionList 

    0
