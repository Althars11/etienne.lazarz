(* -----------------=={TP 2 Caml: Fractales}==-----------------------*)

(*-----={Patterns}=-----*)

(*----={1.1.1: Build me a line}=----*)

let rec build_line n str=
  if n=0 then ""
  else
    str^build_line (n-1) str ;;

build_line 4 "@" ;;

(*----={1.1.2: Draw me a square}=----*)

let square nline str =
  if nline<=0 then ()
  else
    let rec square_rec nb str =
      if nb=0 then ()
      else
        begin
          print_string str ;
          print_newline () ;
          square_rec (nb-1) str ;
        end in
    square_rec nline (build_line nline str) ;;

square 6 "@" ;;

(*----={1.1.3: Draw me a square-bis}=----*)

(*V1*)

(*let square2 nb (str1,str2) =
  if nb<=0 then invalid_arg"nb must be a positiv"
  else
    let a=str1^str2 and b=str2^str1 and switch=nb in
    let rec square2_rec nb a b = match nb with
      |0 -> ()
      |nb when (switch-nb)mod 2=0 ->
       (begin
          print_string (build_line switch a);
          print_newline ();
          square2_rec (nb-1) a b;
        end)
      |_ ->
       (begin
          print_string (build_line switch b);
          print_newline ();
          square2_rec (nb-1) a b;
        end) in
  square2_rec nb a b;;*)

(*V2*)

let square2 nb (str1,str2) =
  if nb<=0 then invalid_arg"nb must be positiv"
  else
    let a=str1^str2 and b=str2^str1 and switch=nb in
    let rec square2_rec nb a b=match nb with
      |0->()
      |_->
          print_string (build_line switch a);
          print_newline ();
          square2_rec (nb-1) b a;
    in
    square2_rec nb a b;;

square2 5 ("@", "m");;

(*----={1.1.4: Draw me a triangle}=----*)

let triangle nb str =
  if nb<=0 then invalid_arg"n must be positiv"
  else
    let stocker = str in
    let rec triangle_rec nb str stocker =match nb with
      |0 -> ()
      |_ ->
        print_string str;
        print_newline ();
        triangle_rec (nb-1) (str^stocker) stocker;
    in
    triangle_rec nb str stocker;;

triangle 6 "t" ;;

(*--Bonus--*)
(*----={1.2.1: Draw me a pyramid}=----*)

(* let pyramid n (str1, str2) =
  if m <=0 then invalid_arg "n must be a positiv"
  else
    let rec pyra_rec n str1 str2 m o = match n with
      |0 -> ()
      |_ -> 5 *)

(*----={1.2.2: Draw me a cross}=----*)
