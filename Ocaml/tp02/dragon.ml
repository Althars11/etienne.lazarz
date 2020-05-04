(*-------------=={TP 2 Caml: Fractales}==---------------*)

(*----={2.1.2: Dragon}=----*)


#load "graphics.cma";;
open Graphics ;;
open_graph "";;
open_graph "300x100" ;;

let draw_line (x, y) (z, t) =
  moveto x y ;
  lineto z t ;;

let dragon nb coord1 coord2 =
  clear_graph ();
  if nb<=0 then  invalid_arg "nb must be positive"
  else
    let rec dragon_rec nb (x, y) (z, t) = match nb with
      |0 -> draw_line (x,y) (z, t)
      |_ ->
        let u=(x+z)/2+(t-y)/2 and v=(y+t)/2-(z-x)/2 in
        dragon_rec (nb-1) (x, y) (u, v) ;
        dragon_rec (nb-1) (z, t) (u, v) ;
    in dragon_rec nb coord1 coord2 ;;

dragon 19 (150,150) (350,350) ;;
