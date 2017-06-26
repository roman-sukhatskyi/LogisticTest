mainModule.service('AccountService', function ($http) {
    this.getAccount = function (user, token, id) {
        return $http.get('/api/user?id=' + id + '&token=' + token + '&user=' + user);
    }
	
	this.createAccount = function (account) {
		return $http.post("/api/account/create/", account);
	}
	
	this.updateAccount = function (account) {
		return $http.post("/api/account/update/", account);
	}
	
	this.removeAccount = function (user, token, id) {
		var request = {
			Id: id,
			User: user,
			Token: token
		};
		return $http.post("/api/account/delete/", request);
	}
	
	this.getAccounts = function (user, token, page, count) {
        return $http.get('/api/users?page=' + page + '&count=' + count + '&token=' + token + "&user=" + user);
	}

	this.registerAccount = function (userToRegister) {
	    return $http.post("/api/account/register/", userToRegister);
	}

	this.addAccount = function (userToAdd) {
	    return $http.post("/api/account/add/", userToAdd);
	}

	this.getRoles = function () {
	    return $http.get("/api/roles");
	}
});