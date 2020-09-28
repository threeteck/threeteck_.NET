open System

let tryParseResultToOption (a:bool * _) =
    match a with
    | true, v -> Some v
    | false, _ -> None

type MaybeBuilder() =

    member this.Bind(x, f) = 
        match x with
        | None -> None
        | Some a -> f a

    member this.Return(x) = 
        Some x
   
let maybe = new MaybeBuilder()

let add x y =
    maybe
        {
        let! a = x
        let! b = y
        return a + b
        }
        
let subtract x y =
    maybe
        {
        let! a = x
        let! b = y
        return a - b
        }
        
let divide x y =
    maybe
        {
        let! a = x
        let! b = 
            match y with
            | Some 0.0 | None -> None
            | Some c -> Some c
        return a / b
        }

let multiply x y =
    maybe
        {
        let! a = x
        let! b = y
        return a * b
        }
        
let calculate op x y =
            match op with
            | "+" -> add x y
            | "*" -> multiply x y
            | "-" -> subtract x y
            | "/" -> divide x y
            | _ -> None
    
[<EntryPoint>]
let main argv =
    let a = Console.ReadLine() |> Double.TryParse |> tryParseResultToOption
    let op = Console.ReadLine()
    let b = Console.ReadLine() |> Double.TryParse |> tryParseResultToOption
    let result = calculate op a b
    match result with
    | None -> printf "Error occurred"
    | Some a -> printf "Result: %f" a
    0
