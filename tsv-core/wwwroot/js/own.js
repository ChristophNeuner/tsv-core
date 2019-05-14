$(document).ready(function () {
    $(function () {
        $('.dropdown').on({
            "click": function (event) {
                if ($(event.target).closest('.dropdown-toggle').length) {
                    $(this).data('closable', true);
                } else {
                    $(this).data('closable', false);
                }
            },
            "hide.bs.dropdown": function (event) {
                hide = $(this).data('closable');
                $(this).data('closable', true);
                return hide;
            }
        });
    });

    if (feature.touch) {
        $(".dropdown-submenu").on({
            tap: function () { $(this).children("ul").toggle(); },
            mouseenter: function () { $(this).children("ul").show(); },
            mouseleave: function () { $(this).children("ul").hide(); }
        });
    }


    if (!feature.touch) {

        $(".dropdown-submenu").on({
            click: function () { $(this).children("ul").toggle(); },
            mouseenter: function () { $(this).children("ul").show(); },
            mouseleave: function () { $(this).children("ul").hide(); }
        });

        $(".dropdown").hover(
               function () {
                   $(this).addClass("open");
                   $(this).children(".dropdown-toggle").attr("aria-expanded", "true");
               },
               function () {
                   $(this).removeClass("open");
                   $(this).children(".dropdown-toggle").attr("aria-expanded", "false");
               }
           );
    }
});