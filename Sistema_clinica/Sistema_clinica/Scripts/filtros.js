function exibirFiltro(id) {
    var x = document.getElementById(id);
    if (x.className.indexOf("filtro_show") == -1)
        x.className += "filtro_show";
    else
        x.className = x.className.replace("filtro_show", "");
}