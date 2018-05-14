module Puzzle10

    open Library

    type FirstName =
        | Amanda  = 0B1
        | Belinda = 0B10
        | Carol   = 0B100
        | Debbie  = 0B1000

    let firstNames = enumToList<FirstName> 

    type LastName =
        | Clark   = 0B10000
        | Johnson = 0B100000
        | Meyer   = 0B1000000
        | Smith   = 0B10000000

    let lastNames = enumToList<LastName>

    type Jacket =
        | Blue   = 0B100000000
        | Green  = 0B1000000000
        | Red    = 0B10000000000
        | Yellow = 0B100000000000

    let jackets = enumToList<Jacket>

    type Shoes =
        | Brown  = 0B1000000000000
        | Tan    = 0B10000000000000
        | White  = 0B100000000000000
        | Black  = 0B1000000000000000

    let shoes = enumToList<Shoes>

    type Person =
        {
            FirstName:FirstName;
            LastName:LastName;
            Jacket:Jacket;
            Shoes:Shoes;
            Thumbprint:uint32;
            toString:string;
        } 

    let newPerson (f:FirstName) (l:LastName) (j:Jacket) (s:Shoes) : Person =
        let toString = sprintf "%10s%10s%10s%10s" (f.ToString()) (l.ToString()) (j.ToString()) (s.ToString())
        {
            FirstName = f;
            LastName = l;
            Jacket = j;
            Shoes = s;
            Thumbprint = uint32(f) + uint32(l) + uint32(j) + uint32(s);
            toString = toString;
        }

    // These are the domain specific rules, which I found easier to write backwards. So
    // the rules say Amanda does not wear the red jacket. If it turns out she does (very first term
    // in the list of predicates) the rules method returns 'false'
    let rules (p:Person) :bool = 
        let b = (p.FirstName = FirstName.Amanda && p.Jacket = Jacket.Red) ||
                (p.FirstName = FirstName.Amanda && p.Shoes = Shoes.Brown) ||
                (p.FirstName = FirstName.Amanda && p.LastName = LastName.Meyer) ||
                (p.Jacket = Jacket.Red && p.Shoes = Shoes.Brown) ||
                (p.LastName = LastName.Meyer && p.Shoes = Shoes.Brown) ||
                (p.LastName = LastName.Meyer && p.Jacket = Jacket.Red) ||

                // rule 2
                (p.LastName = LastName.Clark && p.Shoes = Shoes.White) ||
                (p.LastName = LastName.Clark && p.FirstName = FirstName.Carol) ||

                // rule 3
                (p.Shoes = Shoes.Tan && p.Jacket = Jacket.Red) ||
                (p.Shoes = Shoes.Tan && p.Jacket = Jacket.Blue) ||

                // rule 4
                (p.Jacket = Jacket.Yellow && p.Shoes = Shoes.Brown) ||
                (p.Jacket = Jacket.Yellow && p.FirstName = FirstName.Amanda) ||

                // rule 5
                (p.Jacket = Jacket.Blue && p.Shoes = Shoes.Black) ||
                (p.Jacket = Jacket.Blue && p.LastName = LastName.Johnson) ||
                (p.Shoes = Shoes.Black && p.LastName = LastName.Johnson) ||

                // rule 6
                (p.FirstName = FirstName.Amanda && p.Shoes = Shoes.Black) ||
                (p.FirstName = FirstName.Amanda && p.Jacket = Jacket.Green) ||
                (p.FirstName = FirstName.Belinda && p.Jacket = Jacket.Green) ||
                (p.FirstName = FirstName.Belinda && p.Shoes = Shoes.Black)
        not b

    // tests if a potential solution of an N Person list has each
    // person unique - i.e. no single attribute (say FirstName)
    // is duplicated by the other (N-1) members of the list
    let personTest (x:List<Person>) = 
        let rec personTest' a l = 
            match l with 
                | [] -> true
                | hd::tl -> 
                    match a &&& hd.Thumbprint with 
                    | 0u -> personTest' (a|||hd.Thumbprint) tl
                    | _ -> false

        personTest' 0u x

    let printSolutionList ll =
        ll |> List.iter (fun x -> 
            x |> List.iter (fun (p:Person) -> printfn "%s" (p.toString))
            printfn "")
        printfn ""

    /// this maps a list<Person> to the tupple (k, List<Person>)
    /// for insertion into a map. k is the 'or' of the individual 
    /// id's
    let personListToTupple x = 
        let k = x |> List.fold (fun a x -> a||| x.Thumbprint) 0u
        (k, x)

