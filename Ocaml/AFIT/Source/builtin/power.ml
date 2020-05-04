(** Power function implementations for built-in integers *)

(*#mod_use "builtin.ml"
  #mod_use "basic_arithmetics.ml"*)
open Builtin
open Basic_arithmetics

(* Naive and fast exponentiation ; already implemented in-class.
 *)

(** Naive power function. Linear complexity
              @param x base
    @param n exponent
*)

let rec pow x n = match n with
  |0 -> 1
  |1 -> x
  |n -> x*(pow x (n-1));;

(** Fast integer exponentiation function. Logarithmic complexity.
    @param x base
    @param n exponent
**)
let rec power x n = match n with
  |0 -> 1
  |n when n mod 2 = 1 -> x*(power (x*x) ((n-1)/2))
  |_ -> power (x*x) (n/2);;

(* Modular expnonentiation ; modulo a given natural number smaller
   max_int we never have integer-overflows if implemented properly.
 *)

(** Fast modular exponentiation function. Logarithmic complexity.
    @param x base
    @param n exponent
    @param m modular base
*)

(*version fonctionnel mais trop lente)
let rec mod_power x n m = match n with
  |0 -> 1
  |1 -> modulo x m
  |_ -> modulo ((modulo x m)*(mod_power x (n-1) m)) m;;*)

let rec mod_power x n m =
  match n with
  |0-> 1
  |_ ->let k= modulo(mod_power x (n/2) m) m in
       modulo((modulo (k*k) m)*(if n mod 2=0 then 1 else x )) m
(* Making use of Fermat Little Theorem for very quick exponentation
   modulo prime number.
 *)

(** Fast modular exponentiation function mod prime. Logarithmic complexity.
    It makes use of the Little Fermat Theorem.
    @param x base
    @param n exponent
    @param p prime modular base
 *)
let rec prime_mod_power x n p =
  match n with
    |0 -> 1
    |1 -> modulo x p
    |n when n = p -> modulo x p
    |n -> modulo (x*(prime_mod_power x (n-1) p)) p
