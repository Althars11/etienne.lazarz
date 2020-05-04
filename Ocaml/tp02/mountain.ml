(*-------------------=={TP 2 Caml: Fractales}==-----------------*)

(*----={2.1.1: Mountains}=----*)

#load "graphics.cma";;
open Graphics ;;
open_graph "";;
open_graph "300x100" ;;

let draw_line (x, y) (z, t) =
  moveto x y ;
  lineto z t ;;

let montagne nb coord1 coord2 =
  if nb<=0 then invalid_arg "nb must be positive"
  else
    let rec montagne_rec nb (x, y) (z, t) = match nb with
      |0 -> draw_line (x, y) (z, t)
      |_ -> let nh1= (y+t)/2 + Random.int(10*nb) and nh2= (x+z)/2 in
          montagne_rec (nb-1) (x, y) (nh2, nh1) ;
          montagne_rec (nb-1) (nh2, nh1) (z, t) ;
in montagne_rec nb coord1 coord2 ;;

montagne 9 (100,100) (300,100);;
