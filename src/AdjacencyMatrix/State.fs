module AdjacencyMatrix.State

open Elmish
open AdjacencyMatrix.Types

let init() =
    let data =
        let cells =
            seq {
                for rowIndex in [ 0 .. 14 - 1 ] do
                    for columnIndex in [ 0 .. 14 - 1 ] do
                    match System.Random().NextDouble() with
                    | value when value > 0.6 ->
                        yield {
                            Row = rowIndex
                            Column = columnIndex
                            Value = System.Random().NextDouble()
                        }
                    | _ -> ()
            } |> List.ofSeq

        createData
            [Data.row1 ; Data.row2]
            [Data.col1 ; Data.col2]
            cells

    let model = {
        SortOrder = Ascending
        Data = data
    }

    model, Cmd.none

let sortDataBy (data : Data) (sortOrder : SortOrder) =

    let sortNodesBy tree (sortOrder : SortOrder) =
        match sortOrder with
        | Ascending ->
            tree
            // nodes |> List.sortBy (fun node -> node.Position)
        | Descending ->
            tree
            // nodes |> List.sortByDescending (fun node -> node.Position)
        | Shuffle ->
            tree
            // Shuffle.shuffleList nodes

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
            | Descending -> Shuffle
            | Shuffle -> Ascending
        let newModel =
             { model with
                Data = sortDataBy model.Data newSortOrder
                SortOrder = newSortOrder
            }
        newModel, Cmd.none
