module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser

open App.Global
open App.Types

let pageParser: Parser<Page -> Page, Page> =
    oneOf [
        map SortableTable (s "sortable-table")
    ]

let urlUpdate (result : Option<Page>) model =
    match result with
    | None ->
        console.error("Error parsing URL")
        model, Navigation.modifyUrl (toHash model.CurrentPage)
    | Some page ->
        { model with CurrentPage = page }, Cmd.none

let init result =
    let (tableModel, tableCmd) = Table.State.init()
    let (model, cmd) =
        urlUpdate result
            { CurrentPage = SortableTable
              Table = tableModel }

    model, Cmd.batch [ cmd ; Cmd.map TableMsg tableCmd ]

let update msg model =
  match msg with
  | TableMsg msg ->
      let (table, tableCmd) = Table.State.update msg model.Table
      { model with Table = table }, Cmd.map TableMsg tableCmd
