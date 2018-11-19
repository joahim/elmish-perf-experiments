module Table.State

open Elmish
open Table.Types

let init() =
    let rows =
        [ for i in 0..100 do
              yield i, 
                    [ for j in 0..9 do
                          yield (j + i * 10 |> string) ]
                    |> Array.ofSeq ]
        |> Array.ofSeq
    
    let model =
        { SortOrder = Ascending
          Rows = rows }
    
    model, Cmd.none

let update msg model =
    match msg with
    | ChangeSortOrder -> 
        let model =
            match model.SortOrder with
            | Ascending -> { model with SortOrder = Descending }
            | Descending -> { model with SortOrder = Ascending }
        model, Cmd.none
