function init() {
    // Check the current user's login status
    FB.getLoginStatus(function (response) {
        console.log(response);
        if (response.status === 'connected') {
            Session["User"] = response.userID;
        }
        else if (response.status === 'unknown' || response.status === 'not_authorized') {

        }
        //statusChangeCallback(response);
    });
}

// Code snippet to load Facebook SDK into the site's pages
window.fbAsyncInit = function () {
    FB.init({
        appId: '2432053446862527', // TODO: Replace with a constant variable
        cookie: true,
        xfbml: true,
        version: 'v4.0' // TODO: Replace with a constant variable
    });

    FB.AppEvents.logPageView();
    init()
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));