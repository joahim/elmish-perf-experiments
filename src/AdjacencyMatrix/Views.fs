module AdjacencyMatrix.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props

open AdjacencyMatrix.Types

// let pixelsOfInt value =
//     string value + "px"

let renderCell (cellSize : int) rowIndex columnindex rowPosition columnPosition (cell : Cell) =
    let fillOpacity =
        match cell with
        | None -> 0.0
        | Some value -> value

    rect [ Key (sprintf "%d-%d" columnindex rowIndex)
           SVGAttr.Width cellSize
           SVGAttr.Height cellSize
           SVGAttr.Fill "hotpink"
           SVGAttr.FillOpacity fillOpacity
           Style [
               Transition "all 1s ease-in-out"
               Transform (sprintf "translate(%dpx, %dpx)" (columnPosition * cellSize) (rowPosition * cellSize))
           ] ] [ ]

let renderMatrix cellSize (data : Data) =
    let matrixWidth = data.Columns.Length * cellSize
    let matrixHeight = data.Rows.Length * cellSize

    let cells =
        data.Matrix
        |> Array.mapi (fun rowIndex row ->
            row
            |> Array.mapi (fun columnIndex cell ->
                renderCell 50 rowIndex columnIndex rowIndex columnIndex cell
            )
        )
        |> Array.collect id

    svg [ SVGAttr.Width matrixWidth
          SVGAttr.Height matrixHeight
        ] cells

let view (model : Model) dispatch =
    div [] [ renderMatrix 50 model.Data ]

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
