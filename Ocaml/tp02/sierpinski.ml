(*-------------={TP 2:Fractales}=--------------*)

(*----={2.2.2:sierpinski}=----*)

#load "graphics.cma";;
open Graphics ;;
open_graph "";;
open_graph "300x100" ;;

let draw_line (x, y) (z, t) =
  moveto x y ;
  lineto z t ;;

let sponge (x, y) n =
  clear_graph ();
  if n<=0 then
    invalid_arg" n must be positive"
  else
    let rec draw_sponge (x,y) n =
    match n with
      |1 -> fill_rect x y 1 1
      |n -> let n= n/3 in
            draw_sponge ((x+2*n),y) n;
            draw_sponge ((x+2*n,y+n)) n;
            draw_sponge ((x+2*n),(y+2*n)) n;
            draw_sponge ((x+n),y) n;
            draw_sponge ((x+n),(y+2*n)) n;
            draw_sponge (x, y) n;
            draw_sponge (x,(y+n)) n;
            draw_sponge (x ,(y+2*n)) n;
    in
    draw_sponge (x, y) n;;


let sierpinski  (x, y) (z, t) (v, w) n =
  clear_graph ();
  if n<0 then invalid_arg"n must be natural"
  else
    let rec sierpinski_rec  (x, y) (z, t) (v, w) n =
      let abm = ((x+z)/2,(y+t)/2) in
      let bcm= ((z+v)/2,(t+w)/2) in
      let acm=((v+x)/2,(w+y)/2) in match n with
        |0 ->
          draw_line (x, y) (z, t) ;
          draw_line (z, t) (v, w) ;
          draw_line (v, w) (x, y) ;
        |_ ->
          sierpinski_rec (x, y) abm acm (n-1);
          sierpinski_rec abm (z, t) bcm  (n-1);
          sierpinski_rec bcm (v,w) acm (n-1) ;
    in
    sierpinski_rec  (x, y) (z, t) (v, w) n;;

sierpinski (100,100) (300,100) (200,300) 8;;


