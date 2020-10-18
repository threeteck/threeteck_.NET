open System
open System.IO
open FSharp.Data

let tryParseResultToOption (a:bool * _) =
    match a with
    | true, v -> Some v
    | false, _ -> None

type AsyncMaybeBuilder() =
    member this.Bind(x, f) =
        async {
            let! x' = x
            match x' with
            | Some v -> return! f v
            | None -> return None
        }

    member this.Return x = async { return x }

let asyncMaybe = AsyncMaybeBuilder()

let getResponse requestUrl=
    async{
        let! response = Http.AsyncRequestStream(requestUrl, silentHttpErrors = true)
        return Some response
    }
    
let getBody (bodyStream : Stream) =
    async{
        let responseReader = new StreamReader(bodyStream)
        let! responseBody = responseReader.ReadToEndAsync() |> Async.AwaitTask
        return Some responseBody
    }

let getCalculationResult requestUrl =
    Async.RunSynchronously(
          asyncMaybe{
              let! response = getResponse requestUrl
              let! status = async{
                      return match response.StatusCode with
                                | 500 -> Some "Server error"
                                | 400 -> Some "Bad request"
                                | 200 -> Some "Calculation result"
                                | _ -> None
                  }
              
              let! responseBody = getBody response.ResponseStream
              let result = sprintf "%s: %s" status responseBody
              
              return Some result
          }                    
    )
    
[<EntryPoint>]
let main argv =
    let url = "http://localhost:5000"
    let result =
        match argv.Length with
        | 1 ->  let query = argv.[0].Replace("+", "%2B")
                getCalculationResult (sprintf "%s?query=%s" url query)
        | 3 ->  let a = argv.[0]
                let op = argv.[1].Replace("+", "%2B")
                let b = argv.[2]
                getCalculationResult (sprintf "%s?firstNumber=%s&op=%s&secondNumber=%s" url a op b)
        | _ -> Some (sprintf "This command does not accept %d arguments" argv.Length)
        
    match result with
        | Some v -> printf "%s" v
        | None -> printf "Unexpected error"
    0
