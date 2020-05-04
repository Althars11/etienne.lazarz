(** Encoding Strings *)

open Builtin
open Basic_arithmetics
open Power

(** Encode a string containing ASCII characters.
    @param str is a string representing message.
    @param bits number of bits on which to store a character ;
           alphanumeric ASCII is 7.
 *)
let encode str bits =
  let rec encode_rec str bits len = match len with
    |0 -> int_of_char(str.[0])
    |_ -> int_of_char(str.[len]) + ((encode_rec str bits (len-1)) *(power 2 bits))
  in encode_rec str bits ((String.length str) -1);;

(** Decode a string containing ASCII characters.
    @param msg is an integer representing an encoded message.
    @param bits number of bits on which to store a character ;
           alphanumeric ASCII is 7.
 *)
let decode msg bits =
let rec div n x i = match n with
        |n when n mod x = 0 -> i
        |n -> div (n - 1) x (i + 1)
    in
    let rec decode_rec bits msg =
    let i = div msg (power 2 bits) 0 in
    let msg_rec = (msg - i) / (power 2 bits) in
    match msg with
        |0 -> ""
        |msg -> ( decode_rec bits msg_rec) ^ (Char.escaped (char_of_int i))
    in decode_rec bits msg
