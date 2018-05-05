module Puzzle10

    type FirstName =
        | Amanda = 0B1
        | Belinda = 0B10
        | Carol = 0B100
        | Debbie = 0B1000

    let firstNames = [ FirstName.Amanda; FirstName.Belinda; FirstName.Carol; FirstName.Debbie ]

    type LastName =
        | Clark = 0B1
        | Johnson = 0B10
        | Meyer = 0B100
        | Smith = 0B1000

    let lastNames = [ LastName.Clark; LastName.Johnson; LastName.Meyer; LastName.Smith ]

    type Jacket =
        | Blue = 0B1
        | Green = 0B10
        | Red = 0B100
        | Yellow = 0B1000

    let jackets = [ Jacket.Blue; Jacket.Green; Jacket.Red; Jacket.Yellow ]

    type Shoes =
        | Brown = 0B1
        | Tan  = 0B10
        | White  = 0B100
        | Black = 0B1000

    let shoes = [ Shoes.Black; Shoes.Brown; Shoes.Tan; Shoes.White ]

    let thumbprint (f:FirstName) (l:LastName) (j:Jacket) (s:Shoes) :uint32 =
        let fi = uint32(f)
        let li = uint32(l) <<< 4
        let ji = uint32(j) <<< 8
        let si = uint32(s) <<< 12
        fi + li + ji + si

    type Person =
        {
            FirstName:FirstName;
            LastName:LastName;
            Jacket:Jacket;
            Shoes:Shoes;
            Thumbprint:uint32
        }

    let newPerson (f:FirstName) (l:LastName) (j:Jacket) (s:Shoes) : Person =
        {
            FirstName = f;
            LastName = l;
            Jacket = j;
            Shoes = s;
            Thumbprint = thumbprint f l j s
        }

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

    let rec personTest (x:List<Person>) = 
        personTest' 0u x
    and personTest' a l = 
        match l with 
            | [] -> true
            | hd::tl -> 
                match a &&& hd.Thumbprint with 
                | 0u -> personTest' (a|||hd.Thumbprint) tl
                | _ -> false
