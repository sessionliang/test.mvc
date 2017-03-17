(function () {
    //左侧菜单显示隐藏
    $("#btnSideBar").unbind("click").bind("click", function () {
        if ($(this).hasClass("show")) {
            $(this).removeClass("show");
            $(this).parent().removeClass("col-sm-offset-3").removeClass("col-md-offset-2");
            $("#main").removeClass("col-sm-offset-3").removeClass("col-md-offset-2").removeClass("col-sm-9").removeClass("col-md-10");
            $("#sideBar").hide("slow");
        }
        else {
            $(this).addClass("show");
            $(this).parent().addClass("col-sm-offset-3").addClass("col-md-offset-2");
            $("#main").addClass("col-sm-offset-3").addClass("col-md-offset-2").addClass("col-sm-9").addClass("col-md-10");
            $("#sideBar").show("slow");
        }
    });
})();