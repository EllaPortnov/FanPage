//function init() {
//    // Check the current user's login status
//    FB.getLoginStatus(function (response) {
//        console.log(response);
//        if (response.status === 'connected') {
//            Session["User"] = response.userID;
//        }
//        else if (response.status === 'unknown' || response.status === 'not_authorized') {

//        }
//        //statusChangeCallback(response);
//    });
//}

//// Code snippet to load Facebook SDK into the site's pages
//window.fbAsyncInit = function () {
//    FB.init({
//        appId: '2432053446862527', // TODO: Replace with a constant variable
//        cookie: true,
//        xfbml: true,
//        version: 'v4.0' // TODO: Replace with a constant variable
//    });

//    FB.AppEvents.logPageView();
//    init()
//};

//(function (d, s, id) {
//    var js, fjs = d.getElementsByTagName(s)[0];
//    if (d.getElementById(id)) { return; }
//    js = d.createElement(s); js.id = id;
//    js.src = "//connect.facebook.net/en_US/sdk.js";
//    fjs.parentNode.insertBefore(js, fjs);
//}(document, 'script', 'facebook-jssdk'));

// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);
    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
        UpdateSession();
    } else {
        // The person is not logged into your app or we are unable to tell.
        document.getElementById('status').innerHTML = 'Please log ' +
            'into this app.';
    }
}

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '2432053446862527', // TODO: Replace with a constant variable
        cookie: true,  // enable cookies to allow the server to access 
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v4.0' // TODO: Replace with a constant variable
    });

    // Now that we've initialized the JavaScript SDK, we call 
    // FB.getLoginStatus().  This function gets the state of the
    // person visiting this page and can return one of three states to
    // the callback you provide.  They can be:
    //
    // 1. Logged into your app ('connected')
    // 2. Logged into Facebook, but not your app ('not_authorized')
    // 3. Not logged into Facebook and can't tell if they are logged into
    //    your app or not.
    //
    // These three cases are handled in the callback function.

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });

};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function UpdateSession() {

    //$.ajax({
    //    url: '/Users/FacebookLogin',
    //    method: 'POST',
    //    data: {
    //        firstName: 
    //    }
    //})
    //    .done(function (result) {
    //        if (!result.LoginSucceeded) {
    //            alert('incorrect user/password');
    //            return;
    //        }
    //        location.reload()
    //    })
    //    .fail(function () {
    //        console.log("login failed");
    //    })

    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me', { fields: 'first_name' }, function (response) {

        $.ajax({
            url: '/Users/FacebookLogin',
            method: 'POST',
            data: {
                firstName: response.first_name
            }
        })
            .done(function (result) {
                if (!result.LoginSucceeded) {
                    alert('Problem with login, try again later..');
                    return;
                }
                location.reload()
            })
            .fail(function () {
                console.log("login failed");
            })


        //console.log('Successful login for: ' + response.name);
        //document.getElementById('status').innerHTML =
        //    'Thanks for logging in, ' + response.name + '!';
        //console.log(response);
    });
}