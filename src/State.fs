module App.State

open Elmish
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open Types

let init () =
    let rows =
        [ for i in 0 .. 100 do
            yield (string i), [ for j in 1 .. 10 do yield (j + i * 10 |> string) ] |> Array.ofSeq
        ] |> Array.ofSeq

    { rows = rows ; direction = Ascending }, Cmd.none

let update msg model =
  match msg with
  | ChangeSortDirection ->
        let model =
            match model.direction with
            | Ascending ->
                { model with direction = Descending }
            | Descending ->
                { model with direction = Ascending }
        model, Cmd.none
