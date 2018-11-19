module CollapsibleTree.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Elmish.React
open Components.CollapsibleTree
open CollapsibleTree.Types

let view (model : Model) dispatch =
    let options =
        { width = 800
          height = 600 }
    div [ Class "collapsible-tree" ] [ CollapsibleTree [ Data model.Tree
                                                         Width 800
                                                         Height 600
                                                         Options options ] ]
