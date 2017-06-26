mainModule.controller('UserController', function ($scope, $log, AccountService, SessionService, moduleConstants, NotificationService, FormHelper) {
	
    //variables
    $scope.users = [];
    $scope.isLoading = false;
    $scope.isPartLoading = false;
    $scope.currentPage = 0;
    $scope.currentCount = moduleConstants.pageRowsCount;
    //variables
	//methods
	
	$scope.listUsers = function(page, count) {
		var user = SessionService.getSessionUser();
		var token = SessionService.getSessionToken();
	    AccountService.getAccounts(user, token, page, count).success(function (response) {
	        $scope.isLoading = false;
	        $scope.isPartLoading = false;
	        if (response.Success) {
	            for (var i = 0; i < response.Result.length; i++) {
	                $scope.users.push({
	                    id: FormHelper.getFormValue(response.Result[i].Id),
	                    login: FormHelper.getFormValue(response.Result[i].UserName),
	                    fullName: FormHelper.getFormValue(response.Result[i].Account.FullName),
	                    homePhone: FormHelper.getFormValue(response.Result[i].Account.HomePhone),
	                    workPhone: FormHelper.getFormValue(response.Result[i].Account.WorkPhone),
	                    country: FormHelper.getFormValue(response.Result[i].Account.Country),
	                    region: FormHelper.getFormValue(response.Result[i].Account.Region),
	                    city: FormHelper.getFormValue(response.Result[i].Account.City),
	                    street: FormHelper.getFormValue(response.Result[i].Account.Street),
	                    createdOn: new Date(response.Result[i].CreatedOn).toLocaleString(),
	                    role: FormHelper.getFormValue(response.Result[i].Role.Name),
	                    modifiedOn: new Date(response.Result[i].ModifiedOn).toLocaleString(),
	                    status: response.Result[i].Connected,
                        connectionId: response.Result[i].ConnectionId
	                });
	            }
	        } else {
	            NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
	        }
		}).error(function (error) {
		    $scope.isLoading = false;
		    $scope.isPartLoading = false;
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
	}

	$scope.loadMore = function () {
	    $scope.isPartLoading = true;
	    $scope.currentPage++;
	    $scope.listUsers($scope.currentPage, $scope.currentCount);
	}

	$scope.initUsersList = function () {
	    $scope.isLoading = true;
	    $scope.listUsers($scope.currentPage, $scope.currentCount);
	}

	$scope.deleteUser = function (id) {
	    bootbox.confirm({
	        message: moduleConstants.deleteUserConfirmation,
	        buttons: {
	            confirm: {
	                label: 'Так',
	                className: 'btn-default btn-sm'
	            },
	            cancel: {
	                label: 'Ні',
	                className: 'btn-default btn-sm'
	            }
	        },
	        callback: function (ok) {
	            if (ok == true) {

	                $scope.isLoading = true;
	                var user = SessionService.getSessionUser();
	                var token = SessionService.getSessionToken();
	                AccountService.removeAccount(user, token, id).success(function (response) {
	                    $scope.isLoading = false;
	                    if (response.Success == false) {
	                        NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
	                    } else {
	                        for (var i = 0; i < $scope.users.length; i++)
	                            if ($scope.users[i].id === id) {
	                                $scope.users.splice(i, 1);
	                                break;
	                            }
	                    }
	                }).error(function (error) {
	                    $scope.isLoading = false;
	                    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
	                });
	            }
	        }
	    });
	}

	$scope.$on("onlineStateChanged", function (event, args) {
	    if (args && args.nick == "") {
	        return;
	    }
	    for (var i = 0; i < $scope.users.length; i++) {
	        if ($scope.users[i].login == args.nick) {
	            $scope.$apply(function () {
	                $scope.users[i].status = args.isOnline;
	            });
	            break;
	        }
	    }
	});
    //methods

	$scope.initUsersList();
});