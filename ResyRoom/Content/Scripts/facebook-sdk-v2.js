var constantErrorConnecting = "Ha ocurrido un error, por favor vuelva a intentarlo en un momento mas.";

function statusChangeCallback(response) {
    if (response.status === 'connected') {
        SaveFacebookLogin();
    } else {
        viewModel.MostrarNotificacion(true);
        var notificaciones = [new notificacion(2, [constantErrorConnecting])]; // 2: error
        viewModel.Notificaciones(notificaciones);
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
        perms: 'user_photos',
        version: 'v2.0'
    });
};

function getAlbumPhotos() {
    var token = FB.getAccessToken();
    console.log(token);
    FB.api('/me/albums', function (resp) {
        console.log(resp);
        var ul = document.getElementById('albums');
        for (var i = 0, l = resp.data.length; i < l; i++) {
            var
                album = resp.data[i],
                li = document.createElement('li'),
                a = document.createElement('a');
            a.innerHTML = album.name;
            a.href = album.link;
            console.log(a);
            console.log(li);
            //li.appendChild(a);
            //ul.appendChild(li);
        }
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
        if (document.getElementById('status') != null) {
            document.getElementById('status').innerHTML = 'Gracias por identificarte, ' + response.name + '!';
        }

        console.log(response);
        
        var accion = "/User/RegisterUserFacebook";
        var data = { Nombre: response.name, Email: response.email, FacebookId: response.id };

        $.ajax({
            url: accion,
            type: "POST",
            data: data,
            success: function (result) {
                //window.location.href = result;
            }
        });
    });
}