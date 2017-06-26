mainModule.controller('CreateUserController', function ($rootScope, $scope, $log, $location, AccountService,
	SessionService, moduleConstants, NotificationService) {

    // variables
    $scope.isLoading = false;
    $scope.userToAdd = {
        Login: "",
        Password: "",
        ConfirmationPassword: "",
        FullName: "",
        WorkPhone: "",
        City: "",
        RoleId: 1
    };

    $scope.roles = {
        model: {
            id: 1
        },
        options: []
    };
    //

    // methods
    $scope.createUser = function () {
        if (!$scope.createForm.$valid) {
            return;
        }
        $scope.isLoading = true;
        var request = $scope.userToAdd;
        request.user = SessionService.getSessionUser();
        request.token = SessionService.getSessionToken();
        $scope.userToAdd.RoleId = $scope.roles.model != null ? $scope.roles.model.id : 1;
        AccountService.addAccount(request).success(function (response) {
            $scope.isLoading = false;
            if (response.Success == true) {
                $location.path(moduleConstants.accountsPath);
            }
            else {
                NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
            }
        }).error(function (error) {
            $scope.isLoading = false;
            NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
        });
    }

    $scope.loadRoles = function () {
        AccountService.getRoles()
		.success(function (response) {
		    for (var i = 0; i < response.length; i++) {
		        $scope.roles.options.push({
		            id: response[i].Id,
		            name: response[i].Name
		        });
		    }
		}).error(function (error) {
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
    }
    //

    //init
    $scope.loadRoles();
    //
});