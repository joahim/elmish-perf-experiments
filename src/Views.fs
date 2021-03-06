module App.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props
open App.Global
open App.Types

let renderNavbar =
    nav [ ClassName "nav" ]
        [ div [ ClassName "nav-left" ]
              [ h1 [ ClassName "nav-item is-brand title is-4" ]
                    [ str "Elmish playground" ] ] ]

let renderMenuItem (label : string) (page : Page) (currentPage : Page) =
    li [] [ a [ classList [ "is-active", page = currentPage ]
                Href(toHash page) ] [ str label ] ]

let renderMenu currentPage =
    aside [ ClassName "menu" ]
        [ ul [ ClassName "menu-list" ]
              [ renderMenuItem "Sortable table" SortableTable currentPage
                renderMenuItem "Collapsible tree" CollapsibleTree currentPage
                renderMenuItem "Adjacency matrix" AdjacencyMatrix currentPage ] ]

let view model dispatch =
    let pageHtml page =
        match page with
        | SortableTable -> Table.Views.view model.Table (TableMsg >> dispatch)
        | CollapsibleTree ->
            CollapsibleTree.Views.view model.CollapsibleTree
                (CollapsibleTreeMsg >> dispatch)
        | AdjacencyMatrix ->
            AdjacencyMatrix.Views.view model.AdjacencyMatrix
                (AdjacencyMatrixMsg >> dispatch)
    div []
        [ div [ ClassName "navbar-bg" ]
              [ div [ ClassName "container" ] [ renderNavbar ] ]

          div [ ClassName "section" ]
              [ div [ ClassName "container" ]
                    [ div [ ClassName "columns" ]
                          [ div [ ClassName "column is-3" ]
                                [ renderMenu model.CurrentPage ]

                            div [ ClassName "column" ]
                                [ pageHtml model.CurrentPage ] ] ] ] ]
