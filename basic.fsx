#load "./paket-files/include-scripts/net46/include.suave.fsx"
#load "./paket-files/include-scripts/net46/include.chiron.fsx"

type Operands = {
    left: int
    right: int
}

type Result = {
    result: int
}

open Chiron

type Operands with 
    static member FromJson(_:Operands) = json {
        let! l = Json.read "left"
        let! r = Json.read "right"
        return {left = l; right = r}
    }
    static member ToJson(o:Operands) = json {
        do! Json.write "left" o.left
        do! Json.write "right" o.right
    }

type Result with
    static member FromJson(_:Result) = json {
        let! r = Json.read "result"
        return {result = r}
    }
    static member ToJson(r:Result) = json {
        do! Json.write "result" r.result
    }

let inline fromJson1 (bytes : byte []) =
    UTF8.toString bytes |> Json.parse |> Json.deserialize

let inline toJson1 o =
    Json.serialize o |> Json.format |> UTF8.bytes

open Suave.Json

let mapJson = mapJsonWith fromJson1 toJson1

open Suave
open Suave.Filters
open Suave.Operators

let app =
    choose [
        POST >=> choose [
            path "/add" >=> mapJson (fun o -> { result = o.left + o.right })
            path "/multiply" >=> mapJson (fun o -> { result = o.left * o.right })
        ]
    ]

startWebServer defaultConfig app