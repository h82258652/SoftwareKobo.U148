function setContent(contentHtml) {
    $("#content").html(contentHtml);
}

function removeImgLink() {
    var anchors = document.querySelectorAll(".img");
    for (var i = 0; i < anchors.length; i++) {
        var anchor = anchors[i];
        anchor.href = "javascript:void(0);";
    }
}