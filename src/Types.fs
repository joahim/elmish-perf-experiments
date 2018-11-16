module App.Types

open App.Global

type Msg = TableMsg of Table.Types.Msg

type Model =
    { CurrentPage : Page
      Table : Table.Types.Model }
