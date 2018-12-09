module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open App.Global
open App.Types

let pageParser : Parser<Page -> Page, Page> =
    oneOf [ map SortableTable (s "sortable-table")
            map CollapsibleTree (s "collapsible-tree")
            map AdjacencyMatrix (s "collapsible-adjacency-matrix") ]

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
    let (adjacencyMatrixModel, adjacencyMatrixCmd) =
        AdjacencyMatrix.State.init()
    let (model, cmd) =
        urlUpdate result { CurrentPage = SortableTable
                           Table = tableModel
                           CollapsibleTree = collapsibleTreeModel
                           AdjacencyMatrix = adjacencyMatrixModel }
    model,
    Cmd.batch [ cmd
                Cmd.map TableMsg tableCmd
                Cmd.map CollapsibleTreeMsg collapsibleTreeCmd
                Cmd.map AdjacencyMatrixMsg adjacencyMatrixCmd ]

let update msg model =
    match msg with
    | TableMsg msg ->
        let (model', cmd) = Table.State.update msg model.Table
        { model with Table = model' }, Cmd.map TableMsg cmd
    | CollapsibleTreeMsg msg ->
        let (model', cmd) =
            CollapsibleTree.State.update msg model.CollapsibleTree
        { model with CollapsibleTree = model' }, Cmd.map CollapsibleTreeMsg cmd
    | AdjacencyMatrixMsg msg ->
        let (model', cmd) =
            AdjacencyMatrix.State.update msg model.AdjacencyMatrix
        { model with AdjacencyMatrix = model' }, Cmd.map AdjacencyMatrixMsg cmd
