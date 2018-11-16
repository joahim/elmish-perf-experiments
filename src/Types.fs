module App.Types

type Direction =
  | Ascending
  | Descending

type Msg =
  | ChangeSortDirection

type Col = string
type Key = string

type Row = Key * Col array

type Model = {
  direction : Direction
  rows : Row array
}
