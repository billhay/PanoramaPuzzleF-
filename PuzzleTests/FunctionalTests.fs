module FunctionalTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Library

type Name = 
    | Jack = 1
    | Jill = 2

let names = [ Name.Jack; Name.Jill ]

type Pet =
    | Dog = 4
    | Cat = 8

let pets = [ Pet.Dog; Pet.Cat; ]

type TestPerson =
    {
        Name:Name;
        Pet:Pet;
        Thumbprint: uint32
    }

let makeTestPerson (name:Name) (pet:Pet) = 
    {
        Name = name;
        Pet = pet;
        Thumbprint = ((uint32)name) + ((uint32)pet);
    }
        
let testRules (p:TestPerson)  = 
    not (p.Name = Name.Jack) && (p.Pet = Pet.Cat)

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.CreateTestPerson() =
        let p = makeTestPerson Name.Jack Pet.Dog
        Assert.AreEqual(5u, p.Thumbprint)

    [<TestMethod>]
    member this.CombineTest() =
        let things = 
            [makeTestPerson] 
            |> combine names 
            |> combine pets 

        Assert.AreEqual(4, things.Length)
