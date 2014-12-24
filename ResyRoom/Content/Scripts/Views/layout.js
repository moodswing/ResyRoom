var LayoutConfiguration = function () {
    var init = function () {
        $(document).ready(setDocumentEvents);
    },
        setDocumentEvents = function () {
            var openerLoginModal = getOpenerLoginModal();
            var loginButton = getLoginButton();
            
            $(document).on('click', hideLoginModal);
            $(document).on("change, blur", "form .login-form input", function () { $(this).valid(); });
            $("#LoginUsuario_Email, #LoginUsuario_Password").on("keypress", submitLoginOnEnterPress);
            
            loginButton.on('click', submitLoginForm);
            openerLoginModal.on('click', showLoginModal);
        },
        submitLoginForm = function() {
            var form = getLoginModal().find("form");
            var loginModal = getLoginModal();
            cleanErrorSummary();
            
            if (!form.valid()) return false;

            showLoadingLogin();
            $.post("/User/Autentificar", form.serialize(), function (data) {
                if (data.result == "Redirect") window.location = data.url;
                else {
                    loginModal.parent().html(getLoginModal($(data)));
                    setDocumentEvents();
                    hideLoadingLogin();
                }
            });

            return false;
        },
        showLoginModal = function () {
            var loginModal = getLoginModal();
            if (loginModal.is(":visible")) return;
            
            loginModal.show();
            loginModal.find("input").first().focus();
        },
        hideLoginModal = function (e) {
            var loginModal = getLoginModal();
            var openerLoginModal = getOpenerLoginModal();
            
            if (loginModal.length == 0 || loginModal.is(":hidden")) return;
            if (loginModal[0] != e.target && !$.contains(loginModal[0], e.target) && openerLoginModal[0] != e.target && !$.contains(openerLoginModal[0], e.target))
                loginModal.hide();
        },
        submitLoginOnEnterPress = function (e) {
            if (e.which == 13) $('#LoginForm').submit();
        },
        showLoadingLogin = function() {
            $("#LoadingLogin").html("<div id='loading' style='padding-bottom: 5px;'><div class='bubblingG' style='margin: auto;'><span id='bubblingG_1'>" +
                "</span><span id='bubblingG_2'></span><span id='bubblingG_3'></span></div><div style='text-align: center;'></div></div>");
            $("#LoadingLogin").show();
        },
        hideLoadingLogin = function() {
            $("#LoadingLogin").hide();
        },
        cleanErrorSummary = function() {
            var form = getLoginModal().find("form");
            form.find(".validation-summary-errors").remove();
        },
        getLoginButton = function () { return $('#LoginButton'); },
        getOpenerLoginModal = function() { return $('.login-div'); },
        getLoginModal = function(container) {
            if (container == null) return $('.login-form');
            else return container.find('.login-form');
        };

    return {
        Init: init
    };
}
