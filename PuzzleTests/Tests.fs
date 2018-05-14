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
        let tp = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        Assert.AreEqual(expected, tp.Thumbprint)

    [<TestMethod>] 
    member this.TestCompose() =
        let fn = fun f p -> f p

        let things = 
            firstNames 
            |> List.map (fun x -> newPerson x)
            |> outerProduct fn lastNames 
            |> outerProduct fn jackets 
            |> outerProduct fn shoes 

        Assert.AreEqual(256, things.Length)

        let filteredList = things |> List.filter rules
        Assert.AreEqual(69, filteredList.Length);

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

    [<TestMethod>]
    member this.SprintfTest() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let s1 = p1.toString
        printf "%s\n" s1
        printf "%s\n" "0000000000111111111122222222223333333333"
        printf "%s\n" "0123456789012345678901234567890123456789"

        Assert.AreEqual("    Amanda     Clark      Blue     Brown", s1);

    [<TestMethod>]
    member this.TestId1() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan
        let p3 = newPerson FirstName.Carol LastName.Meyer Jacket.Red Shoes.White
        let p3' = newPerson FirstName.Carol LastName.Meyer Jacket.Green Shoes.White
        let p4 = newPerson FirstName.Debbie LastName.Smith Jacket.Yellow Shoes.Black
        let expected = 0xffffu;

        let (actual, _) = Puzzle10.personListToTupple [p1;p2;p3;p4]
        Assert.AreEqual(expected, actual)

        let (actual, _) = Puzzle10.personListToTupple [p2;p1;p4;p3]
        Assert.AreEqual(expected, actual)

        let expected = 0xfbffu
        let (actual, _) = Puzzle10.personListToTupple [p1;p2;p3';p4]
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.TestRemove() =
        let p0 = newPerson FirstName.Amanda LastName.Clark Jacket.Green Shoes.Tan
        let p1 = newPerson FirstName.Carol LastName.Clark Jacket.Red Shoes.Tan
        let p2 = newPerson FirstName.Carol LastName.Clark Jacket.Red Shoes.Tan
        let p3 = newPerson FirstName.Amanda LastName.Meyer Jacket.Blue Shoes.Tan
        let p4 = newPerson FirstName.Belinda LastName.Clark Jacket.Green Shoes.Tan
        let p5 = newPerson FirstName.Carol LastName.Clark Jacket.Blue Shoes.Tan
        let p6 = newPerson FirstName.Amanda LastName.Johnson Jacket.Red Shoes.Brown
        let p7 = newPerson FirstName.Carol LastName.Clark Jacket.Green Shoes.Brown
        let p8 = newPerson FirstName.Belinda LastName.Clark Jacket.Red Shoes.Brown
        let p9 = newPerson FirstName.Belinda LastName.Clark Jacket.Blue Shoes.Tan
        let p10 = newPerson FirstName.Carol LastName.Meyer Jacket.Red Shoes.Black
        let p11 = newPerson FirstName.Amanda LastName.Meyer Jacket.Blue Shoes.Tan
        let p12 = newPerson FirstName.Carol LastName.Meyer Jacket.Blue Shoes.Black
        let p13 = newPerson FirstName.Amanda LastName.Clark Jacket.Green Shoes.Tan
        let p14 = newPerson FirstName.Carol LastName.Johnson Jacket.Red Shoes.Brown
        let p15 = newPerson FirstName.Belinda LastName.Johnson Jacket.Blue Shoes.Tan
        let p16 = newPerson FirstName.Carol LastName.Johnson Jacket.Blue Shoes.Black
        let p17 = newPerson FirstName.Amanda LastName.Meyer Jacket.Blue Shoes.Brown
        let p18 = newPerson FirstName.Amanda LastName.Meyer Jacket.Blue Shoes.Tan
        let p19 = newPerson FirstName.Amanda LastName.Meyer Jacket.Green Shoes.Brown

        // these are potential 'solutions'. The order of Person objects doesn't matter
        // and we want to remove duplicates. In this case s2 and s2' are actually the 
        // same solution, so one of them needs to be removed
        let s1 = [p1;p2;p3;p4]
        let s2 = [p1;p2;p3;p5]
        let s3 = [p6;p8;p12;p19]
        let s2'= [p5;p3;p1;p2]

        let l = [s1;s2;s3;s2']

        printSolutionList l
        let normalized = l |> distinct (fun v -> personListToTupple v)
        printf "*******************************\n"
        printSolutionList normalized
        Assert.AreEqual(3, normalized.Length)

    [<TestMethod>]
    member this.EndToEndTest() =
        let fn = fun f p -> f p
        let persons = 
            [newPerson]
            |> outerProduct fn firstNames
            |> outerProduct fn lastNames 
            |> outerProduct fn jackets 
            |> outerProduct fn shoes 

        Assert.AreEqual(256, persons.Length)

        let filteredList = persons |> List.filter rules
        Assert.AreEqual(69, filteredList.Length);
        printfn "Length of filteredList = %d" filteredList.Length
        let addPerson = buildlist filteredList >> List.filter personTest >> distinct personListToTupple

        // adding first person
        let l0 = [[]] |> addPerson
        printfn "Length of l0 = %d" l0.Length

        let l1 = l0 |> addPerson
        printfn "Length of l1 = %d" l1.Length

        let l2 = l1 |> addPerson
        printfn "Length of l2 = %d" l2.Length

        let l3 = l2 |> addPerson
        printfn "Length of l3 = %d" l3.Length

 
        l3|> printSolutionList 

        Assert.AreEqual(1, l3.Length)

    [<TestMethod>]
    member this.EndToEndTest'() =
        let fn = fun f p -> f p
        let persons = 
            [newPerson]
            |> outerProduct fn firstNames
            |> outerProduct fn lastNames 
            |> outerProduct fn jackets 
            |> outerProduct fn shoes 

        let filteredList = persons |> List.filter rules
        let addPerson = buildlist filteredList >> List.filter personTest >> distinct personListToTupple

        let solution =   [[]] |> addPerson |> addPerson |> addPerson |> addPerson
        solution |> printSolutionList 

        Assert.AreEqual(1, solution.Length)

    [<TestMethod>]
    member this.CalculateThumbprintTest() =
        let p1 = newPerson FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown
        let p2 = newPerson FirstName.Belinda LastName.Johnson Jacket.Green Shoes.Tan
        let p3 = newPerson FirstName.Carol LastName.Meyer Jacket.Red Shoes.White
        let p4 = newPerson FirstName.Debbie LastName.Smith Jacket.Yellow Shoes.Black

        let s = [p1; p2; p3; p4]

        let x = List.fold  (fun acc (p:Person) -> p.Thumbprint ||| acc) 0u s
        printfn "%x" x

        Assert.AreEqual(0xffffu, x)

    [<TestMethod>]
    member this.AlternativeToCombineTest() =
        let l1 = List.map newPerson firstNames
        let l2 = l1 |> List.map2 (fun p f -> (f p)) lastNames
        let l3 = l2 |> List.map2 (fun p f -> (f p)) jackets
        let l4 = l3 |> List.map2 (fun p f -> (f p)) shoes
        Assert.AreEqual(128, l4.Length);

        0
