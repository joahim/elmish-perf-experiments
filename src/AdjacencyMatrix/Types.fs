module AdjacencyMatrix.Types
open Table.Types
open Table.Types

type SortOrder =
    | Ascending
    | Descending

type Node = {
    Key : int
    Name : string
}

type Cell = {
    Row : int
    Column : int
    Value: float
}

type Data =
    { Rows : Node list
      Columns : Node list
      Cells : Cell list }

let nodesOfValues values =
    values
    |> List.mapi (fun i value ->
        { Key = i
          Name = value } )

type Model = {
    Data : Data
    SortOrder : SortOrder
}

type Msg =
    | SortOrderChanged
