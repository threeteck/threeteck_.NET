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
    x + y
        
let subtract x y =
    x - y
        
let divide x y =
    match y with
    | 0.0 -> None
    | _ -> Some(x/y)

let multiply x y =
    x * y
        
let calculate op x y =
            match op with
            | "+" -> Some(add x y)
            | "*" -> Some(multiply x y)
            | "-" -> Some(subtract x y)
            | "/" -> divide x y
            | _ -> None
    
[<EntryPoint>]
let main argv =
    let a = Console.ReadLine() |> Double.TryParse |> tryParseResultToOption
    let op = Console.ReadLine()
    let b = Console.ReadLine() |> Double.TryParse |> tryParseResultToOption
    let result = maybe {
            let! x = a
            let! y = b
            let! calc = calculate op x y
            return calc
        }
    match result with
    | None -> printf "Error occurred"
    | Some v -> printf "Result: %f" v
    0
