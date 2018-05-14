

module Library

    open System

    let enumToList<'a> = (Enum.GetValues(typeof<'a>) :?> ('a [])) |> Array.toList

    // given:
    // a:list<'a>
    // b:list<'b>
    // fn 'a->'b->c'
    // this produces all permutations of a and b, with the output being
    // whatever fn 'a 'n returns
    let rec outerProduct fn a b =
        match b with 
        |[] -> []
        |h::t -> List.append (List.map (fn h) a) (outerProduct fn a t)

    /// builds the list of person combinations. This is done via a pipeline
    let rec buildlist l ll =
        let rec buildlist' a ll =
            match ll with
            |[] -> []
            |h::t -> List.append [a::h] (buildlist' a t)

        match l with 
        |[] -> []
        |h::t -> List.append (buildlist' h ll)  (buildlist t ll)

    /// This takes a list of objects, and filters out any duplicates. It takes as a parameter
    /// a predicate of the form 'x -> 'y, where 'x is the type of object in the list, and 'y
    /// is something that implements comparable, and is unique to an individual 'x.
    let distinct fn = 
        List.map fn >>  // turn value into (key, value) via function fn
        Map.ofList >>   // now make a map of the new (k,v) list. For duplicate keys the last one is retained.
        Map.toList >>   // convert the may to a (now) distinct list of <k,v> tuples
        List.map snd    // convert to just a list of <v>

