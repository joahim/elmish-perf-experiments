module AdjacencyMatrix.Types

type Position =
    | Left
    | Right

type Model = {
    Position : Position
}

type Msg =
    Animate
