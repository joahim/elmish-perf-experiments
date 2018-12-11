module AdjacencyMatrix.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props

open AdjacencyMatrix.Types

let renderCell (cellSize : int) (cell : Cell) rowOffset columnOffset rowPosition columnPosition =
    rect [ Class "adjacency-matrix__cell"
           SVGAttr.Width cellSize
           SVGAttr.Height cellSize
           SVGAttr.FillOpacity cell.Value
           Style [ Transition "all 1s ease-in-out"
                   TransitionDelay (sprintf "%dms" (50 * (cell.Row + cell.Column)))
                   Transform (sprintf "translate(%dpx, %dpx)"
                             ((columnPosition * cellSize) + rowOffset)
                             ((rowPosition * cellSize) + columnOffset))
           ] ] [ ]

let renderMatrix cellSize (data : Data) =
    let rowLabelSize = 50
    let rowLabelOffset = 10
    let rowOffset = rowLabelSize + rowLabelOffset

    let columnLabelSize = 50
    let columnLabelOffset = 10
    let columnOffset = columnLabelSize + columnLabelOffset

    let matrixWidth = data.Columns.Length * cellSize
    let matrixHeight = data.Rows.Length * cellSize

    let positionMap nodes =
        nodes
        |> List.mapi (fun i el -> (el.Position, i))
        |> Map.ofList

    let rowPositionsMap = positionMap data.Rows
    let columnPositionsMap = positionMap data.Columns

    let rowLabels =
        data.Rows
        |> List.sortBy (fun row -> row.Position)
        |> List.mapi (fun i row ->
            text
                [ Style
                    [ Transition "all 1s ease-in-out"
                      TransitionDelay (sprintf "%dms" (50 * rowPositionsMap.[i]))
                      Transform (sprintf "translate(%fpx, %fpx)"
                                (float rowLabelSize)
                                ((float columnOffset) + (float rowPositionsMap.[i] + 0.5) * (float cellSize))) ] ]
                [ str row.Name ])

    let columnLabels =
        data.Columns
        |> List.sortBy (fun column -> column.Position)
        |> List.mapi (fun i column ->
            text
                [ Style
                    [ Transition "all 1s ease-in-out"
                      TransitionDelay (sprintf "%dms" (50 * columnPositionsMap.[i]))
                      Transform (sprintf "translate(%fpx, %fpx)"
                                ((float rowOffset) + (float columnPositionsMap.[i] + 0.5) * (float cellSize))
                                (float columnLabelSize)) ] ]
                    [ str column.Name ] )

    let cells =
        data.Cells
        |> List.map (fun cell ->
            renderCell
                cellSize cell
                (rowLabelSize + rowLabelOffset)
                (columnLabelSize + columnLabelOffset)
                rowPositionsMap.[cell.Row]
                columnPositionsMap.[cell.Column])

    svg [ Class "adjacency-matrix"
          SVGAttr.Width (matrixWidth + rowLabelSize + rowLabelOffset)
          SVGAttr.Height (matrixHeight + columnLabelSize + columnLabelOffset)
        ] [ g [ Class "adjacency-matrix__row-labels" ] rowLabels
            g [ Class "adjacency-matrix__column-labels" ] columnLabels
            g [ Class "adjacency-matrix__cells" ] cells
        ]

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
