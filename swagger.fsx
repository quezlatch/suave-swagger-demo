#load "./common.fsx"

open Suave
open Suave.Swagger
open Rest
open FunnyDsl
open Swagger
open Common

let add = JsonBody<Operands>(operandAction (+) >> MODEL)
let multiply = JsonBody<Operands>(operandAction (*) >> MODEL)

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