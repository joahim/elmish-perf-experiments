module App.Global

type Page = SortableTable

let toHash page =
    match page with
    | SortableTable -> "#table"
