namespace PuzzleLibTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass () =
    [<TestMethod>]
    member this.ShowDate() =
        Assert.Fail (System.DateTime.Now.ToString())
