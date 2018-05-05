// Learn more about F# at http://fsharp.org

open System
open Library
open Puzzle10

[<EntryPoint>]
let main argv =
    let things = 
        [ newPerson] 
        |> combine firstNames 
        |> combine lastNames 
        |> combine jackets 
        |> combine shoes 
        |> filter rules

    printf "%d" (List.length things)

    0
