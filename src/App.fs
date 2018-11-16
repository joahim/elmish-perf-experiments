module App.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

open Elmish
open Elmish.React

open Types
open App.State

importAll "../sass/main.sass"

let renderTable model =

    let renderRow row =
        row
        |> Array.mapi (fun i col ->
            td [ ] [ str col ] )
        |> ofArray

    let sortRows direction rows =
        match direction with
        | Ascending ->
            Array.sort rows
        | Descending ->
            Array.sortDescending rows

    let renderRows direction rows =
        sortRows model.direction model.rows
        |> Array.map (fun (key, row) ->
            fragment [ Fable.Helpers.React.Props.Key (string key) ] [ lazyView (fun row -> tr [ ] [ renderRow row ]) row ] )
        |> ofArray

    table [ ClassName "table" ] [ renderRows model.direction model.rows ]

let view model dispatch =
  div
    []
    [ div
        [ ClassName "section" ]
        [ div
            [ ClassName "container" ]
            [ div
                [ ]
                [ button [ ClassName "button" ; OnClick (fun _ -> dispatch ChangeSortDirection) ] [ str "Sort" ]
                  renderTable model ] ] ] ]

open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update view
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReact "elmish-app"
|> Program.run
