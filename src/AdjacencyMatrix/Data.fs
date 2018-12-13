module AdjacencyMatrix.Data

open Tree
open AdjacencyMatrix.Types

let row1 =
    Branch ({ Name = "Row 1" },
        [ Leaf { Name = "Row 1-1" }
          Branch ({ Name = "Row 1-2" }, [
              Leaf { Name = "Row 1-2-1" }
              Leaf { Name = "Row 1-2-2" }
              Leaf { Name = "Row 1-2-3" }
            ] )
          Leaf { Name = "Row 1-3" }
        ])

let row2 =
    Branch ({ Name = "Row 2" },
        [ Leaf { Name = "Row 2-1" }
          Branch ({ Name = "Row 2-2" }, [
              Leaf { Name = "Row 2-2-1" }
              Leaf { Name = "Row 2-2-2" }
              Leaf { Name = "Row 2-2-3" }
            ] )
          Leaf { Name = "Row 2-2" }
        ])

let row3 =
    Branch ({ Name = "Row 3" },
        [ Leaf { Name = "Row 3-1" }
          Branch ({ Name = "Row 3-2" }, [
              Leaf { Name = "Row 3-2-1" }
              Leaf { Name = "Row 3-2-2" }
              Leaf { Name = "Row 3-2-3" }
            ] )
          Leaf { Name = "Row 3-2" }
        ])

let col1 =
    Branch ({ Name = "Col 1" },
        [ Leaf { Name = "Col 1-1" }
          Branch ({ Name = "Col 1-2" }, [
              Leaf { Name = "Col 1-2-1" }
              Leaf { Name = "Col 1-2-2" }
              Leaf { Name = "Col 1-2-3" }
            ] )
          Leaf { Name = "Col 1-3" }
        ])

let col2 =
    Branch ({ Name = "Col 2" },
        [ Leaf { Name = "Col 2-1" }
          Branch ({ Name = "Col 2-2" }, [
              Leaf { Name = "Col 2-2-1" }
              Leaf { Name = "Col 2-2-2" }
              Leaf { Name = "Col 2-2-3" }
            ] )
          Leaf { Name = "Col 2-2" }
        ])

let col3 =
    Branch ({ Name = "Col 3" },
        [ Leaf { Name = "Col 3-1" }
          Branch ({ Name = "Col 3-2" }, [
              Leaf { Name = "Col 3-2-1" }
              Leaf { Name = "Col 3-2-2" }
              Leaf { Name = "Col 3-2-3" }
            ] )
          Leaf { Name = "Col 3-2" }
        ])
