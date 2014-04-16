function tab() {

    var layer = parent.frames["CurrentStatus"].document.getElementById("layer");
    var level = parent.frames["LineChart"].document.getElementById("level");

    for (var i = 0; i <= layer.getElementsByTagName("li").length - 1; i++) {

        (function (i) {

            layer.getElementsByTagName("li").item(i).onmouseover = function () {

                for (var n = 0; n <= layer.getElementsByTagName("li").length - 1; n++) {
                    layer.getElementsByTagName("li").item(n).className = "black";
                }
                this.className = "white";
                for (var j = 0; j <= layer.getElementsByTagName("li").length - 1; j++) {
                    parent.frames["CurrentStatus"].document.getElementById("layer" + j).style.display = "none";
                }
                parent.frames["CurrentStatus"].document.getElementById("layer" + i).style.display = "block";

                if (i == 0) {

                    for (var m = 0; m <= level.getElementsByTagName("li").length - 1; m++) {
                        parent.frames["LineChart"].document.getElementById("level" + m).style.display = "none";
                        parent.frames["LineChart"].document.getElementById("tab" + m).className = "black";
                    }
                    parent.frames["LineChart"].document.getElementById("level0").style.display = "block";
                    parent.frames["LineChart"].document.getElementById("tab0").className = "white";

                } else if (i == 1) {

                    for (var m = 0; m <= level.getElementsByTagName("li").length - 1; m++) {
                        parent.frames["LineChart"].document.getElementById("level" + m).style.display = "none";
                        parent.frames["LineChart"].document.getElementById("tab" + m).className = "black";
                    }
                    parent.frames["LineChart"].document.getElementById("level1").style.display = "block";
                    parent.frames["LineChart"].document.getElementById("tab1").className = "white";
                } else {

                    for (var m = 0; m <= level.getElementsByTagName("li").length - 1; m++) {
                        parent.frames["LineChart"].document.getElementById("level" + m).style.display = "none";
                        parent.frames["LineChart"].document.getElementById("tab" + m).className = "black";
                    }

                    parent.frames["LineChart"].document.getElementById("level4").style.display = "block";
                    parent.frames["LineChart"].document.getElementById("tab4").className = "white";
                }
            }
        })(i)
    }

    for (var i = 0; i <= level.getElementsByTagName("li").length - 1; i++) {

        (function (i) {

            level.getElementsByTagName("li").item(i).onmouseover = function () {
                for (var n = 0; n <= level.getElementsByTagName("li").length - 1; n++) {
                    level.getElementsByTagName("li").item(n).className = "black";
                }
                this.className = "white";
                for (var j = 0; j <= level.getElementsByTagName("li").length - 1; j++) {
                    parent.frames["LineChart"].document.getElementById("level" + j).style.display = "none";
                }
                parent.frames["LineChart"].document.getElementById("level" + i).style.display = "block";


                if (i == 0) {

                    for (var m = 0; m <= layer.getElementsByTagName("li").length - 1; m++) {
                        parent.frames["CurrentStatus"].document.getElementById("layer" + m).style.display = "none";
                        parent.frames["CurrentStatus"].document.getElementById("price" + m).className = "black";
                    }
                    parent.frames["CurrentStatus"].document.getElementById("layer0").style.display = "block";
                    parent.frames["CurrentStatus"].document.getElementById("price0").className = "white";

                } else if (i >= 1 && i <= 3) {

                    for (var m = 0; m <= layer.getElementsByTagName("li").length - 1; m++) {
                        parent.frames["CurrentStatus"].document.getElementById("layer" + m).style.display = "none";
                        parent.frames["CurrentStatus"].document.getElementById("price" + m).className = "black";
                    }
                    parent.frames["CurrentStatus"].document.getElementById("layer1").style.display = "block";
                    parent.frames["CurrentStatus"].document.getElementById("price1").className = "white";
                } else {

                    for (var m = 0; m <= layer.getElementsByTagName("li").length - 1; m++) {
                        parent.frames["CurrentStatus"].document.getElementById("layer" + m).style.display = "none";
                        parent.frames["CurrentStatus"].document.getElementById("price" + m).className = "black";
                    }

                    parent.frames["CurrentStatus"].document.getElementById("layer2").style.display = "block";
                    parent.frames["CurrentStatus"].document.getElementById("price2").className = "white";
                }
            }

        })(i)
    }
}

tab();