mainModule.service('TransportationService', function ($http) {
    this.getTransportation = function (user, token, id) {
        return $http.get('/api/transportation?id=' + id + '&token=' + token + '&user=' + user);
    }
	
	this.createTransportation = function (cooperation) {
		return $http.post("/api/transportation/create/", cooperation);
	}
	
	this.updateTransportation = function (cooperation) {
		return $http.post("/api/transportation/update/", cooperation);
	}
	
	this.removeTransportation = function (user, token, id) {
		var request = {
			Id: id,
			User: user,
			Token: token
		};
		return $http.post("/api/transportation/delete/", request);
	}
	
	this.getTransportations = function (user, token, page, count) {
        return $http.get('/api/transportations?page=' + page + '&count=' + count + '&token=' + token + "&user=" + user);
    }
});