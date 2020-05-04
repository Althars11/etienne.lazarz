(** Factoring Built-In Int Primes *)

open Builtin
open Basic_arithmetics

(** Factors product of two primes.
    @param key is public key of an RSA cryptosystem.
 *)
let break (key,_)=
  let rec break_rec key n =
    if modulo key n = 0 then (key/n ,n) else break_rec key (n - 2)
in break_rec key (key - 2)
