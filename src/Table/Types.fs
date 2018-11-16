module Table.Types

type SortOrder =
    | Ascending
    | Descending

type Msg =
    | ChangeSortOrder

type Column = string
type Key = int

type Row = Key * Column array

type Model = {
    SortOrder : SortOrder
    Rows : Row array
}
