$(function () {

    var masonry = false;
    var grid = $('.grid');
    var masonrySettings = {
        itemSelector: '.grid-item',
        columnWidth: '.grid-item',
        transitionDuration: 0
    };
    masonry = function () {
        if (grid && grid.masonry) {
            grid.masonry(masonrySettings);
        }
    };


    function failFunction(element) {
        element.html("<div class=\"alert alert-danger\"><i class=\"fas fa-times-circle\"></i> Chyba na serveru <hr /> Došlo k chybě při načítání této části stránky ☹️. <a href=\"javascript: window.location.reload()\">Refreshněte</a> a zkuste to znovu.</div>");
        masonry();
    }

    function onResponse(data, status, xhr) {
        var form = this;
        var contentDiv = form.closest(".partial-view-ajax-content");

        if (data.Location) {
            window.location = data.Location;
            return;
        }
        if (contentDiv.data('on-response-refresh')) {
            window.location.reload();
            return;
        }

        contentDiv.html(data);
        masonry();
        bindFormsInElement(contentDiv);
    }

    function bindElement(element) {
        var form = element.closest("form");
        var contentDiv = form.closest(".partial-view-ajax-content");
        if (!form) {
            console.error("Form not found in button", element);
            return;
        }
        var url = form.attr('action');


        if (element.is(":button")) {
            element.click(function (e) {

                if (Ladda) {
                    var laddaButton = null;

                    var domElement = element[0];
                    laddaButton = Ladda.create(domElement);
                    laddaButton.start();
                }
                event.preventDefault();
                var data = form.serialize();

                if (element.attr('name') !== null && element.attr('value') !== null) {
                    data += (data.length > 0 ? "&" : "") + element.attr('name') + "=" + element.attr('value');
                }

                $.post(url, data)
                    .done(onResponse.bind(form))
                    .done(function () {
                        if (laddaButton) {
                            laddaButton.stop();
                        }
                    })
                    .fail(function () {
                        failFunction(element.closest(".partial-view-ajax-content"));
                    });
            });
        }
        else {
            form.unbind();
            form.on("submit", function () {

                var laddaButton = null;

                if (element.is(":button") && Ladda) {
                    var domElement = element[0];
                    laddaButton = Ladda.create(domElement);
                    laddaButton.start();
                }

                event.preventDefault();

                var data = form.serialize();

                if (element.is(":button") && element.attr('name') !== null && element.attr('value') !== null) {
                    data += (data.length > 0 ? "&" : "") + element.attr('name') + "=" + element.attr('value');
                }

                $.post(url, data)
                    .done(onResponse.bind(form))
                    .done(function () {
                        if (laddaButton) {
                            laddaButton.stop();
                        }
                    })
                    .fail(function () {
                        failFunction(element.closest(".partial-view-ajax-content"));
                    });
            });
        }

    }

    function bindFormsInElement(element) {
        element.find("[data-partial-view-form-button=true]").each(function (i, e) {
            bindElement($(e));
        });
    }

    $(".partial-view-ajax-content").each(function (i, e) {
        var element = $(e);
        var url = element.data("url");

        if (!url) {
            console.error("Undefined url in partial-view-ajax-content div");
            return;
        }

        var spinnerDiv = $("<div style=\"padding:1em;\"></div>");
        element.append(spinnerDiv);
        var spinner = new Spinner().spin(spinnerDiv[0]);

        $.get(url, function (data) {
            spinner.stop();
            element.html(data);
            masonry();
            bindFormsInElement(element);
        }).fail(function () {
            failFunction(element);
        });


    });
});