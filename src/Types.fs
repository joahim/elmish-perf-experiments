module App.Types

open App.Global
open Components

type Msg =
    | TableMsg of Table.Types.Msg
    | CollapsibleTreeMsg of CollapsibleTree.Types.Msg

type Model =
    { CurrentPage : Page
      Table : Table.Types.Model
      CollapsibleTree : CollapsibleTree.Types.Model }
