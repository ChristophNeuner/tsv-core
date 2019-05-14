var dropzone;
var imgBox;
var imgArray;
var btnGalErst;
var folderNameInput;
document.getElementById("btnGalErst").addEventListener("click", ajaxUpload);

function setup() {
    dropzone = select("#dropzone");
    imgBox = select("#imgBox");
    btnGalErst = select("#btnGalErst");
    folderNameInput = select("#folderNameInput");

    dropzone.dragOver(highlight);
    dropzone.dragLeave(unhighlight);
    dropzone.drop(gotFile, unhighlight);
}

function highlight() {
    dropzone.style("background-color", "#ccc");
    imgArray = new Array();
    $("#imgBox").empty();
}
function unhighlight() {
    dropzone.style("background-color", "#fff");
}
function gotFile(file) {
    if (file.type === "image") {
        imgArray.push(file);
        var p = createP(file.name + " " + file.size);
        p.parent("imgBox");
        var img = createImg(file.data);
        img.size(100, 100);
        img.parent("imgBox");
    }
}

function ajaxUpload() {
    if (imgArray.length !== 0 && folderNameInput.value !== "") {
        var data = new FormData();
        data.append("folder name", folderNameInput);
        for (var i = 0; i <= imgArray.length; i++) {
            data.append(imgArray[i].name, imgArray[i]);
        }

        $.ajax({
            type: "POST",
            url: "/UserArea/CreateGalleryAjax",
            contentType: false,
            processData: false,
            data: data,
            success: function (message) {
                alert(message);
            },
            error: function () {
                alert("There was error uploading files!");
            }
        });
    }
}