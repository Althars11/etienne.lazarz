(** Testing for primality *)

open Builtin
open Basic_arithmetics
open Power

(** Deterministic primality test *)
let  is_prime n =
  let rec is_prime_rec f n =
    if (power f 2) > n then
      true
    else
      if(modulo n f) == 0 then
        false
      else
        is_prime_rec (f+1) n
  in is_prime_rec 2 n;;

(** Pseudo-primality test based on Fermat's Little Theorem
    @param p tested integer
    @param testSeq sequence of integers againt which to test
*)
(*http://villemin.gerard.free.fr/Wwwgvmm/Premier/pseudo.htm*)

let rec is_pseudo_prime p test_seq =
  if p=2 then true
  else
  match test_seq with
    |[]-> true
    |e::l when mod_power e (p-1) p = 1 -> is_pseudo_prime p l
    |_ -> false
