mainModule.service('ApplicationTrashService', function ($http) {
	this.createApplicationTrash = function (request) {
	    return $http.post("/api/trash/add/", request);
	}

	this.getApplicationTrashCountByCreatedBy = function (id) {
	    if (!id) {
	        id = 0;
	    }
	    return $http.get("/api/trash/count?id=" + id);
	}
	
	this.getApplicationTrashElements = function (id) {
	    if (!id) {
	        id = 0;
	    }
	    return $http.get("/api/trash/userApplications?id=" + id);
	}

	this.removeTrashElement = function (id, type) {
	    if (!id) {
	        id = 0;
	    }
	    return $http.get("/api/trash/remove?id=" + id + "&type=" + type);
	}
});