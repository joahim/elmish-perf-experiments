module AdjacencyMatrix.Types

type Position =
    | Left
    | Right

type Node = {
    Key : int
    Name : string
    Position : int
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
          Name = value
          Position = i } )

type Model = {
    Position : Position
    Data : Data
}

type Msg =
    Animate
