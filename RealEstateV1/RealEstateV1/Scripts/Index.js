function nextImg(name,num) {
    for (var j = 1 ; j <= num ; j++) {
        if (document.getElementById(name + j).style.display == "block" && j < num - 2) {
            document.getElementById(name + j).style.display = "none";
            document.getElementById(name + (j + 3)).style.display = "block";
            break;
        }
        else if (document.getElementById(name + j).style.display == "block") {
            for (var q = 1 ; q <= num ; q++) {
                document.getElementById(name + q).style.display = "none";
            }
            document.getElementById(name + "1").style.display = "block";
            document.getElementById(name + "2").style.display = "block";
            document.getElementById(name + "3").style.display = "block";
            break;
        }
    }
}

function previousImg(name,num) {
    for (var j = 1 ; j <= num ; j++) {
        if (document.getElementById(name + j).style.display == "block" && j > 1) {
            document.getElementById(name + (j - 1)).style.display = "block";
            document.getElementById(name + (j + 2)).style.display = "none";
            break;
        }
        else if (document.getElementById(name + j).style.display == "block" && j == 1) {
            for (var q = 1 ; q <= num ; q++) {
                document.getElementById(name + q).style.display = "none";
            }
            document.getElementById(name + num).style.display = "block";
            document.getElementById(name + (num - 1)).style.display = "block";
            document.getElementById(name + (num - 2)).style.display = "block";
            break;
        }
    }
}

