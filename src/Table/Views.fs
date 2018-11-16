module Table.Views

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Elmish.React
open Table.Types

let renderTable model =
    let renderRow row =
        row
        |> Array.mapi 
               (fun i col -> 
               fragment [ Fable.Helpers.React.Props.Key(string i) ] 
                   [ lazyView (fun col -> td [] [ str col ]) col ])
        |> ofArray
    
    let sortRows sortOrder rows =
        match sortOrder with
        | Ascending -> rows |> Array.sortBy (fun (key, row) -> key)
        | Descending -> rows |> Array.sortByDescending (fun (key, row) -> key)
    
    let renderRows sortOrder rows =
        sortRows sortOrder rows
        |> Array.map 
               (fun (key, row) -> 
               fragment [ Fable.Helpers.React.Props.Key(string key) ] 
                   [ lazyView (fun row -> tr [] [ renderRow row ]) row ])
        |> ofArray
    
    table [ ClassName "table" ] 
        [ tbody [] [ renderRows model.SortOrder model.Rows ] ]

let view model dispatch =
    div [] [ button [ ClassName "button"
                      OnClick(fun _ -> dispatch ChangeSortOrder) ] 
                 [ str "Sort" ]
             renderTable model ]
