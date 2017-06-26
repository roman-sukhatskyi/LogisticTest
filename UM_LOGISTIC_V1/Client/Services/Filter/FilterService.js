mainModule.service('FilterService', function ($http) {
    this.getTransportationApplications = function (filter, page, count) {
        return $http.get('/api/t_applications?filter=' + filter + '&page=' + page + '&count=' + count);
    }
    this.getCooperationApplications = function (filter, page, count) {
        return $http.get('/api/c_applications?filter=' + filter + '&page=' + page + '&count=' + count);
    }

    this.getOrderedByMeApplications = function (type, userId, page, count) {
        return $http.get('/api/my_orderd_applications?type=' + type + '&userId=' + userId + '&page=' + page + '&count=' + count);
    }

    this.acceptApplication = function (type, id) {
        var request = {
            Id: id,
            Type: type
        };
        return $http.post('/api/application/accept', request);
    }

    this.declineApplication = function (type, id) {
        var request = {
            Id: id,
            Type: type
        };
        return $http.post('/api/application/decline', request);
    }

    this.getNotFilteredApplicationsCount = function () {
        return $http.get('/api/application/count');
    }

    this.upToDateApplication = function (type, id) {
        return $http.get('/api/up_to_date_application?type=' + type + '&id=' + id);
    }

    this.removeApplication = function (type, id) {
        return $http.get('/api/application/remove?type=' + type + '&id=' + id);
    }
});