module App.Global

type Page =
    | SortableTable
    | CollapsibleTree
    | AdjacencyMatrix

let toHash page =
    match page with
    | SortableTable -> "#sortable-table"
    | CollapsibleTree -> "#collapsible-tree"
    | AdjacencyMatrix -> "#collapsible-adjacency-matrix"
