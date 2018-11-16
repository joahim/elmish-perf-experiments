module App

open Fable.Core.JsInterop

open Elmish
open Elmish.React
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser

open App.State
open App.Views

importAll "../sass/main.sass"

open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update view
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReact "elmish-app"
|> Program.run
