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

let private GetAnswer(a:float,b:float,oper) =
       async{
           let req = WebRequest.Create("http://localhost:53881?a="+a.ToString()+"&b="+b.ToString()+"&oper="+oper+"", Method = "GET", ContentType = "text/plain")
           let rsp = req.GetResponse()
           let sr = rsp.GetResponseStream()
           let rdr = new StreamReader(sr)
           return rdr.ReadToEnd()
       }
module Calculator=

    let masync = new AsyncMaybeBuilder()
    
    let public ChangeOper(oper) = 
        match oper with
        | "+" -> "%2B"
        | "*" -> "*"
        | "/" -> "%2F"
        | "-" -> "-"
        | _ -> ""
    
    let public Calculate(a:float,b:float,oper)=
        let oper = ChangeOper(oper)
        let ans = Async.RunSynchronously(GetAnswer(a,b,oper))
        ans



        
let write (x:int option) = 
    if x=None then Console.WriteLine("None")
    else Console.WriteLine(x.Value)

[<ExcludeFromCodeCoverage>]            
   module Program =
       [<EntryPoint>]
       let main argv =
           Console.WriteLine("Give a operation b  for calculator");
           let a = Convert.ToDouble(Console.ReadLine())
           let oper = Console.ReadLine()
           let b = Convert.ToDouble(Console.ReadLine())
           let ans  = Calculator.Calculate(a,b,oper)
           printf"%s"(ans)
           0