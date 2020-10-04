module Tests

open System
open Xunit
open Program
[<Fact>]
let ``divide 6/2`` () =
    Assert.Equal(Some(6/2), Program.divide 6 2)
[<Fact>]
let ``divide 6/0`` () =
    Assert.Equal(None, Program.divide 6 0)
[<Fact>]
let ``calculate 5+5`` () =
    Assert.Equal(Some(10), Program.calculate "+" 5 5)
let ``calculate 5*5`` () =
    Assert.Equal(Some(25), Program.calculate "*" 5 5)
let ``calculate 5/5`` () =
    Assert.Equal(Some(1), Program.calculate "/" 5 5)
let ``calculate 5-5`` () =
    Assert.Equal(Some(0), Program.calculate "-" 5 5)
let ``calculate "&" 5 5`` () =
    Assert.Equal(None, Program.calculate "&" 5 5)

