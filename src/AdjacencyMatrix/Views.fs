module AdjacencyMatrix.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props

open AdjacencyMatrix.Types

let renderRect className xPosition yPosition width height opacity delay children =
    rect [
        Class className
        SVGAttr.Width width
        SVGAttr.Height height
        SVGAttr.FillOpacity opacity
        SVGAttr.Transform (sprintf "translate(%d, %d)" xPosition yPosition)
        Style [
            Transition "all 1s ease-in-out"
            TransitionDelay (sprintf "%dms" (50 * delay)) ] ]
        children

let renderMatrix cellSize (data : Data) =
    let labelPadding = 10
    let cellPadding = 2

    let rowLabelWidth = 80
    let rowLabelMargin = 10

    let columnLabelHeight = 80
    let columnLabelMargin = 10

    let rowNodes = data.Rows |> List.collect Tree.flatten
    let columnNodes = data.Columns |> List.collect Tree.flatten

    let matrixWidth = rowNodes.Length * (cellSize + cellPadding) - cellPadding
    let matrixHeight = rowNodes.Length * (cellSize + cellPadding) - cellPadding

    let positionMap nodes =
        nodes
        |> List.mapi (fun i el -> (el.Position, i))
        |> Map.ofList

    let rowPositionsMap = positionMap rowNodes
    let columnPositionsMap = positionMap columnNodes

    let rowLabels =
        rowNodes
        |> List.sortBy (fun row -> row.Position)
        |> List.mapi (fun i row ->
            g [
                SVGAttr.Transform (sprintf "translate(%d, %d)" 0 (rowPositionsMap.[i] * (cellSize + cellPadding)))
                Style [
                    Transition "all 1s ease-in-out"
                    TransitionDelay (sprintf "%dms" (50 * rowPositionsMap.[i])) ] ]
              [
                renderRect "" 0 0 rowLabelWidth cellSize 0.5 0 []
                text [
                    SVGAttr.X (rowLabelWidth - labelPadding)
                    SVGAttr.Y ((float cellSize) * 0.5)
                    ] [ str row.Name ]
              ])

    let columnLabels =
        columnNodes
        |> List.sortBy (fun column -> column.Position)
        |> List.mapi (fun i column ->
            g [
                SVGAttr.Transform (sprintf "translate(%d, %d)" (columnPositionsMap.[i] * (cellSize + cellPadding)) 0)
                Style [
                    Transition "all 1s ease-in-out"
                    TransitionDelay (sprintf "%dms" (50 * columnPositionsMap.[i])) ] ]
              [
                renderRect "" 0 0 cellSize columnLabelHeight 0.5 0 []
                g [ SVGAttr.Transform (sprintf "translate(%f %d)" (float cellSize * 0.5) (columnLabelHeight - labelPadding)) ] [
                    text [ SVGAttr.Transform (sprintf "rotate(-90)") ] [ str column.Name ]
                ]
              ])

    let cells =
        data.Cells
        |> List.map (fun cell ->
            renderRect
                "adjacency-matrix__cell"
                (columnPositionsMap.[cell.Column] * (cellSize + cellPadding))
                (rowPositionsMap.[cell.Row] * (cellSize + cellPadding))
                cellSize cellSize cell.Value
                (cell.Row + cell.Column)
                [])

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
        | Ascending ->
            span [ OnClick (fun e -> dispatch SortOrderChanged) ] [ str "ascending" ]
        | Descending ->
            span [ OnClick (fun e -> dispatch SortOrderChanged) ] [ str "descending" ]
        | Shuffle ->
            span [ OnClick (fun e -> dispatch SortOrderChanged) ] [ str "shuffle" ]
    div [ Class "sort-order" ] [
        str "Order by: "
        order
    ]

let view (model : Model) dispatch =
    div [] [
        renderSortOrder model.SortOrder dispatch
        renderMatrix 50 model.Data
    ]
