module Tests

open System
open Microsoft.VisualStudio.TestPlatform.TestHost
open Xunit

[<Fact>]
let ``divide 6/2`` () =
    Assert.Equal(Some(6/2), Program.divide 6 2)
[<Fact>]
let ``divide 6/0`` () =
    Assert.Equal(None, Program.divide 6 0)
[<Fact>]
let ``calculate 5+5`` () =
    Assert.Equal(Some(10), Program.calculate "+" 5 5)
[<Fact>]    
let ``calculate 5*5`` () =
    Assert.Equal(Some(25), Program.calculate "*" 5 5)
[<Fact>]
let ``calculate 5/5`` () =
    Assert.Equal(Some(1), Program.calculate "/" 5 5)
[<Fact>]
let ``calculate 5-5`` () =
    Assert.Equal(Some(0), Program.calculate "-" 5 5)
[<Fact>]
let ``calculate "&" 5 5`` () =
    Assert.Equal(None, Program.calculate "&" 5 5)
[<Fact>]
let ``Console write 5`` () =
    Assert.Equal(Program.write(Some 5), System.Console.Write(5))

[<Fact>]
let ``Console write None`` () =
    Assert.Equal(Program.write(None), System.Console.Write("None"))

