module App.Global

type Page =
    | SortableTable
    | CollapsibleTree

let toHash page =
    match page with
    | SortableTable -> "#sortable-table"
    | CollapsibleTree -> "#collapsible-tree"
