// Learn more about F# at http://fsharp.org

open System

let divide x y = x/y
let sum x y = x+y
let difinition x y = x-y
let multi x y = x*y

let calculate op x y =
    match op with 
    |"+" -> divide x y
    |"-" -> difinition x y
    |"*" -> multi x y
    |"/" -> divide x y
    | _ -> raise(System.NotImplementedException("NotCompFunc"))


[<EntryPoint>]
let main argv =
    let x  = Console.ReadLine() |> Int32.Parse
    let op  = Console.ReadLine() 
    let y  = Console.ReadLine() |> Int32.Parse
    Console.WriteLine(calculate op x y)
    0 // return an integer exit code
