module CollapsibleTree.State

open Elmish
open Components.CollapsibleTree
open CollapsibleTree.Types
open CollapsibleTree.Data

let rec addPathToTree (tree : Tree) (path : string list) : Tree =
    match path with
    | [] -> tree
    | [ x ] -> 
        match tree with
        | Leaf l -> Node(l, [| Leaf x |])
        | Node(nodeName, children) -> 
            if children
               |> Array.exists (fun el -> 
                      match el with
                      | Leaf n -> n = x
                      | Node(n, _) -> n = x)
            then tree
            else Node(nodeName, Array.append children [| Leaf x |])
    | x :: xs -> 
        match tree with
        | Leaf name -> Node(name, [| addPathToTree (Node(x, [||])) xs |])
        | Node(name, children) -> 
            let updatedChildren =
                if children
                   |> Array.exists (fun el -> 
                          match el with
                          | Leaf n -> n = x
                          | Node(n, _) -> n = x)
                then 
                    children
                    |> Array.map (fun el -> 
                           match el with
                           | Leaf n when n = x -> 
                               Node(n, [| addPathToTree (Node(x, [||])) xs |])
                           | Node(n, cs) as node when n = x -> 
                               addPathToTree node xs
                           | el -> el)
                else 
                    Array.append children [| addPathToTree (Node(x, [||])) xs |]
            Node(name, updatedChildren)

let generateTree data =
    data
    |> List.fold (fun (state : Tree) (path : string list) -> 
           path
           |> List.rev
           |> addPathToTree state) (Leaf "")

let init() =
    let model = { Tree = generateTree paths }
    model, Cmd.none

let update msg model = model, Cmd.none
