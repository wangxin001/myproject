function SetVisiable(id) {

    var bm1 = document.getElementById("bm01");
    var bm2 = document.getElementById("bm02");
    var bm3 = document.getElementById("bm03");
    var bm4 = document.getElementById("bm04");
    var bm5 = document.getElementById("bm05");
    var bm6 = document.getElementById("bm06");

    bm1.className = "cur2_off";
    bm2.className = "cur2_off";
    bm3.className = "cur2_off";
    bm4.className = "cur2_off";
    bm5.className = "cur2_off";
    bm6.className = "cur2_off";

    document.getElementById("bm0" + id).className = "cur2_on";

    var list1 = document.getElementById("list1");
    var list2 = document.getElementById("list2");
    var list3 = document.getElementById("list3");
    var list4 = document.getElementById("list4");
    var list5 = document.getElementById("list5");
    var list6 = document.getElementById("list6");

    list1.style.display = "none";
    list2.style.display = "none";
    list3.style.display = "none";
    list4.style.display = "none";
    list5.style.display = "none";
    list6.style.display = "none";

    document.getElementById("list" + id).style.display = "";
}


function SetTable(id) {

    var jrc1 = document.getElementById("jrc_1");
    var jrc2 = document.getElementById("jrc_2");

    if (id == 1) {
        jrc1.className = "jrc_top_1_1";
        jrc2.className = "jrc_top_2_2";
    }
    else {
        jrc1.className = "jrc_top_1_2";
        jrc2.className = "jrc_top_2_1";
    }

    var list1 = document.getElementById("tab01");
    var list2 = document.getElementById("tab02");

    list1.style.display = "none";
    list2.style.display = "none";

    document.getElementById("tab0" + id).style.display = "";

}