namespace PuzzleTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Puzzle10

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestThumbPrint() =
        let expected:UInt32 = 0x1111u
        let tp = (thumbprint FirstName.Amanda LastName.Clark Jacket.Blue Shoes.Brown)
        Assert.AreEqual(expected, tp)
