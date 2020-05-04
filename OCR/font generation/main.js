Array.prototype.random = function () {
  return this[Math.floor((Math.random()*this.length))];
};

var text = "! # $ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? @ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z [ \\ ] ^ _ ` a b c d e f g h i j k l m n o p q r s t u v w x y z { | }";
console.log(text);

//text = "The quick brown fox jumps over the lazy dog";

var styles = [
    "font-family: 'Roboto', sans-serif;",
    "font-family: 'Slabo 27px', serif;",
    "font-family: 'Source Serif Pro', serif;",
    "font-family: 'Assistant', sans-serif;",
    //"font-family: 'Tinos', serif;",
    //"font-family: 'Josefin Slab', serif;",
    //"font-family: 'Hammersmith One', sans-serif;",
    "font-family: 'Enriqueta', serif;",
    //"font-family: 'Armata', sans-serif;",
    "font-family: 'Miriam Libre', sans-serif;",
    //"font-family: 'DM Serif Display', serif;",
    "font-family: 'Red Hat Display', sans-serif;",
    //"font-family: 'Cutive', serif;",
    "font-family: 'Corben', cursive;",
    //"font-family: 'Rakkas', cursive;",
    "font-family: 'Manrope', sans-serif;",
    //"font-family: 'Padauk', sans-serif;",
    "font-family: 'Sura', serif;",
];



$(document).ready(function() {
	var str = "";
	[...styles].forEach(function(style) {
		str += "<span style=\"" + style + "\">";
		str += text;
		str += "</span><br>";
	});
	console.log(str);
	$("#training").html(str);
})
