$(function () {

    if ($("[data-recaptcha=True]").length > 0 && !window.RecaptchaApiLoaded) {
        var s = document.createElement("script");
        s.type = "text/javascript";
        s.src = "https://www.google.com/recaptcha/api.js?onload=recaptchaOnLoadCallback&render=invisible";
        $("head").append(s);

        window.RecaptchaApiLoaded = true;
    }
});

function recaptchaOnLoadCallback() {
    $("[data-recaptcha=True]").each(function (i, e) {
        var element = $(e);
        var form = $(e).closest("form");

        var recaptchaResponseInput = $("<input type=\"hidden\" name=\"RecaptchaToken\" />");
        form.append(recaptchaResponseInput);


        grecaptcha.render(e, {
            'sitekey': element.data('sitekey'),
            'callback': function (token) {
                recaptchaResponseInput.val(token);
                form.submit();
            }
        });
    });
}

function recaptchaRebind_GlobalOnResponseBind() {
    recaptchaOnLoadCallback();
}