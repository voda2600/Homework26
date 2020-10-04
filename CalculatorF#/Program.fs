// Learn more about F# at http://fsharp.org

open System

type MaybeBuilder() =
    member this.Bind(x,f)=
        match x with
        |None -> None
        |Some a -> f a
    member this.Return(x)=
        Some x

let maybe = new MaybeBuilder()


let divide x y = 
   match y with
   | 0 -> None
   | _ -> Some (x / y)

let checkNull x=
    match x with
    |Some(x) -> printfn x
    |None -> printfn 

let calculate op x y =
    maybe{
    let! result =
        match op with 
        |"+" -> Some(x+y)
        |"-" -> Some(x-y)
        |"*" -> Some(x*y)
        |"/" -> divide x y
        |_ -> None
    return result
    }
        
let write (x:int option) = 
    if x=None then Console.WriteLine("None")
    else Console.WriteLine(x.Value)


[<EntryPoint>]
let main argv =
    let x  = Console.ReadLine() |> Int32.Parse
    let op  = Console.ReadLine() 
    let y  = Console.ReadLine() |> Int32.Parse
    let result = calculate op x y
    write result
    0 // return an integer exit code
