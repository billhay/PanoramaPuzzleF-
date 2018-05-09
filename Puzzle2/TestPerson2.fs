module TestPerson2

type Name = 
    | Jack = 1
    | Jill = 2

let names = [ Name.Jack; Name.Jill ]

type Pet =
    | Dog = 4
    | Cat = 8

let pets = [ Pet.Dog; Pet.Cat; ]

type TestPerson =
    {
        Name:Name;
        Pet:Pet;
        Thumbprint: uint32
    }

let makeTestPerson (name:Name) (pet:Pet) = 
    {
        Name = name;
        Pet = pet;
        Thumbprint = ((uint32)name) + ((uint32)pet);
    }

let rec personTest (x:List<TestPerson>) = 
    personTest' 0u x
and personTest' a l = 
    match l with 
        | [] -> true
        | hd::tl -> 
            match a &&& hd.Thumbprint with 
            | 0u -> personTest' (a|||hd.Thumbprint) tl
            | _ -> false
        
let testRules (p:TestPerson)  = 
    not ((p.Name = Name.Jack) && (p.Pet = Pet.Cat))

let testPerson2String (p:TestPerson) =
    sprintf "%6s %6s" (p.Name.ToString()) (p.Pet.ToString())

let printTestPersonList (l:list<TestPerson>) = l |> List.iter  (fun p -> printf "[%6s; %6s]\n" (p.Name.ToString()) (p.Pet.ToString())) 

let printTestPersonListList (ll:list<List<TestPerson>>) =
      ll |> List.iter (fun l -> printTestPersonList l; printf "---------------------------\n")




