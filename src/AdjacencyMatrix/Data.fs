module AdjacencyMatrix.Data

open Tree
open AdjacencyMatrix.Types

let row1 =
    Branch ({ Id = "1" ; Name = "Row 1" },
        [ Leaf { Id = "1-1" ; Name = "Row 1-1" }
          Branch ({ Id = "1-2" ; Name = "Row 1-2" }, [
              Leaf { Id = "1-2-1" ; Name = "Row 1-2-1" }
              Leaf { Id = "1-2-2" ; Name = "Row 1-2-2" }
              Leaf { Id = "1-2-3" ; Name = "Row 1-2-3" }
            ])
          Leaf { Id = "1-3" ; Name = "Row 1-3" }
        ])

let row2 =
    Branch ({ Id = "2" ; Name = "Row 2" },
        [ Leaf { Id = "2-1" ; Name = "Row 2-1" }
          Branch ({ Id = "2-2" ; Name = "Row 2-2" }, [
              Leaf { Id = "2-2-1" ; Name = "Row 2-2-1" }
              Branch ({ Id = "2-2-2" ; Name = "Row 2-2-2" }, [
                  Leaf { Id = "2-2-2-1" ; Name = "Row 2-2-2-1" }
                  Leaf { Id = "2-2-2-2" ; Name = "Row 2-2-2-2" }
                  Leaf { Id = "2-2-2-3" ; Name = "Row 2-2-2-3" }
                  Leaf { Id = "2-2-2-4" ; Name = "Row 2-2-2-4" }
              ])
              Leaf { Id = "2-2-3" ; Name = "Row 2-2-3" }
            ])
          Leaf { Id = "2-3" ; Name = "Row 2-3" }
        ])

let row3 =
    Branch ({ Id = "3" ; Name = "Row 3" },
        [ Leaf { Id = "3-1" ; Name = "Row 3-1" }
          Branch ({ Id = "3-2" ; Name = "Row 3-2" }, [
              Leaf { Id = "3-2-1" ; Name = "Row 3-2-1" }
              Leaf { Id = "3-2-2" ; Name = "Row 3-2-2" }
              Leaf { Id = "3-2-3" ; Name = "Row 3-2-3" }
            ])
          Leaf { Id = "3-3" ; Name = "Row 3-3" }
        ])

let col1 =
    Branch ({ Id = "1" ; Name = "Col 1" },
        [ Leaf { Id = "1-1" ; Name = "Col 1-1" }
          Branch ({ Id = "1-2" ; Name = "Col 1-2" }, [
              Leaf { Id = "1-2-1" ; Name = "Col 1-2-1" }
              Leaf { Id = "1-2-2" ; Name = "Col 1-2-2" }
              Branch ({ Id = "1-2-3" ; Name = "Col 1-2-3" }, [
                  Leaf { Id = "1-2-3-1" ; Name = "Col 1-2-3-1" }
                  Leaf { Id = "1-2-3-2" ; Name = "Col 1-2-3-2" }
                  Leaf { Id = "1-2-3-3" ; Name = "Col 1-2-3-3" }
                  Leaf { Id = "1-2-3-4" ; Name = "Col 1-2-2-4" }
              ])
              Leaf { Id = "1-2-4" ; Name = "Col 1-2-4" }
            ])
          Leaf { Id = "1-3" ; Name = "Col 1-3" }
        ])

let col2 =
    Branch ({ Id = "2" ; Name = "Col 2" },
        [ Leaf { Id = "2-1" ; Name = "Col 2-1" }
          Branch ({ Id = "2-2" ; Name = "Col 2-2" }, [
              Leaf { Id = "2-2-1" ; Name = "Col 2-2-1" }
              Leaf { Id = "2-2-2" ; Name = "Col 2-2-2" }
              Leaf { Id = "2-2-3" ; Name = "Col 2-2-3" }
            ])
          Leaf { Id = "2-3" ; Name = "Col 2-3" }
        ])

let col3 =
    Branch ({ Id = "3" ; Name = "Col 3" },
        [ Leaf { Id = "3-1" ; Name = "Col 3-1" }
          Branch ({ Id = "3-2" ; Name = "Col 3-2" }, [
              Leaf { Id = "3-2-1" ; Name = "Col 3-2-1" }
              Leaf { Id = "3-2-2" ; Name = "Col 3-2-2" }
              Leaf { Id = "3-2-3" ; Name = "Col 3-2-3" }
            ])
          Leaf { Id = "3-3" ; Name = "Col 3-3" }
        ])

let cells =
    [ { Row = "1-1" ; Column = "1-2" ; Value = 1.0 }
      { Row = "2" ; Column = "2" ; Value = 0.9 }
      { Row = "3" ; Column = "3" ; Value = 0.8 }
      { Row = "1-1" ; Column = "3" ; Value = 0.8 }
      { Row = "3" ; Column = "1-2" ; Value = 0.7 }
      { Row = "3" ; Column = "1-2-3-1" ; Value = 0.6 }
      { Row = "1-1" ; Column = "1-2-3-1" ; Value = 0.5 }
      { Row = "1-2-2" ; Column = "1-2-3-1" ; Value = 0.4 }
    ]
