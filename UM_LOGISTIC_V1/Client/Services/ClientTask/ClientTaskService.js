mainModule.service('ClientTaskService', function ($http) {
    this.createCallFeedback = function (request) {
        return $http.post('/api/tasks/call_feedback', request);
    }

    this.createApplicationTask = function (request) {
        return $http.post('/api/tasks/app_task', request);
    }

    this.getClientTasks = function (page, count, filter) {
        return $http.get('/api/tasks?filter=' + filter + '&page=' + page + '&count=' + count);
    }

    this.getClientTasksCount = function (filter) {
        return $http.get('/api/tasks/count?filter=' + filter);
    }

    this.acceptTask = function (id) {
        var request = {
            Id: id
        };
        return $http.post('/api/tasks/accept', request);
    }
});