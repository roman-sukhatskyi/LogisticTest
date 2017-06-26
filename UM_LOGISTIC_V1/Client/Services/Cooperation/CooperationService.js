mainModule.service('CooperationService', function ($http) {
    this.getCooperation = function (user, token, id) {
        return $http.get('/api/cooperation?id=' + id + '&token=' + token + '&user=' + user);
    }
	
	this.createCooperation = function (cooperation) {
		return $http.post("/api/cooperation/create/", cooperation);
	}
	
	this.updateCooperation = function (cooperation) {
		return $http.post("/api/cooperation/update/", cooperation);
	}
	
	this.removeCooperation = function (user, token, id) {
		var request = {
			Id: id,
			User: user,
			Token: token
		};
		return $http.post("/api/cooperation/delete/", request);
	}
	
	this.getCooperations = function (user, token, page, count) {
        return $http.get('/api/cooperations?page=' + page + '&count=' + count + '&token=' + token + "&user=" + user);
    }
	
	this.getWorkTypes = function () {
		return $http.get("/api/appworktype");
	}
});