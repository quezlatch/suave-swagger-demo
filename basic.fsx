#load "./paket-files/include-scripts/net46/include.main.group.fsx"

type Operands = {
    left: int
    right: int
}

type Result = {
    result: int
}

open Newtonsoft.Json
open Suave.Json

let mapJson = 
    mapJsonWith 
        (UTF8.toString >> JsonConvert.DeserializeObject<Operands>) 
        (JsonConvert.SerializeObject >> UTF8.bytes)

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