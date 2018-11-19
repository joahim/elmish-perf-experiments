module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open App.Global
open App.Types

let pageParser : Parser<Page -> Page, Page> =
    oneOf [ map SortableTable (s "sortable-table")
            map CollapsibleTree (s "collapsible-tree") ]

let urlUpdate (result : Option<Page>) model =
    match result with
    | None -> 
        console.error ("Error parsing URL")
        model, Navigation.modifyUrl (toHash model.CurrentPage)
    | Some page -> { model with CurrentPage = page }, Cmd.none

let init result =
    let (tableModel, tableCmd) = Table.State.init()
    let (collapsibleTreeModel, collapsibleTreeCmd) =
        CollapsibleTree.State.init()
    
    let (model, cmd) =
        urlUpdate result { CurrentPage = SortableTable
                           Table = tableModel
                           CollapsibleTree = collapsibleTreeModel }
    model, 
    Cmd.batch [ cmd
                Cmd.map TableMsg tableCmd
                Cmd.map CollapsibleTreeMsg collapsibleTreeCmd ]

let update msg model =
    match msg with
    | TableMsg msg -> 
        let (table, tableCmd) = Table.State.update msg model.Table
        { model with Table = table }, Cmd.map TableMsg tableCmd
    | CollapsibleTreeMsg msg -> 
        let (collapsibleTree, collapsibleTreeCmd) =
            CollapsibleTree.State.update msg model.CollapsibleTree
        { model with CollapsibleTree = collapsibleTree }, 
        Cmd.map CollapsibleTreeMsg collapsibleTreeCmd
