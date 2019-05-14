var widget = document.getElementById("bfv1534926416872");

$(window).resize(function () {
    var width = window.innerWidth;
    if (width < 768) {
        widget.width = 10;
    }
});