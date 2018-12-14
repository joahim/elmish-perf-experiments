module AdjacencyMatrix.State

open Elmish
open AdjacencyMatrix.Types
open System

let init() =
    let data =
        createData
            [Data.row1 ; Data.row2 ; Data.row3]
            [Data.col1 ; Data.col2 ; Data.col3]
            Data.cells

    let model = {
        Data = data
        SortOrder = Default
        SortOrderHierarchy = false
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
    | SortOrderHierarchyChanged ->
        { model with SortOrderHierarchy = not model.SortOrderHierarchy }, Cmd.none

let sortDataBy (data : Data) (sortOrder : SortOrder) (sortOrderHierarchy : SortOrderHierarchy) =

    match sortOrder with
    | Default ->
        ( data.Rows |> List.collect Tree.flatten,
          data.Columns |> List.collect Tree.flatten)
    | DefaultReverse ->
        ( data.Rows |> List.collect Tree.flatten |> List.rev,
          data.Columns |> List.collect Tree.flatten |> List.rev)
    | EdgeCount ->

        let sort node = node.EdgeCount

        match sortOrderHierarchy with
        | false ->
            ( data.Rows |> List.collect Tree.flatten |> List.sortByDescending sort,
              data.Columns |> List.collect Tree.flatten |> List.sortByDescending sort)
        | true ->
            ( data.Rows
              |> List.map (Tree.sortByDescending sort)
              |> List.sortByDescending (Tree.getRootNode >> sort)
              |> List.collect Tree.flatten,
              data.Columns
              |> List.map (Tree.sortByDescending sort)
              |> List.sortByDescending (Tree.getRootNode >> sort)
              |> List.collect Tree.flatten)
