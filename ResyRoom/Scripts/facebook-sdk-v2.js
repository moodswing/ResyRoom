function statusChangeCallback(response) {
    if (response.status === 'connected') {
        SaveFacebookLogin();
    } else {
        myViewModel.MostrarNotificaciones(true);
        var notificaciones = [ new notificacion("error", [new messageNotificacion("Ha ocurrido un error, por favor vuelva a intentarlo en un momento mas.")]) ];
        myViewModel.Notificaciones(notificaciones);
    }
}

function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '328136124005127',
        cookie: true, 
        xfbml: true, 
        version: 'v2.0'
    });
};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

// Here we run a very simple test of the Graph API after login is successful.
function SaveFacebookLogin() {
    FB.api('/me', function (response) {
        document.getElementById('status').innerHTML = 'Gracias por identificarte, ' + response.name + '!';
        
        var accion = "RegisterUser";
        var data = { Nombre: response.name, Email: response.email, IsFacebookLogin: true };

        $.ajax({
            url: accion,
            type: "POST",
            data: data,
            success: function (result) {
                window.location.href = result;
            }
        });
    });
}