module AdjacencyMatrix.State

open Elmish
open AdjacencyMatrix.Types

let init() =
    let data =
        createData
            [Data.row1 ; Data.row2 ; Data.row3]
            [Data.col1 ; Data.col2 ; Data.col3]
            Data.cells

    let model = {
        SortOrder = Default
        Data = data
    }

    model, Cmd.none

let update msg (model : Model) =
    match msg with
    | SortOrderChanged ->
        let newSortOrder =
            match model.SortOrder with
            | Default -> DefaultReverse
            | DefaultReverse -> EdgeCount
            | EdgeCount -> Default
        let newModel =
             { model with
                SortOrder = newSortOrder
            }
        newModel, Cmd.none

let sortDataBy (data : Data) (sortOrder : SortOrder) =

    match sortOrder with
    | Default ->
        ( data.Rows |> List.collect Tree.flatten,
          data.Columns |> List.collect Tree.flatten)
    | DefaultReverse ->
        ( data.Rows |> List.collect Tree.flatten |> List.rev,
          data.Columns |> List.collect Tree.flatten |> List.rev)
    | EdgeCount ->
        ( data.Rows |> List.collect Tree.flatten |> List.sortByDescending (fun n -> n.EdgeCount),
          data.Columns |> List.collect Tree.flatten |> List.sortByDescending (fun n -> n.EdgeCount))
