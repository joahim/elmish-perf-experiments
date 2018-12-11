module AdjacencyMatrix.Types
open Table.Types
open Table.Types

type SortOrder =
    | Ascending
    | Descending

type Node = {
    Name : string
    Position : int
}

type Cell = {
    Row : int
    Column : int
    Value: float
}

// Data is a matrix of cells.
// Data is usually a sparse matrix (not all cells have values) and therefore cells are represented as a list (to optimize animations).
// Rows and Columns hold respective labels but are also used to store the order of columns and rows.
// At creation, Rows and Columns are assigned Positions which are a simple enumerator (see createData).
// These Positions are referenced by Cells and used to pass the current Row/Column ordering to Cells.

type Data = {
    Rows : Node list
    Columns : Node list
    Cells : Cell list }

let createData rowLabels columnLabels cells =

    let nodesOfLabels labels =
        labels
        |> List.mapi (fun i value ->
            { Name = value
              Position = i })

    { Rows = nodesOfLabels rowLabels
      Columns = nodesOfLabels columnLabels
      Cells = cells }


type Model = {
    Data : Data
    SortOrder : SortOrder
}

type Msg =
    | SortOrderChanged
