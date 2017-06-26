mainModule.service('LoginService', function ($http) {
    this.loginUser = function (userName, userPassword) {
        var user = {
            UserName: userName,
            UserPassword: userPassword
        };
        return $http.post('/api/login/', user);
    }
});