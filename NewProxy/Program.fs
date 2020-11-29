// Learn more about F# at http://fsharp.org

open System
open System.Diagnostics.CodeAnalysis
open System.Net
open System.Text
open System.IO
open System.Threading





[<ExcludeFromCodeCoverage>]
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
let masync = new AsyncMaybeBuilder()

let public StatusCode(responce:HttpWebResponse) = 
    async{
        return 
            match Convert.ToInt32(responce.StatusCode) with
            |200->let s = responce.GetResponseStream()
                  let rdr = new StreamReader(s)
                  rdr.ReadToEnd()|>Some
            |400 -> None
            |500 ->None
            |_ -> None
    }

let private GetAnswer(a,b,oper) =
       masync{
                let req = HttpWebRequest.Create("http://localhost:53881?a="+a+"&b="+b+"&oper="+oper+"", Method = "GET", ContentType = "text/plain")
                let rsp = req.GetResponse() :?> HttpWebResponse
                let! statusAns = StatusCode(rsp)
                return Some(statusAns)
       }
module Calculator=

    
    let public ChangeOper(oper) = 
        async{
        match oper with
        | "+" -> return "%2B"
        | "*" -> return "*"
        | "/" -> return "%2F"
        | "-" ->return "-"
        | _ ->return ""
        }

    let public Calculate(a,b,oper)=
        async{
        let! oper = ChangeOper(oper)
        let! ans = GetAnswer(a,b,oper)
        return ans
        }


module ProxyCutting=
    let public Cut (a:string) (b:string) (oper:string) proxy=
        let res  = (a,b,oper)|> proxy |> Async.RunSynchronously
        match res with 
        |Some s -> s
        |None -> "Error"
        


[<ExcludeFromCodeCoverage>]            
   module Program =
       [<EntryPoint>]
       let main argv =
           Console.WriteLine("Give a operation b  for calculator");
           let a = Console.ReadLine()
           let oper = Console.ReadLine()
           let b = Console.ReadLine()
           let proxyAns = ProxyCutting.Cut a b oper
           printfn "%s"(proxyAns Calculator.Calculate)
           0