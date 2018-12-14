module AdjacencyMatrix.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props

open AdjacencyMatrix.Types

let renderRect xPosition yPosition width height (opacity : float) (delay : int) children =
    rect [
        SVGAttr.Width width
        SVGAttr.Height height
        SVGAttr.FillOpacity opacity
        SVGAttr.Transform (sprintf "translate(%d, %d)" xPosition yPosition)
        Style [
            Transition "all 1s ease-in-out"
            TransitionDelay (sprintf "%dms" (50 * delay)) ] ]
        children

let renderMatrix cellSize (data : Data) (sortOrder : SortOrder)=
    let labelPadding = 8
    let cellPadding = 1
    let levelSizeDecrement = 15

    let rowLabelWidth = 140
    let rowLabelMargin = 1

    let columnLabelHeight = 140
    let columnLabelMargin = 1

    let rowNodes = data.Rows |> List.collect Tree.flatten
    let columnNodes = data.Columns |> List.collect Tree.flatten

    let matrixWidth = columnNodes.Length * (cellSize + cellPadding) - cellPadding
    let matrixHeight = rowNodes.Length * (cellSize + cellPadding) - cellPadding

    let orderedRowNodes, orderedColumnNodes = State.sortDataBy data sortOrder

    let nodePositionsMap nodes =
        nodes
        |> List.mapi (fun i el -> (el.Id, i))
        |> Map.ofList

    let rowPositionsMap = nodePositionsMap orderedRowNodes
    let columnPositionsMap = nodePositionsMap orderedColumnNodes

    let rowLabels =
        rowNodes
        |> List.map (fun row ->
            let sizeDecrement = levelSizeDecrement * row.Level
            g [
                Class (sprintf "adjacency-matrix__label level-%d" row.Level)
                SVGAttr.Transform (sprintf "translate(%d, %d)" 0 (rowPositionsMap.[row.Id] * (cellSize + cellPadding)))
                Style [
                    Transition "all 1s ease-in-out"
                    TransitionDelay (sprintf "%dms" (50 * rowPositionsMap.[row.Id])) ] ]
              [
                renderRect sizeDecrement 0 (rowLabelWidth - sizeDecrement) cellSize 1.0 0 []
                text [
                    SVGAttr.X (rowLabelWidth - labelPadding)
                    SVGAttr.Y ((float cellSize) * 0.5)
                    ] [ str row.Name ]
              ])

    let columnLabels =
        columnNodes
        |> List.mapi (fun i column ->
            let sizeDecrement = levelSizeDecrement * column.Level
            g [
                Class (sprintf "adjacency-matrix__label level-%d" column.Level)
                SVGAttr.Transform (sprintf "translate(%d, %d)" (columnPositionsMap.[column.Id] * (cellSize + cellPadding)) 0)
                Style [
                    Transition "all 1s ease-in-out"
                    TransitionDelay (sprintf "%dms" (50 * columnPositionsMap.[column.Id])) ] ]
              [
                renderRect 0 sizeDecrement cellSize (columnLabelHeight - sizeDecrement) 1.0 0 []
                g [ SVGAttr.Transform (sprintf "translate(%f %d)" (float cellSize * 0.5) (columnLabelHeight - labelPadding)) ] [
                    text [ SVGAttr.Transform (sprintf "rotate(-90)") ] [ str column.Name ]
                ]
              ])

    let cells =
        data.Cells
        |> List.map (fun cell ->
            g [ Class "adjacency-matrix__cell" ] [
                renderRect
                    (columnPositionsMap.[cell.Column] * (cellSize + cellPadding))
                    (rowPositionsMap.[cell.Row] * (cellSize + cellPadding))
                    cellSize cellSize cell.Value
                    ((float (columnPositionsMap.[cell.Column] + rowPositionsMap.[cell.Row])) / 2.0 |> int)
                    []])

    svg [ Class "adjacency-matrix"
          SVGAttr.Width (matrixWidth + rowLabelWidth + rowLabelMargin)
          SVGAttr.Height (matrixHeight + columnLabelHeight + columnLabelMargin)
        ] [ g [ Class "adjacency-matrix__row-labels"
                SVGAttr.Transform (sprintf "translate(%d, %d)" 0 (columnLabelHeight + columnLabelMargin)) ] rowLabels
            g [ Class "adjacency-matrix__column-labels"
                SVGAttr.Transform (sprintf "translate(%d, %d)" (rowLabelWidth + rowLabelMargin) 0) ] columnLabels
            g [ Class "adjacency-matrix__cells"
                SVGAttr.Transform (sprintf "translate(%d, %d)" (rowLabelWidth + rowLabelMargin) (columnLabelHeight + columnLabelMargin)) ] cells
        ]

let renderSortOrder sortOrder dispatch =
    let order =
        match sortOrder with
        | Default ->
            span [ OnClick (fun e -> dispatch SortOrderChanged) ] [ str "default" ]
        | DefaultReverse ->
            span [ OnClick (fun e -> dispatch SortOrderChanged) ] [ str "default reverse" ]
        | EdgeCount ->
            span [ OnClick (fun e -> dispatch SortOrderChanged) ] [ str "default edge count" ]
    div [ Class "sort-order" ] [
        str "Order by: "
        order
    ]

let view (model : Model) dispatch =
    div [] [
        renderSortOrder model.SortOrder dispatch
        renderMatrix 30 model.Data model.SortOrder
    ]
