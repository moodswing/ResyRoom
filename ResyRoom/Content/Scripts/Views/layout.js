var LayoutConfiguration = function () {
    var init = function () {
        $(document).ready(setDocumentEvents);
    },
        setDocumentEvents = function () {
            var openerLoginModal = geOpenerLoginModal();
            
            $(document).on('click', hideLoginModal);
            $(document).on("change, blur", "form .login-form input", function () { $(this).valid(); });
            $("#LoginUsuario_Email, #LoginUsuario_Password").on("keypress", submitLoginOnEnterPress);

            openerLoginModal.on('click', showLoginModal);
        },
        showLoginModal = function () {
            var loginModal = getLoginModal();
            if (loginModal.is(":visible")) return;
            
            loginModal.show();
            loginModal.find("input").first().focus();
        },
        hideLoginModal = function (e) {
            var loginModal = getLoginModal();
            var openerLoginModal = geOpenerLoginModal();
            
            if (loginModal.length == 0 || loginModal.is(":hidden")) return;
            if (loginModal[0] != e.target && !$.contains(loginModal[0], e.target) && openerLoginModal[0] != e.target && !$.contains(openerLoginModal[0], e.target))
                loginModal.hide();
        },
        submitLoginOnEnterPress = function (e) {
            if (e.which == 13) $('#LoginForm').submit();
        },
        geOpenerLoginModal = function () { return $('.login-div'); },
        getLoginModal = function () { return $('.login-form'); };

    return {
        Init: init
    };
}
