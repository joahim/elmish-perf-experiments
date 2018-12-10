module AdjacencyMatrix.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props

open AdjacencyMatrix.Types

let renderCell (cellSize : int) (cell : Cell) rowPosition columnPosition =
    rect [ Key (sprintf "%d-%d" cell.Row cell.Column)
           Class "adjacency-matrix__cell"
           SVGAttr.Width cellSize
           SVGAttr.Height cellSize
           SVGAttr.FillOpacity cell.Value
           Style [
               Transition "all 1s ease-in-out"
               TransitionDelay (sprintf "%dms" (50 * (cell.Row + cell.Column)))
               Transform (sprintf "translate(%dpx, %dpx)" (columnPosition * cellSize) (rowPosition * cellSize))
           ] ] [ ]

let renderMatrix cellSize (data : Data) =
    let matrixWidth = data.Columns.Length * cellSize
    let matrixHeight = data.Rows.Length * cellSize

    let positionMap nodes =
        nodes
        |> List.mapi (fun i el -> (el.Key, i))
        |> Map.ofList

    let rowPositions = positionMap data.Rows
    let columnPositions = positionMap data.Columns

    let cells =
        data.Cells
        |> List.map (fun cell ->
            renderCell 50 cell rowPositions.[cell.Row] columnPositions.[cell.Column])

    svg [ Class "adjacency-matrix"
          SVGAttr.Width matrixWidth
          SVGAttr.Height matrixHeight
        ] cells

let renderSortOrder sortOrder dispatch =
    let order =
        match sortOrder with
        | Ascending ->
            span [ OnClick (fun e -> dispatch SortOrderChanged) ] [ str "ascending" ]
        | Descending ->
            span [ OnClick (fun e -> dispatch SortOrderChanged) ] [ str "descending" ]
    div [ Class "sort-order" ] [
        str "Order by: "
        order
    ]

let view (model : Model) dispatch =
    div [] [
        renderSortOrder model.SortOrder dispatch
        renderMatrix 50 model.Data
    ]

// let square position width height color =
//     rect [ SVGAttr.X position.X
//            SVGAttr.Y position.Y
//            SVGAttr.Width width
//            SVGAttr.Height height
//            SVGAttr.Fill color
//            SVGAttr.FillOpacity 0.5
//            Style [
//                Transition "all 1s ease-in-out"
//                Transform "translate(0px, 0px)"
//            ] ] [ ]

// let bars =
//     match model.Position with
//     | Left ->
//         [ square { X = 0 ; Y = 0 } 5 100 "cyan"
//           square { X = 5 ; Y = 0 } 5 100 "magenta"
//           square { X = 10 ; Y = 0 } 5 100 "yellow" ]
//     | Right ->
//         [ square { X = 95 ; Y = 0 } 5 100 "cyan"
//           square { X = 90 ; Y = 0 } 5 100 "magenta"
//           square { X = 85 ; Y = 0 } 5 100 "yellow" ]
// div [] [ svg [
//     OnClick (fun e -> dispatch Animate)
//     SVGAttr.ViewBox "0 0 100 100"
//     // SVGAttr.Width "100%"
//     // SVGAttr.Height "100vh"
//     ] bars ]
