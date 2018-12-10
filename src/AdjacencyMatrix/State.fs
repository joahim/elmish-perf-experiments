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
        SortOrder = Ascending
        Data = data
    }

    model, Cmd.none

let sortDataBy (data : Data) (sortOrder : SortOrder) =

    let sortNodesBy (nodes : Node list) (sortOrder : SortOrder) =
        match sortOrder with
        | Ascending ->
            nodes |> List.sortBy (fun node -> node.Key)
        | Descending ->
            nodes |> List.sortByDescending (fun node -> node.Key)

    { data with
        Rows = sortNodesBy data.Rows sortOrder
        Columns = sortNodesBy data.Columns sortOrder
    }

let update msg (model : Model) =
    match msg with
    | SortOrderChanged ->
        let newSortOrder =
            match model.SortOrder with
            | Ascending -> Descending
            | Descending -> Ascending
        let newModel =
             { model with
                Data = sortDataBy model.Data newSortOrder
                SortOrder = newSortOrder
            }
        newModel, Cmd.none
