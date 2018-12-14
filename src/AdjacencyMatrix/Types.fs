module AdjacencyMatrix.Types

open Tree

type SortOrder =
    | Default
    | DefaultReverse
    | EdgeCount

type SortOrderHierarchy = bool

type Node = {
    Id : string
    Name : string
}

type MatrixNode = {
    Id : string
    Name : string
    Level : int
    Visible  : bool
    EdgeCount : int
}

type Cell = {
    Row : string
    Column : string
    Value: float
}

type Data = {
    Rows : Tree<MatrixNode> list
    Columns : Tree<MatrixNode> list
    Cells : Cell list }

// Data is a matrix of cells with (hierarchical) rows and columns.
// Rows and columns represent graph nodes and cells represent graph edges (specified by node IDs).
// Rows and Columns hold respective node IDs and labels + hierarchy level and visibility (added by Create data).
// Data is usually a sparse matrix and therefore cells are represented as a list (to optimize animations).

let createData (rows : Tree<Node> list) (columns : Tree<Node> list) (cells : Cell list) =

    let rowEdgeCounts =
        cells
        |> List.map (fun cell -> cell.Row)
        |> List.countBy id
        |> Map.ofList

    let getRowEdgeCount id =
        if rowEdgeCounts.ContainsKey id then
            rowEdgeCounts.[id]
        else 0

    let columnEdgeCounts =
        cells
        |> List.map (fun cell -> cell.Column)
        |> List.countBy id
        |> Map.ofList

    let getColumnEdgeCount id =
        if columnEdgeCounts.ContainsKey id then
            columnEdgeCounts.[id]
        else 0

    let rec createTree getEdgeCount (level : int) (tree : Tree<Node>) : Tree<MatrixNode> =
        match tree with
        | Leaf node ->
            Leaf { Id = node.Id ; Name = node.Name ; Level = level ; Visible = true ; EdgeCount = getEdgeCount node.Id }
        | Branch (node, subTrees) ->
            let matrixNode = { Id = node.Id ; Name = node.Name ; Level = level ; Visible = true ; EdgeCount = getEdgeCount node.Id }
            let matrixSubTrees =
                subTrees
                |> List.fold (
                    fun acumulator tree ->
                        let matrixTree = createTree getEdgeCount (level + 1) tree
                        in List.append acumulator [matrixTree])
                    List.Empty
            Branch (matrixNode, matrixSubTrees)

    let createTreeList getEdgeCount (trees : List<Tree<Node>>) : List<Tree<MatrixNode>> =
        trees |> List.map (createTree getEdgeCount 0)

    { Rows = createTreeList getRowEdgeCount rows
      Columns = createTreeList getColumnEdgeCount columns
      Cells = cells }

type Model = {
    Data : Data
    SortOrder : SortOrder
    SortOrderHierarchy : SortOrderHierarchy
}

type Msg =
    | SortOrderChanged
    | SortOrderHierarchyChanged
