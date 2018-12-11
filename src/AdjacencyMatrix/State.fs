module AdjacencyMatrix.State

open Elmish
open AdjacencyMatrix.Types

let init() =
    let data =
        let rows = [ 1..15 ] |> List.map string
        let columns = [ 'A' .. 'O' ] |> List.map string
        let cells =
            seq {
                for rowIndex in [ 0 .. rows.Length - 1 ] do
                    for columnIndex in [ 0 .. columns.Length - 1 ] do
                    match System.Random().NextDouble() with
                    | value when value > 0.1 ->
                        yield {
                            Row = rowIndex
                            Column = columnIndex
                            Value = value
                        }
                    | _ -> ()
            } |> List.ofSeq

        createData rows columns cells

    let model = {
        SortOrder = Ascending
        Data = data
    }

    model, Cmd.none

let sortDataBy (data : Data) (sortOrder : SortOrder) =

    let sortNodesBy (nodes : Node list) (sortOrder : SortOrder) =
        match sortOrder with
        | Ascending ->
            nodes |> List.sortBy (fun node -> node.Position)
        | Descending ->
            nodes |> List.sortByDescending (fun node -> node.Position)

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
