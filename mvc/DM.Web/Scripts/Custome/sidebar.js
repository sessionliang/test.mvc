///<reference path="string.extension.js"/>

/**
var data = [];
var sideBar = new SideBar($('#id'));
sideBar.render(data);
*/

(function ($) {
    if (!$) {
        console.error("jQuery is not defined.");
        return;
    }

    var test_data = [
            { name: "功能菜单", href: '', icon: 'glyphicon glyphicon-home' },
            {
                name: '客户管理', href: '', icon: '', subs: [
                    { name: '客户基础信息维护', href: '/Home/About', icon: '' },
                    { name: '客户社保公积金信息维护', href: '/Home/Contact', icon: '' },
                    { name: '客户组织结构管理', href: '/Home/List', icon: '' },
                    { name: '客户合同查询', href: '', icon: '' }
                ]
            },
            {
                name: '员工管理', href: '', icon: '', subs: [
                    { name: '员工信息维护', href: '', icon: '' },
                    { name: '员工花名册导入', href: '', icon: '' },
                    { name: '员工信息查询', href: '', icon: '' }
                ]
            },
            {
                name: '增减员管理', href: '', icon: '', subs: [
                    { name: '增员', href: '', icon: '' },
                    { name: '增员记录查询', href: '', icon: '' },
                    {
                        name: '减员', href: '', icon: '', subs: [
                            { name: '减少人员', href: '', icon: '' },
                            { name: '取消人员', href: '', icon: '' }
                        ]
                    },
                    { name: '减员记录查询', href: '', icon: '' }
                ]
            }
    ];

    //展开样式
    var class_open = "open";
    //选中样式
    var class_active = "active";
    //有子菜单样式
    var class_has_sub = "has-sub";
    //组件元素
    var SideBar = function ($dom) {
        //容器
        this.$dom = $dom;
        //渲染方法
        this.render = render;
        //指定菜单选中
        this.active = active;
    };
    /**
     * 渲染方法
     * @param data {Array} JSON数组
     * @return result {Object} 当前组件的实例
     */
    function render(data) {
        if (!this.$dom || !data) {
            return;
        }
        if (!Array.isArray(data)) {
            data = [data];
        }
        var ulDom = $('<ul class="nav nav-pills nav-stacked"></ul>');
        this.$dom.append(recursionDom(ulDom, data));
        //绑定点击事件
        ulDom.unbind("click").bind("click", itemClick);

        return this;
    };
    /**
     * 递归遍历数组
     * @param $dom {Object} 容器
     * @param data {Array} JSON数组
     * @return result {Object} 容器
     */
    function recursionDom($dom, data) {
        data.forEach(function (item) {
            if (item) {
                var liDom = $('<li class = "open collapse" id = "' + (!item.href ? '' : item.href.replaceAll("/", "_")) + '"></li>');
                var aDom = $('<a href="' + (!item.href ? 'javascript:;' : item.href) + '" role="button" aria-haspopup="true" aria-expanded="false"><i class = "' + item.icon + '">&nbsp;</i>' + item.name + '</a>');
                liDom.append(aDom);
                if (item.subs && item.subs.length > 0) {
                    liDom.addClass(class_has_sub);
                    var sonUlDom = $('<ul class="nav nav-sidebar"></ul>');
                    liDom.append(sonUlDom);
                    recursionDom(sonUlDom, item.subs);
                }
                $dom.append(liDom);
            }
        });
        return $dom;
    }

    //菜单点击事件
    function itemClick(e) {
        var item = e.target;
        if (item.nodeName === "A") {
            item = item.parentElement;
        } else if (item.nodeName === "LI") {
            item = item;
        }

        if (item.nodeName === "LI") {
            //打开关闭
            openOrClose(item);
            //高亮显示
            hightShow(item);
        }
    }
    //打开关闭
    function openOrClose(li) {
        if ($(li).hasClass(class_open)) {
            $(li).children("ul").fadeOut("slow");
            $(li).removeClass(class_open);
        }
        else {
            $(li).children("ul").fadeIn("slow");
            $(li).addClass(class_open);
        }
    }
    //高亮显示
    function hightShow(li) {
        $(li).parents("ul").find(".active").removeClass(class_active);
        $(li).addClass(class_active);
    }

    /**
    * 指定Id的菜单显示
    * @param id {String} 元素ID: Home/Index 或者 Home_Index
    */
    function active(id) {
        openOrClose($("#" + id.replaceAll("/", "_")));
        hightShow($("#" + id.replaceAll("/", "_")));
        return this;
    }

    window.SideBar = SideBar;

    //测试
    new SideBar($('#sideBar')).render(test_data).active(window.location.pathname);

    //第一个菜单：打开或关闭全部菜单
    $(".nav-pills").find("li").first().bind("click", function () {
        if ($(this).hasClass(class_open)) {
            $(this).siblings().find("ul").fadeOut();
            $(this).siblings().find("ul").parent("li").removeClass(class_open);
        }
        else {
            $(this).siblings().find("ul").fadeIn();
            $(this).siblings().find("ul").parent("li").addClass(class_open);
        }
    });
})(jQuery);
