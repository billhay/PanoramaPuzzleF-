module FunctionalTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Library



[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.CreateTestPerson() =
        let p = makeTestPerson Name.Jack Pet.Dog
        Assert.AreEqual(5u, p.Thumbprint)

//    [<TestMethod>]
//    member this.CombineTest() =
//        let things = 
//            [makeTestPerson] 
//            |> combine names 
//            |> combine pets 

//        Assert.AreEqual(4, things.Length)
