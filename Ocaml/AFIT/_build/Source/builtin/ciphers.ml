(** Ciphers
    Built-in integer based ciphers.
*)

(*source pour RSA: https://fr.wikipedia.org/wiki/Chiffrement_RSA*)

open Builtin
open Basic_arithmetics
open Power
open Random
(********** Cesar Cipher **********)

(** Cesar's cipher encryption
    @param k is an integer corresponding to key
    @param m word to cipher.
    @param b base ; for ASCII codes should be set to 255.
 *)
let encrypt_cesar k m b =
  let rec encrypt_cesar k m b = match m with
    |[] -> []
    |e::reste ->(modulo (e+k) b)::(encrypt_cesar k reste b)
  in encrypt_cesar k m b

(** Cesar's cipher decryption
    @param k is an integer corresponding to key
    @param m encrypted word.
    @param b base ; for ASCII code should be set to 255.
 *)
let decrypt_cesar k m b =
  let rec  decrypt_cesar_rec k m b = match m with
    |[] -> []
    |e::l -> (modulo (e-k) b)::( decrypt_cesar_rec k l b)
  in decrypt_cesar_rec k m b

(********** RSA Cipher **********)

(** Generate an RSA ciphering keys.
    Involved prime numbers need to be distinct. Output is a couple
    of public, private keys.
    @param p prime number
    @param q prime number
*)

(*    Choisir p et q, deux nombres premiers distincts ;
    calculer leur produit n = pq, appelé module de chiffrement ;
    calculer φ(n) = (p - 1)(q - 1) (c'est la valeur de l'indicatrice d'Euler en
 n) ;    choisir un entier naturel e premier avec φ(n) et strictement inférieur
 à φ(n), appelé exposant de chiffrement ;
    calculer l'entier naturel d, inverse de e modulo φ(n), et strictement
inférieur à φ(n), appelé exposant de déchiffrement ; d peut se calculer
      efficacement par l'algorithme d'Euclide étendu.*)

let generate_keys_rsa p q =
  let n = p * q in
  let euler = (p-1)*(q-1) in
  let rec find_expo_chiff euler y = match y with
    |y when gcd y euler = 1 -> y
    |_ -> find_expo_chiff euler (Random.int euler) in
  let e = find_expo_chiff euler (Random.int euler) in
  let (d,a,b) = bezout e euler in (*bezout renvoit un triplet*)
  ((n,e) , (n,d))


(** Encryption using RSA cryptosystem.
    @param m integer hash of message
    @param pub_key a tuple (n, e) composing public key of RSA cryptosystem.
 *)
let encrypt_rsa m (n, e) = mod_power m e n
;;

(** Decryption using RSA cryptosystem.
    @param m integer hash of encrypter message.
    @param pub_key a tuple (n, d) composing private key of RSA cryptosystem.
 *)
let decrypt_rsa m (n , d) = mod_power m d n
;;

(********** ElGamal Cipher **********)

(** Generate ElGamal public data. Generates a couple (g, p)
    where p is prime and g having high enough order modulo p.
    @param p is prime having form 2*q + 1 for prime q.
 *)
let public_data_g p =
  let rec public_data_rec p =
  let g = Random.int p in
  match g with
    |x when prime_mod_power x p p <> 1  -> (g,p)
    |_ -> public_data_rec p
  in public_data_rec p

(** Generate ElGamal public data.
    @param pub_data a tuple (g, p) of public data for ElGamal cryptosystem.
*)

  (*On commence par choisir un nombre premier p.
On choisit ensuite deux entiers a
et m tels que 0≤a≤p−2 et 0≤m≤p−1.
On pose alors n≡ma[p].
La clé publique sera le triplet (p,m,n)
et la clé secrète sera l'entier a*)
let generate_keys_g (g, p) = let a= Random.int (p-2) and m=Random.int (p-1) in
                             let n = mod_power g a p in (n, a)

(** ElGamal encryption process.
    @param msg message to be encrypted.
    @param pub_data a tuple (g, p) of ElGamal public data.
    @param kA ElGamal public key.
*)

(*Soit (p,m,n) une clé publique.On commence par choisir un entier k
aléatoirement tel que 0≤k≤p−1. Un bloc de chiffres x
du message d'origine tel que x<p sera alors chiffré par
 un couple de blocs de chiffres (y1,y2)
vérifiant y1≡mk[p]ety2≡xnk[p]*)
let encrypt_g msg (g, p) kA = let k= Random.int (p-1) in
                              let y1=mod_power g k p and
                                  y2 = modulo(msg*(mod_power kA k p))p
in (y1,y2);;

(** ElGamal decryption process.
    @param msg a tuple (msgA, msgB) forming an encrypted ElGamal message.
    @param a private key
    @param pub_data a tuple (g, p) of public data for ElGamal cryptosystem.
**)


let decrypt_g (msgA, msgB) a (g, p) = let (x, b, c) =
bezout msgA p in modulo (msgB * mod_power (modulo x p) a  p) p


(* modulo( msgB* mod_power(modulo msgA p)msgA p) p*)

