var previous = 0;
var derniere_position_de_scroll_connue = 0;
var ticking = false;
var header = document.querySelector("header");

function faitQuelquechose(old_pos, new_pos) {
  if (new_pos - old_pos > 0)
      header.style.top = "-2em";
  else
      header.style.top = "0em";

}

window.addEventListener('scroll', function(e) {
    var old = derniere_position_de_scroll_connue;
  derniere_position_de_scroll_connue = window.scrollY;
  if (!ticking) {
    window.requestAnimationFrame(function() {
      faitQuelquechose(old, derniere_position_de_scroll_connue);
      ticking = false;
    });
  }
  ticking = true;
});

var global_id = 1000

function open_tag(tagName, id)
{
    return "<" + tagName.toLowerCase() + "><a href=\"#" + id + "\">";
}

function close_tag(tagName)
{
    return "</a></" + tagName.toLowerCase() + ">";
}

var nav = document.querySelector("nav");

if (nav !== null)
{
    var hs = document.querySelectorAll("h2, h3, h4");
    var tc = "";
    var n = 0;

    for (var i = 0; i < hs.length; i++)
    {
        var tagName = hs[i].tagName;

        if (hs[i].id == "")
            hs[i].id = "head_" + global_id++;

        var qn = "";
        if (hs[i].className == "question")
            qn = ++n + ". ";

        tc += open_tag(tagName, hs[i].id) + qn + hs[i].innerText + close_tag(tagName);
    }

    nav.innerHTML = tc;
}
