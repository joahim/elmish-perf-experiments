module AdjacencyMatrix.Types

open Tree

type SortOrder =
    | Default
    | DefaultReverse

type Node = {
    Id : string
    Name : string
}

type MatrixNode = {
    Id : string
    Name : string
    Level : int
    Visible  : bool
}

type Cell = {
    Row : string
    Column : string
    Value: float
}

// Data is a matrix of cells.
// Data is usually a sparse matrix (not all cells have values) and therefore cells are represented as a list (to optimize animations).
// Rows and Columns hold respective labels but are also used to store the order of columns and rows.
// At creation, Rows and Columns are assigned Positions which are a simple enumerator (see createData).
// These Positions are referenced by Cells and used to pass the current Row/Column ordering to Cells.

type Data = {
    Rows : Tree<MatrixNode> list
    Columns : Tree<MatrixNode> list
    Cells : Cell list }

let createData (rows : Tree<Node> list) (columns : Tree<Node> list) (cells : Cell list) =

    let rec createTree (level : int) (tree : Tree<Node>) : Tree<MatrixNode> =
        match tree with
        | Leaf node ->
            Leaf { Id = node.Id ; Name = node.Name ; Level = level ; Visible = true}
        | Branch (node, subTrees) ->
            let matrixNode = { Id = node.Id ; Name = node.Name ; Level = level ; Visible = true}
            let matrixSubTrees =
                subTrees
                |> List.fold (
                    fun acumulator tree ->
                        let matrixTree = createTree (level + 1) tree
                        in List.append acumulator [matrixTree])
                    List.Empty
            Branch (matrixNode, matrixSubTrees)

    let createTreeList (trees : List<Tree<Node>>) : List<Tree<MatrixNode>> =
        trees |> List.map (createTree 0)

    { Rows = createTreeList rows
      Columns = createTreeList columns
      Cells = cells }

type Model = {
    Data : Data
    SortOrder : SortOrder
}

type Msg =
    | SortOrderChanged
