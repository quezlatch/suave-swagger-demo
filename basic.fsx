#load "./common.fsx"

open Newtonsoft.Json
open Suave.Json
open Common

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
            path "/add" >=> mapJson (operandAction (+))
            path "/multiply" >=> mapJson (operandAction (*))
        ]
    ]

startWebServer defaultConfig app