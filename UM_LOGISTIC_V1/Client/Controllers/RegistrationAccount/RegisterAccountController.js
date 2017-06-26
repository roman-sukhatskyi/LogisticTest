mainModule.controller('RegisterAccountController', function ($rootScope, $scope, $log, $location, AccountService,
	SessionService, moduleConstants, NotificationService) {

    // variables
    $scope.isLoading = false;
    $scope.userToRegister = {
        Login: "",
        Password: "",
        ConfirmationPassword: "",
        FullName: "",
        WorkPhone: "",
        City: "",
        Image: ""
    };

    //

    // methods
    $scope.registerUser = function () {
        if (!$scope.registerForm.$valid) {
            return;
        }
        $scope.isLoading = true;
        AccountService.registerAccount($scope.userToRegister).success(function (response) {
            $scope.isLoading = false;
            if (response.Success == true) {
                $rootScope.$broadcast("userRegistrated", response);
                $location.path(moduleConstants.homePath);
            }
            else {
                NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
            }
        }).error(function (error) {
            $scope.isLoading = false;
            if (!error) {
                NotificationService.error(moduleConstants.internalErrorCaption);
                return;
            }
            NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
        });
    }

    $scope.fileChanged = function () {
        file = document.getElementById("user-picture").files[0];
        var reader = new FileReader();

        reader.addEventListener("load", function () {
            if (reader.result.indexOf("jpg") != -1 || reader.result.indexOf("jpeg") != -1 || reader.result.indexOf("png") != -1) {
                $scope.userToRegister.Image = reader.result;
            }
            else {
                NotificationService.warning(moduleConstants.invalidPictureFormat);
                document.getElementById("user-picture").value = "";
            }
        }, false);

        if (file && file.size <= (moduleConstants.pictureSizeLimitMb * 1000000)) {
            reader.readAsDataURL(file);
        }
        else if (file && file.size > (moduleConstants.pictureSizeLimitMb * 1000000)) {
            NotificationService.warning(moduleConstants.pictureSizeInvalid.replace("{0}", moduleConstants.pictureSizeLimitMb));
            document.getElementById("user-picture").value = "";
        }
    }


    $scope.phoneMask = function () {
        jQuery(function ($) {
            $("#contactPhone").mask("(999) 999-9999");
        });
    }
    //

    //init
    $scope.phoneMask();
    //
});