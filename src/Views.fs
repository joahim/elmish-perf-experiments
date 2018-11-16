module App.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props

open Elmish.React

open App.Types

let renderTable model =

    let renderRow row =
        row
        |> Array.mapi (fun i col ->
            fragment
                [ Fable.Helpers.React.Props.Key (string i) ]
                [ lazyView (fun col -> td [ ] [ str col ]) col ] )
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
            fragment
                [ Fable.Helpers.React.Props.Key (string key) ]
                [ lazyView (fun row -> tr [ ] [ renderRow row ]) row ] )
        |> ofArray

    table
        [ ClassName "table" ]
        [ tbody
            [ ]
            [ renderRows model.direction model.rows ] ]
