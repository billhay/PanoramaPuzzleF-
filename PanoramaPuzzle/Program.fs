open Library
open Puzzle10

[<EntryPoint>]
let main argv =
    let persons = 
        [ newPerson] 
        |> combine firstNames 
        |> combine lastNames 
        |> combine jackets 
        |> combine shoes 

    let filteredList = persons |> List.filter rules
    let addPerson = buildlist filteredList >> List.filter personTest >> distinct personListToTupple

    let l0 = filteredList |> List.map (fun p -> [p])

    let solution =   l0 |> addPerson |> addPerson |> addPerson
    solution |> printSolutionList 

    0
