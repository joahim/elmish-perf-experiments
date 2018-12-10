module AdjacencyMatrix.State

open Elmish
open AdjacencyMatrix.Types

let init() =
    let data =
        let rows = [ 1..15 ] |> List.map string
        let columns = [ 'A' .. 'R' ] |> List.map string
        let matrix =
            rows
            |> List.map (fun _ ->
                columns
                |> List.map (fun _ ->
                    match System.Random().NextDouble() with
                    | value when value < 0.1 -> None
                    | value -> Some value)
                |> Array.ofList)
            |> Array.ofList

        { Rows = nodesOfValues rows
          Columns = nodesOfValues columns
          Matrix = matrix
        }

    let model = {
        Position = Left
        Data = data
    }
    model, Cmd.none

let update msg (model : Model) =
    match msg with
    | Animate ->
        match model.Position with
        | Left -> { model with Position = Right }, Cmd.none
        | Right -> { model with Position = Left }, Cmd.none
