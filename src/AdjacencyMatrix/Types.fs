module AdjacencyMatrix.Types

open Tree

type SortOrder =
    | Ascending
    | Descending
    | Shuffle

type Node = {
    Name : string
}

type EnumeratedNode = {
    Name : string
    Level : int
    Position : int
    Visible  : bool
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
    Rows : Tree<EnumeratedNode> list
    Columns : Tree<EnumeratedNode> list
    Cells : Cell list }

let createData (rows : Tree<Node> list) (columns : Tree<Node> list) cells =

    // Enumerate tree (depth first)

    let rec enumerateTree (counter : int) (tree : Tree<Node>) : (Tree<EnumeratedNode> * int) =
        match tree with
        | Leaf node ->
            (Leaf { Name = node.Name ; Level = 1 ; Position = counter ; Visible = true}), counter + 1
        | Branch (node, subTreeList) ->
            let enumeratedNode = { Name = node.Name ; Level = 1 ; Position = counter ; Visible = true}
            let enumeratedSubTreeList, newCounter =
                subTreeList
                |> List.fold (
                    fun (acumulator, counter : int) tree ->
                        let enumeratedTree, newCounter = enumerateTree counter tree
                        in (List.append acumulator [enumeratedTree]), newCounter)
                    (List.Empty, counter + 1)
            Branch (enumeratedNode, enumeratedSubTreeList), newCounter

    let enumerateTreeList (trees : List<Tree<Node>>) : List<Tree<EnumeratedNode>> =
        let enumeratedTrees, counter = List.mapFold enumerateTree 0 trees
        enumeratedTrees

    { Rows = enumerateTreeList rows
      Columns = enumerateTreeList columns
      Cells = cells }

type Model = {
    Data : Data
    SortOrder : SortOrder
}

type Msg =
    | SortOrderChanged
