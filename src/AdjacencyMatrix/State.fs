module AdjacencyMatrix.State

open Elmish
open AdjacencyMatrix.Types

let init() =
    let model = ()
    model, Cmd.none

let update msg model = model, Cmd.none
// match msg with
// | ChangeSortOrder ->
//     let model =
//         match model.SortOrder with
//         | Ascending -> { model with SortOrder = Descending }
//         | Descending -> { model with SortOrder = Ascending }
//     model, Cmd.none
