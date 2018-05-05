

module Library
    /// this generates a list of 'things' applying the function f to a list of 
    /// elements l. It is really intended to be used as a pipeline:
    ///    [fn] |> combine l1 |> combine l2 |> combine l3
    // where l1, l2 and l3 are lists of t1's, t2's and t3's. 
    /// fn has the signature fn (l1:list<t1>) (l2:list<t2> (l3:list<t3>) : 'a
    /// combine returns list<'a>
    let rec combine a l =
        match a with
        |[] -> []
        |hd::tl -> List.append (combine' hd l)  (combine tl l)
    and combine' a' l =
        match l with
        | [] -> []
        | hd::tl -> hd a'::combine' a' tl
    
    // Takes a rules object and a list of things. For each thing the rule
    // return true of false - if its false the thing is removed the list
    let rec filter r l =
        match l with
        |[] -> []
        | hd::tail ->
            match (r hd) with
            | true -> hd :: filter r tail
            | false -> filter r tail

// this is an example of how this all might work
    //let things = 
    //    [ newPerson] 
    //    |> combine firstNames 
    //    |> combine lastNames 
    //    |> combine jackets 
    //    |> combine shoes 
    //    |> filter rules

    // test that the attributes on all people in a putative solution are unique. This is
    // the equivalent of the c#
    //    bool TestValidCombination(List<uint> thumbprints)
    //    {
    //        uint a = 0u;
    //        foreach(uint thumbprint in thumbprints)
    //        {
    //           if (a & thumbprint == 0)
    //           {
    //               a = a | thumbprint;
    //           }
    //           else
    //           {
    //               return false;
    //            }
    //         }
    //
    //         return true;
    //    }
    let rec test x = 
        test' 0u x
    and test' a = function
        | [] -> true
        | hd::tl -> 
            match a &&& hd with 
            | 0u -> test' (a|||hd) tl
            | _ -> false

    /// builds the list of person combinations. This is done via a pipeline
    let rec buildlist' a ll =
        match ll with
        |[] -> []
        |h::t -> List.append [a::h] (buildlist' a t)

    let rec buildlist l ll =
        match l with 
        |[] -> []
        |h::t -> List.append (buildlist' h ll)  (buildlist t ll)
