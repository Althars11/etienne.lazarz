(*-----------=={Caml: TP 3: List Toolbox and Midterms}==-----------*)


(*------=={Midterms}==------*)


#use "list_tools.ml";;

(*----={3.1: recompose}=----*)



(*----={3.2: decompose}=----*)

let decompose n =
    let rec decompose_rec n v =
      match n with
  |n when n<=0 -> invalid_arg" n must be positive "
  |n when n=v -> [n]
  |_ ->
    if n mod v = 0 then v::decompose_rec (n/v) v
    else decompose_rec n (v+1) in
decompose_rec n 2;;

(*----={4.1: assoc}=----*)

let assoc n poly =
  let rec assoc_rec n poly = match (n,poly) with
    |(_,[])-> 0
    |(0,(x,y)::r1)->y
    |(_,(x,y)::r1)-> assoc_rec (n-1) r1
  in assoc_rec n poly;;
  

(*----={4.2: add_poly}=----*)



(*----={5.1: rivière}=----*)



(*----={5.2: rencontre}=----*)


