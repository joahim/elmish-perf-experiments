module Components.CollapsibleTree

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.React
open Fable.Helpers.React

type Options =
    { width : int
      height : int }

type Tree =
    | Leaf of string
    | Node of string * Tree array

type Props =
    | Data of Tree
    | Width of int
    | Height of int
    | Options of Options

let inline CollapsibleTree(props : Props list) : ReactElement =
    ofImport "default" "../Components/CollapsibleTree" 
        (keyValueList CaseRules.LowerFirst props) []
