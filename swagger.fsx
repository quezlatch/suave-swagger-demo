#load "./paket-files/include-scripts/net46/include.main.group.fsx"

type Operands = {
    left: int
    right: int
}

type Result = {
    result: int
}

open Suave
open Suave.Swagger
open Rest
open FunnyDsl
open Swagger

let add = JsonBody<Operands>(fun o -> MODEL {result = o.left + o.right })
let multiply = JsonBody<Operands>(fun o -> MODEL {result = o.left * o.right })

let api = 
  swagger {
    for route in posting (simpleUrl "/add" |> thenReturns add) do
      yield description Of route is "Add two numbers together"
      yield route |> addResponse 200 "returns the result" (Some typeof<Result>)
      yield route |> supportsJsonAndXml
      yield parameter "operands" Of route (fun p -> { p with Type = (Some typeof<Operands>); In=Body})

    for route in posting (simpleUrl "/multiply" |> thenReturns multiply) do
      yield description Of route is "Multiply two numbers together"
      yield route |> addResponse 200 "returns the result" (Some typeof<Result>)
      yield route |> supportsJsonAndXml
      yield parameter "operands" Of route (fun p -> { p with Type = (Some typeof<Operands>); In=Body})
  }

startWebServer defaultConfig api.App