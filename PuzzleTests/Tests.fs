namespace PuzzleTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Puzzle10
open Library

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestThumbPrint() =
        let expected:UInt32 = 0x1111u
        let tp = (thumbprint FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown)
        Assert.AreEqual(expected, tp)

    [<TestMethod>] 
    member this.TestCompose() =
        let things = 
            [ newPerson] 
            |> combine firstNames 
            |> combine lastNames 
            |> combine jackets 
            |> combine shoes 
            |> filter rules

        Assert.AreEqual(69, things.Length)

    [<TestMethod>] 
    member this.PersonTestTrueTest() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan
        let p3 = newPerson FirstName.Carol LastName.Meyer Jacket.Red Shoes.White
        let p4 = newPerson FirstName.Debbie LastName.Smith Jacket.Yellow Shoes.Black

        let actual = personTest [p1; p2; p3; p4]

        Assert.IsTrue(actual)

    [<TestMethod>] 
    member this.PersonTestFalseTest() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan
        let p3 = newPerson FirstName.Carol LastName.Meyer Jacket.Blue Shoes.White
        let p4 = newPerson FirstName.Debbie LastName.Smith Jacket.Yellow Shoes.Black

        let actual = personTest [p1; p2; p3; p4]

        Assert.IsFalse(actual)

    [<TestMethod>] 
    member this.PersonFilterRetainTest() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan
        let p3 = newPerson FirstName.Carol LastName.Meyer Jacket.Red Shoes.White
        let p4 = newPerson FirstName.Debbie LastName.Smith Jacket.Yellow Shoes.Black

        let s = [[p1; p2; p3; p4]; [p1; p2; p3; p4]; [p1; p2; p3; p4]]

        let personFilter = List.filter personTest

        let filteredList = s |> personFilter

        Assert.AreEqual(3, filteredList.Length)

    [<TestMethod>] 
    member this.PersonFilterRemoveOneTest() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan
        let p3 = newPerson FirstName.Carol LastName.Meyer Jacket.Red Shoes.White
        let p3' = newPerson FirstName.Carol LastName.Meyer Jacket.Green Shoes.White
        let p4 = newPerson FirstName.Debbie LastName.Smith Jacket.Yellow Shoes.Black

        let s = [[p1; p2; p3; p4]; [p1; p2; p3'; p4]; [p1; p2; p3; p4]]

        let personFilter = List.filter personTest

        let filteredList = s |> personFilter

        Assert.AreEqual(2, filteredList.Length)

    [<TestMethod>] 
    member this.PersonFilterRemoveAllButOneTest() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan
        let p3 = newPerson FirstName.Carol LastName.Meyer Jacket.Red Shoes.White
        let p3' = newPerson FirstName.Carol LastName.Meyer Jacket.Green Shoes.White
        let p4 = newPerson FirstName.Debbie LastName.Smith Jacket.Yellow Shoes.Black

        let s = [[p1; p2; p3'; p4]; [p1; p2; p3'; p4]; [p1; p2; p3; p4]; [ p1; p2; p3'; p4]; ]

        let personFilter = List.filter personTest

        let filteredList = s |> personFilter

        Assert.AreEqual(1, filteredList.Length)

    [<TestMethod>] 
    member this.BuildList1Test() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan

        let s = [p1; p2;]
        let t = [[]] |> buildlist s;
        Assert.AreEqual(2, t.Length);

    [<TestMethod>] 
    member this.BuildList2Test() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan

        let s = [p1; p2;]
        let t = [[]] |> buildlist s |> buildlist s
        Assert.AreEqual(4, t.Length);

 