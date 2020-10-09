// Learn more about F# at http://fsharp.org

open System
open System.Diagnostics.CodeAnalysis
open System.Net
open System.Text
open System.IO
open System.Text


[<ExcludeFromCodeCoverage>]
type MaybeBuilder() =
    member this.Bind(x,f)=
        match x with
        |None -> None
        |Some a -> f a
    member this.Return(x)=
        Some x

let maybe = new MaybeBuilder()

type AsyncMaybeBuilder () =
    member this.Bind(x, f) =
        async {
            let! _x = x
            match _x with
            | Some s -> return! f s
            | None -> return None
            }

    member this.Return(x:'a option) =
        async{return x}
module Calculator=

    let masync = new AsyncMaybeBuilder()

    let public GetAnswer() =
           async{
               let req = WebRequest.Create("http://localhost:53881?a=5&b=10&oper=*", Method = "GET", ContentType = "text/plain")
               let rsp = req.GetResponse()
               let sr = rsp.GetResponseStream()
               let rdr = new StreamReader(sr)
               return rdr.ReadToEnd()
           }

        
let write (x:int option) = 
    if x=None then Console.WriteLine("None")
    else Console.WriteLine(x.Value)

[<ExcludeFromCodeCoverage>]            
   module Program =
       [<EntryPoint>]
       let main argv =
           //let a = Console.ReadLine()
           //let b = Console.ReadLine()
           //let oper = Console.ReadLine()
           let ans  = Async.RunSynchronously(Calculator.GetAnswer())
           printf"%s"(ans)
           0