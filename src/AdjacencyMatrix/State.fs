module AdjacencyMatrix.State

open Elmish
open AdjacencyMatrix.Types

let init() =
    let model = { Position = Left }
    model, Cmd.none

let update msg (model : Model) =
    match msg with
    | Animate ->
        match model.Position with
        | Left -> { model with Position = Right }, Cmd.none
        | Right -> { model with Position = Left }, Cmd.none
