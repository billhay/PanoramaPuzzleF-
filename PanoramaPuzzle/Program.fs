open Library
open Puzzle10

[<EntryPoint>]
let main argv =

    let fn = fun f p -> f p

    let sw = new System.Diagnostics.Stopwatch()
    sw.Start()

    let persons = 
            [newPerson]
            |> outerProduct fn firstNames
            |> outerProduct fn lastNames 
            |> outerProduct fn jackets 
            |> outerProduct fn shoes 

    let filteredList = persons |> List.filter rules
    let addPerson = buildlist filteredList >> List.filter personTest >> distinct personListToTupple

    let solution =   [[]] |> addPerson |> addPerson |> addPerson |> addPerson
    sw.Stop();

    solution |> printSolutionList 
    printfn "Elapsed time = %d msecs" sw.ElapsedMilliseconds
    0
