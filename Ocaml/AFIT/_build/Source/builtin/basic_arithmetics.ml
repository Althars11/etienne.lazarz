(** Basic arithmetics with built-in integers *)


open Builtin

(* Greater common divisor and smaller common multiple
   implemetations.
*)

(** Greater common (positive) divisor of two non-zero integers.
    @param a non-zero integers
    @param b non-zero integer
*)
let rec gcd a b =
  if b*a=0 then a+b
  else
    let gcd_rec a b =
      if b=0 then a
      else
        gcd b (modulo (a*(sign a)) b) in
    gcd_rec a b;;

(* Extended Euclidean algorithm. Computing Bezout Coefficients. *)

(** Extended euclidean division of two integers NOT OCAML DEFAULT.
    Given non-zero entries a b computes triple (u, v, d) such that
    a*u + b*v = d and d is gcd of a and b.
    @param a non-zero integer
    @param b non-zero integer.
*)
let bezout a b =

  (*let rec _bezout s t r old_s old_t old_r = match r with
        |0 -> (old_s, old_t, old_r)
        |r -> let quotient = (quot old_r r) in
                let (old_r, r) = (r, old_r - quotient * r) in
                let (old_s, s) = (s, old_s - quotient * s) in
                let (old_t, t) = (t, old_t - quotient * t) in
                _bezout s t r old_s old_t old_r
                in _bezout 0 1 b 1 0 a*)

  if a*b=0 then (1,1,a+b)
  else
    let rec bezout_rec a b (u1, v1) (u2, v2)=
      let (q,r)=div a b in
      if r=0 then (u2,v2,b)
      else
        bezout_rec b r (u2,v2) (u1-q*u2,v1-q*v2) in
    bezout_rec a b (1,0) (0,1);;
