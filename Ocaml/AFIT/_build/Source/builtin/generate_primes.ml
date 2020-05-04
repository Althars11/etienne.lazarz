(** Generating primes *)

open Builtin
open Basic_arithmetics
open Printf

(* Initializing list of integers for eratosthenes's sieve. Naive
   version.
*)

(** List composed of 2 and then odd integers starting at 3.
    @param n number of elements in the list of integers.
 *)
let init_eratosthenes n =
  let rec init_erato_rec n b =
    match n with
      |0|1 -> []
      |_ when b>n -> []
      |_ -> if modulo n 2 == 0 then b::(init_erato_rec n (b+2))
        else
          b::(init_erato_rec n (b+2))
  in 2::(init_erato_rec n 3);;

(* Eratosthenes sieve. *)

(** Eratosthene sieve.
    @param n limit of list of primes, starting at 2.
*)

let eratosthenes n =
  let rec isdiv init_erath x =
    match init_erath with
      |[] -> true
      |e::l when x mod e = 0 ->false
      |e::l -> isdiv l x in
  let rec erato_rec list primes = match list with
    |[] -> []
    |a::l when isdiv primes a -> a::(erato_rec l  (a::primes))
    |a::l -> erato_rec l primes
  in (erato_rec (init_eratosthenes n) []);;

(* Write and read into file functions for lists. *)

(** Write a list into a file. Element seperator is newline.
    @param file path to write to.
 *)
let write_list li file =
  let fichier = open_out file in
  let rec ecrire li = match li with
    |[] -> close_out fichier
    |e::l -> (fun s-> printf "%s\n" s) (string_of_int e); ecrire l
  in ecrire li;;

(** Write a list of prime numbers up to limit into a txt file.
    @param n limit of prime numbers up to which to build up a list of primes.
    @param file path to write to.
*)
let write_list_primes n file =
  write_list (eratosthenes n) file

(** Read file safely ; catch End_of_file exception.
    @param in_c input channel.
 *)
let input_line_opt in_c =
  try Some (input_line in_c)
  with End_of_file -> None

(** Create a list out of reading a line per line channel.
    @param in_c input channel.
 *)
let create_list in_c =
  let fichier = open_in in_c in
  let rec lecture list =
    try
     lecture (int_of_string(input_line fichier)::list)
    with End_of_file -> close_in fichier;list
  in lecture []
(** Load list of primes into OCaml environment.
    @param file path to load from.
 *)
let read_list_primes file =
  create_list (file)

(* Auxiliary functions to extract big prime numbers for testing
   purposes.
 *)

(** Get biggest prime.
    @param l list of prime numbers.
 *)
let rec last_element l = match l with
  | [] -> failwith "Builtin.generate_primes.last_element: Your list \
                    is empty. "
  | e::[] -> e
  | h::t -> last_element t

(** Get two biggest primes.
    @param l list of prime numbers.
 *)
let rec last_two l = match l with
  | [] | [_] -> failwith "Builtin.generate_primes.last_two: List has \
                          to have at least two prime numbers."
  | e::g::[] -> (e, g)
  | h::t -> last_two t
;;

(* Generating couples of prime numbers for specific or fun
   purposes.
 *)

(** Finding couples of primes where second entry is twice the first
    plus 1.
    @param limit positive integer bounding searched for primes.
    @param isprime function testing for (pseudo)primality.
 *)
let double_primes limit isprime =
  let rec double_primes_rec limit isprime list = match limit with
  |1 -> list
  |_ -> if isprime limit = true then
      if isprime ((limit*2)+1) = true then
      double_primes_rec (limit-1) (isprime) ( ( limit , limit*2+1 )   ::list)
      else double_primes_rec (limit-1) isprime list
    else double_primes_rec (limit-1) isprime list
  in double_primes_rec limit isprime []

(** Finding twin primes.
    @param limit positive integer bounding searched for primes.
    @param isprime function testing for (pseudo)primality.
 *)
let twin_primes limit isprime =
  let rec twin_primes_rec limit isprime list = match limit with
  |2 ->(2,3)::list
  |_ -> if isprime limit = true then
      if isprime (limit+2) = true then
        twin_primes_rec (limit-1) isprime ((limit,limit+2)::list)
      else
        twin_primes_rec (limit-1) isprime list
    else
      twin_primes_rec (limit-1) isprime list
  in twin_primes_rec limit isprime []
