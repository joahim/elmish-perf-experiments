module App.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

open Elmish
open Elmish.React

open Types
open App.State

importAll "../sass/main.sass"

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
                  App.Views.renderTable model ] ] ] ]

open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update view
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReact "elmish-app"
|> Program.run
