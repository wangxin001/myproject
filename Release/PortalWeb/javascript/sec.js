function secBoard(id, i) {
    var sun = document.getElementsByTagName("select");
    if (i == 1) {
        document.getElementById(id).style.display = "";
    }
    else {
        document.getElementById(id).style.display = "none";
    }
} 