mainModule.service('UserService', function ($http) {
    this.getUser = function (user, token, id) {
        return $http.get('/api/user?id=' + id + '&token=' + token + '&user=' + user);
    }
	
	this.createUser = function (user) {
		return $http.post("/api/user/create/", user);
	}
	
	this.updateUser = function (user) {
		return $http.post("/api/user/update/", user);
	}
	
	this.removeUser = function (user, token, id) {
		var request = {
			Id: id,
			User: user,
			Token: token
		};
		return $http.post("/api/user/delete/", request);
	}
	
	this.getUsers = function (user, token, page, count) {
        return $http.get('/api/users?page=' + page + '&count=' + count + '&token=' + token + "&user=" + user);
    }
});