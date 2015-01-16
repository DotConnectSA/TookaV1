function nextImg(num) {
    for (var j = 1 ; j <= num ; j++) {
        if (document.getElementById("sold" + j).style.display == "block" && j < num - 2) {
            document.getElementById("sold" + j).style.display = "none";
            document.getElementById("sold" + (j + 3)).style.display = "block";
            break;
        }
        else if (document.getElementById("sold" + j).style.display == "block") {
            for (var q = 1 ; q <= num ; q++) {
                document.getElementById("sold" + q).style.display = "none";
            }
            document.getElementById("sold1").style.display = "block";
            document.getElementById("sold2").style.display = "block";
            document.getElementById("sold3").style.display = "block";
            break;
        }
    }
}

function previousImg(num) {
    for (var j = 1 ; j <= num ; j++) {
        if (document.getElementById("sold" + j).style.display == "block" && j > 1) {
            document.getElementById("sold" + (j - 1)).style.display = "block";
            document.getElementById("sold" + (j + 2)).style.display = "none";
            break;
        }
        else if (document.getElementById("sold" + j).style.display == "block" && j == 1) {
            for (var q = 1 ; q <= num ; q++) {
                document.getElementById("sold" + q).style.display = "none";
            }
            document.getElementById("sold" + num).style.display = "block";
            document.getElementById("sold" + (num - 1)).style.display = "block";
            document.getElementById("sold" + (num - 2)).style.display = "block";
            break;
        }
    }
}

