                                (*---------={Game of Life}=----------*)


                (*-------=Boite a outils=-------*)
  
# load "graphics.cma" ;;
open Graphics ;;


let color= rgb 255 0 0 ;;
#use "list_tools.ml";;

let open_window size=open_graph(""^string_of_int 300^"x"^string_of_int(300+20));;

                (*-------=de la matrice a l'affichage=--------*)

(*----={1:draw_cell}=----*)

  let draw_cell (x, y) size color =
    let grey= rgb 127 127 127 in
      set_color grey ;
      draw_rect (x+1) (y+1) size size ;
      set_color color ;
      fill_rect (x+2) (y+1) (size-1) (size-1);;

 (*----={2:draw_board}=----*)

  let cell_color = function
    |0 ->white
    |_ -> black ;;

  let board =
    [[1; 1; 1; 1; 1; 1; 1; 1; 1; 1];
     [0; 0; 0; 0; 0; 0; 0; 0; 0; 0];
     [1; 0; 1; 0; 1; 0; 1; 0; 1; 0];
     [0; 1; 0; 1; 0; 1; 0; 1; 0; 1];
     [0; 0; 0; 0; 0; 0; 0; 0; 0; 0];
     [1; 1; 1; 1; 1; 1; 1; 1; 1; 1];
     [0; 0; 0; 0; 0; 0; 0; 0; 0; 0];
     [1; 0; 1; 0; 1; 0; 1; 0; 1; 0];
     [0; 1; 0; 1; 0; 1; 0; 1; 0; 1];
     [0; 0; 0; 0; 0; 0; 0; 0; 0; 0]] ;;

  let draw_board board size =
    let rec board_rec board size n=
      match board with
        |[] -> ()
        |e1::board -> let rec longueur x y board size e1 = match e1 with
              |[]-> board_rec board size (n+size)
              |e2::e1-> draw_cell (x, y) size (cell_color e2);
                   longueur  x (y+size) board size e1;
        in longueur n 0 board size e1
    in board_rec board size 0 ;;


                                     (*--------=Le jeu=---------*)

               (*-------=les regles=--------*)

  (*----={1:rules0}=----*)

  let rules0 cell neighbours = match (cell,neighbours) with
    |(1,x)-> if x=2 ||x=3 then 1 else 0
    |(0,x)-> if x=3 then 1 else 0 ;;

  (*----={2:count_neighbours}=----*)



  let count_neighbours (i, j) board size=
      match (i,j) with
	|(0,y)->if y=0 then
	    get_cell ((i),(j+1)) board +
	      get_cell ((i+1),(j+1)) board +
	      get_cell ((i+1),j) board 
	  else
	    if y=size-1 then
	      get_cell ((i+1),j) board +
		get_cell ((i+1),(j-1)) board +
		get_cell ((i),(j-1)) board 
	    else 
	      get_cell ((i),(j+1)) board +
		get_cell ((i+1),(j+1)) board +
		get_cell ((i+1),j) board +
		get_cell ((i+1),(j-1)) board +
		get_cell ((i),(j-1)) board 
	|(x,0) when (x>0)&&(x<size-1) ->
	  get_cell ((i-1),(j)) board+
	    get_cell ((i-1),(j+1)) board +
	    get_cell ((i),(j+1)) board +
	    get_cell ((i+1),(j+1)) board +
	    get_cell ((i+1),j) board 
	|(x,y) when x=(size-1) ->if y=0 then
	    get_cell ((i-1),(j)) board+
	      get_cell ((i-1),(j+1)) board+
	      get_cell ((i),(j+1)) board 
	  else
	    if y=size-1 then
	      get_cell ((i),(j-1)) board +
		get_cell ((i-1),(j-1)) board +
		get_cell ((i-1),(j)) board
	    else
	      get_cell ((i),(j-1)) board +
		get_cell ((i-1),(j-1)) board +
		get_cell ((i-1),(j)) board+
		get_cell ((i-1),(j+1)) board+
		get_cell ((i),(j+1)) board
	|(x,y) when ((y>0)&&(y<size-1))&&(x=size-1) ->
	  get_cell ((i+1),j) board +
	    get_cell ((i+1),(j-1)) board+
	    get_cell ((i),(j-1)) board +
	    get_cell ((i-1),(j-1)) board +
	    get_cell ((i-1),(j)) board
	|(x,y)->
	  get_cell ((i-1),(j+1)) board +
	  get_cell ((i),(j+1)) board +
	  get_cell ((i+1),(j+1)) board +
	  get_cell ((i+1),j) board +
	  get_cell ((i+1),(j-1)) board +
	  get_cell ((i),(j-1)) board+
	  get_cell ((i-1),(j-1)) board +
	  get_cell ((i-1),(j)) board;;

  
               (*--------=La Vie=--------*)

  (*----={1:seed_life}=----*)

  let rec seed_life board size nb_cell =
    let x = Random.int size and y= Random.int size
    in
    match nb_cell with
      |0 -> board
      |nb_cell when (get_cell (x,y) board)=0 ->
        seed_life (put_cell 1 (x,y) board) size ( nb_cell-1)
      |nb_cell -> seed_life board size (nb_cell);;

 (*----={2:new_board}=----*)

  let new_board size nb =
    let n_b= init_board size size 0 in
    seed_life n_b size nb ;;

(*----={3:next_generation}=----*)

 (* let next_generation board size =
    let rec n_g x board = match board with
      |[] -> []
      |e::l -> (n_h x 1 e)::(n_g (x+1) l) and n_h x y board = match board with
	  |[] -> []
	  |e::l -> rules0 e (count_neighbours (x,y) board size) :: n_h x (y+1) l
    in
    n_g 1 board;;*)
	

  let next_generation board size =
    let rec n_g board size (x,y) = match (x,y) with
        |(x,y) when ((x=size-1) && (y=size-1)) ->
          put_cell (rules0 (get_cell (x,y) board )
                      (count_neighbours (x,y) board size)) (x,y)  board 
        |(x,y) when x<=size-1 ->
            n_g (put_cell (rules0 (get_cell (x,y) board )
                             (count_neighbours (x,y) board size)) (x,y) board)
              size (x+1,y)
        |(x,y) -> 
            n_g (put_cell (rules0 (get_cell (0,y+1) board)
                             (count_neighbours (0,y+1) board size)) (0,y+1) board)
              size (1,y+1)
    in n_g board size (0,0) ;;


 (*----={4:game}=----*)

  let cell_size = 10

  let rec game board size n =
    match n with
    |0 -> draw_board board cell_size
    |_ ->  draw_board board cell_size ;
           game (next_generation board size) size (n-1) ;;

(*----={5:new_game}=----*)

  let cell_size = 10

    
  let new_game size nb_cell n=
    open_window (size*cell_size+80) ;
    game (new_board size nb_cell) size n ;;
