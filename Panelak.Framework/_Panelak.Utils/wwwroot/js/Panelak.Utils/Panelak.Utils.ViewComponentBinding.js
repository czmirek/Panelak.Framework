$(function () {

    function globalRebind(suffix) {
        var globalKeys = Object.keys(window);
        for (var i = 0; i < globalKeys.length; i++) {
            var globalKey = globalKeys[i];
            if (globalKey.includes(suffix) && typeof window[globalKey] === "function") {
                window[globalKey]();
            }
        }
    }

    function bindElement(element) {

        if (!element.data("vc-action"))
            return;

        var action = element.data("vc-action");
        var form = $("#" + element.data("vc-form"));
        var target = element.data("vc-target-id");

        var url = form.attr('action');

        if (window[target + "_VcBind"])
            window[target + "_VcBind"]();

        function onResponse(data, status, xhr) {
            if (data.Location) {
                window.location = data.Location;
                return;
            }

            $("#" + target).replaceWith(data);
            bindElement(element);

            if (window[target + "_VcBind"])
                window[target + "_VcBind"]();

            globalRebind("GlobalOnResponseBind");
        }

        if (action === "submit") {
            form.submit(function (event) {
                event.preventDefault();
                var data = form.serialize();
                $.post(url, data).done(onResponse);
            });
        } else {
            $(e).on(action, function (event) {
                var data = form.serialize();
                $.post(url, data).done(onResponse);
            });
        }
    }
    function bindAll() {
        $("[data-vc-action]").each(function (i, e) {
            bindElement($(e));
        });
    }
    bindAll();
});