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


    let personFilter = List.filter personTest

    let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
    let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan
    let p3 = newPerson FirstName.Carol LastName.Meyer Jacket.Red Shoes.White
    let p4 = newPerson FirstName.Debbie LastName.Smith Jacket.Yellow Shoes.Black

    let g = [p1; p2; p3; p4];

    let b = personTest g

    //let s = List.take 6 things

    //let a = [[]] |> buildlist s |> buildlist s

    //let b = a |> personFilter;



    0
