module Tree
open System

    type Tree<'Node> =
        | Leaf of 'Node
        | Branch of 'Node * Tree<'Node> list

    let rec fold fLeaf fNode acc (tree : Tree<'Node>) : 'Acc =
        let recurse = fold fLeaf fNode
        match tree with
        | Leaf leafInfo ->
            fLeaf acc leafInfo
        | Branch (nodeInfo, subtrees) ->
            // determine the local accumulator at this level
            let localAcc = fNode acc nodeInfo
            // thread the local accumulator through all the subitems using Seq.fold
            let finalAcc = subtrees |> Seq.fold recurse localAcc
            // ... and return it
            finalAcc

    let rec map fLeaf fNode (tree : Tree<'Node>) =
        let recurse = map fLeaf fNode
        match tree with
        | Leaf node ->
            Leaf (fLeaf node)
        | Branch (node, subtrees) ->
            Branch (fNode node, subtrees |> List.map recurse)

    let flatten tree =
        fold
            (fun acc node -> node :: acc)
            (fun acc node -> node :: acc)
            [] tree
        |> List.rev

    let getRootNode (tree : Tree<'Node>) : 'Node =
        match tree with
        | Leaf node -> node
        | Branch (node, _) -> node

    let rec sortByDescending (f : 'Node -> 'Key) (tree : Tree<'Node>) : Tree<'Node> =
        match tree with
        | Leaf node ->
            Leaf node
        | Branch (node, subtrees) ->
            let sortdedSubtrees =
                subtrees
                |> List.sortByDescending (getRootNode >> f)
            Branch (node, sortdedSubtrees |> List.map (sortByDescending f))

    // let rec enumerate (level : int) (counter : int) (tree : Tree<'Node>) : (Tree<EnumeratedNode> * int) =
    //     match tree with
    //     | Leaf node ->
    //         (Leaf { Name = node.Name ; Level = level ; Position = counter ; Visible = true}), counter + 1
    //     | Branch (node, subTreeList) ->
    //         let enumeratedNode = { Name = node.Name ; Level = level ; Position = counter ; Visible = true}
    //         let enumeratedSubTreeList, newCounter =
    //             subTreeList
    //             |> List.fold (
    //                 fun (acumulator, counter : int) tree ->
    //                     let enumeratedTree, newCounter = enumerateTree (level + 1) counter tree
    //                     in (List.append acumulator [enumeratedTree]), newCounter)
    //                 (List.Empty, counter + 1)
    //         Branch (enumeratedNode, enumeratedSubTreeList), newCounter
