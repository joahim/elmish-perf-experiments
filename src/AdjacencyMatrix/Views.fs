module AdjacencyMatrix.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props

open AdjacencyMatrix.Types

type Point = {
    X : int
    Y : int
}

let square position width height color =
    rect [ SVGAttr.X position.X
           SVGAttr.Y position.Y
           SVGAttr.Width width
           SVGAttr.Height height
           SVGAttr.Fill color
           SVGAttr.FillOpacity 0.5
           Style [
               Transition "all 1000ms ease-in-out"
               Transform "translate(0px, 0px)"
           ] ] [ ]

let view (model : Model) dispatch =
    let bars =
        match model.Position with
        | Left ->
            [ square { X = 0 ; Y = 0 } 5 100 "cyan"
              square { X = 5 ; Y = 0 } 5 100 "magenta"
              square { X = 10 ; Y = 0 } 5 100 "yellow" ]
        | Right ->
            [ square { X = 95 ; Y = 0 } 5 100 "cyan"
              square { X = 90 ; Y = 0 } 5 100 "magenta"
              square { X = 85 ; Y = 0 } 5 100 "yellow" ]
    div [] [ svg [
        OnClick (fun e -> dispatch Animate)
        SVGAttr.ViewBox "0 0 100 100"
        // SVGAttr.Width "100%"
        // SVGAttr.Height "100vh"
        ] bars ]
