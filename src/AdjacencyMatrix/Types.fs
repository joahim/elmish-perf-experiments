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

type Cell = float option

type Data =
    { Rows : Node list
      Columns : Node list
      Matrix : Cell array array }

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
