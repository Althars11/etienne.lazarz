(** Test suites for scalable encoding_msg ml file using oUnit. *)

open OUnit2
open Scalable
open Basic_arithmetics
open Power
open Encoding_msg
open Test_scalable_templates

let () = let t_list = [(("Bashar", 7), from_int 2294023860466)]
         in
         run_test template_s1i_1 "Encode Function" encode t_list
;;

let () = let t_list = [((from_int 2294023860466, 7), "Bashar")]
         in
         run_test template_2i_s "Decode Function" decode t_list
;;
