module Shuffle

let rand = new System.Random()

let swap (a: _[]) x y =
    let tmp = a.[x]
    a.[x] <- a.[y]
    a.[y] <- tmp

let shuffleArray array =
    let shuffledArray = Array.copy array
    Array.iteri (fun i _ -> swap shuffledArray i (rand.Next(i, Array.length shuffledArray))) shuffledArray
    shuffledArray

let shuffleList list =
    list |> Array.ofList |> shuffleArray |> List.ofArray
