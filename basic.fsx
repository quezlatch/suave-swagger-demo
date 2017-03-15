#load "./paket-files/include-scripts/net46/include.main.group.fsx"

type Operands = {
    left: int
    right: int
}

type Result = {
    result: int
}

open Newtonsoft
open Newtonsoft.Json
open Newtonsoft.Json.Linq
let fromJson1<'T> bytes =
    UTF8.toString bytes |> JsonConvert.DeserializeObject<'T>

let toJson1 o =
    JsonConvert.SerializeObject o |> UTF8.bytes

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