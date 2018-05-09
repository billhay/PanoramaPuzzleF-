// Learn more about F# at http://fsharp.org

open System
open TestPerson2
open Library

[<EntryPoint>]
let main argv =

    let things = 
        [makeTestPerson] 
        |> combine names 
        |> combine pets 

    printTestPersonList things


    printf "1 #########################################\n\n"

    let filteredThings = things |> List.filter testRules

    printTestPersonList filteredThings


    let solutions =  [[]] |> buildlist filteredThings |> buildlist (List.tail filteredThings)

    printf "2 ****************************************\n"

    printTestPersonListList solutions 

    let foo = List.filter personTest

    printf "3 ****************************************\n"

    solutions |> foo |> printTestPersonListList

    0 // return an integer exit code
