(*---------=={Caml: TP 3: List Toolbox and Midterms}==----------*)


(*------=={List Toolbox}==------*)


(*----={1.1.1: length}=----*)

let rec length = function
  |[] -> 0
  |e::list ->1+(length list) ;;

(*----={1.1.2: product}=----*)

let rec product = function
  |[]->1
  |e::list -> e*(product list) ;;

(*----={1.1.3: nth}=----*)

let nth i list =
  if i<0 then invalid_arg"nth: index must be a natural"
  else
    let rec nth_rec= function
      |(_,[])->failwith"nth: list is too short"
      |(0,e::l)->e
      |(i,e::l)-> nth_rec (i-1,l)
    in nth_rec (i,list);;

(*----={1.1.4: search_pos}=----*)

let rec search_pos pos = function
  |[] -> failwith"search_pos: not found"
  |e::l -> if e<>pos then 1+search_pos pos l
    else 0;;

(*----={1.1.5: sum_digits}=----*)

let sum_digits x =
  let rec sum_digits_rec x=
    if x<10 then x
    else(x mod 10)+sum_digits_rec (x/10)
  in sum_digits_rec x;;

(*----={1.1.6: common}=----*)

let rec commun liste1 liste2 =
  match (liste1,liste2) with
    |([],[]) |(_,[]) |([],_) -> 0
    |(e1::r1,e2::r2) when e1=e2 -> e1
    |(e1::r1,e2::r2) -> if e1>e2 then commun r2 (e1::r1)
      else commun r1 (e2::r2) ;;

(*----={1.2.1: init_list}=----*)

let init_list n x =
  if n<0 then invalid_arg "init_list : n must be a natural"
  else
    let rec init_list_rec n x=
      match n with
        |0 -> []
        |n -> x::(init_list_rec (n-1) x)
    in init_list_rec n x ;;

(*----={1.2.2: put_list}=----*)

let rec put_list v i list =
  if i<0 then invalid_arg"i must be positive"
  else
      let rec put_list_rec v (i,list) = match (i,list) with
        |(_, []) -> list
        |(0, e::reste)-> v::reste
        |(i, e::reste) -> e::put_list_rec v (i-1,reste)
      in put_list_rec v (i , list) ;;

(*----={1.3.1: init_board}=----*)

let  init_board l c v =
  if l<0|| c<0 then []
  else
    let rec init_board_rec (l, c) v =
      match l with
        |0-> []
        |l->(init_list c v)::init_board_rec (l-1,c) v
    in init_board_rec (l, c) v;;

(*----={1.3.2: get_cell}=----*)

let get_cell (x, y) board =
  nth y (nth x board) ;;

(*----={1.3.3: put_cell}=----*)

let put_cell v (x, y) board =
  put_list (put_list v y (nth x board)) x board ;;
