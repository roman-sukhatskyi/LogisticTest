mainModule.controller('CooperationApplicationController', function ($rootScope, $scope, $log, $location, CooperationService, SessionService, moduleConstants, NotificationService, ApplicationPictureService) {

    //variables 
    $scope.cooperation = {
        FullName : "",
        ResidenceAddress : "",
        ParkingPlace : "",
        ContactPhone : "",
        IsPhysicalPerson : false,
        IsBussinessPerson : false,
        CarModel : "",
        TransportLength : 0,
        TransportWidth : 0,
        TransportHeight : 0,
        TransportWeight : 0,
        TransportCapacity : 0,
        TransportArrow : 0,
        WorkCost : 0.0,
        WorkTypeId : 1,
        DeliveryCost: 0.0,
        CreatedBy: null,
        Image: null,
        user: "",
        token : ""
    };
	
	$scope.pictureData = null;
	
	$scope.workTypes = {
	    model: {
            id: 1
	    },
		options: []
	};
    $scope.isLoading = false;
    //variables

    //methods
    $scope.createCooperation = function () {
        if (!$scope.coopForm.$valid) {
            return;
        }
        $scope.isLoading = true;
        $scope.cooperation.user = SessionService.getSessionUser();
        $scope.cooperation.token = SessionService.getSessionToken();
        $scope.cooperation.CreatedBy = SessionService.getSessionUserId();
		$scope.cooperation.WorkTypeId = $scope.workTypes.model != null ? $scope.workTypes.model.id : 1;
        CooperationService.createCooperation($scope.cooperation)
		.success(function (response) {
		    $scope.isLoading = false;
		    if (response.Success) {
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
	
	$scope.loadWorkTypes = function() {
		CooperationService.getWorkTypes()
		.success(function (response) {
			for(var i = 0; i < response.length; i++) {
				$scope.workTypes.options.push({
					id: response[i].Id,
					name: response[i].Name
				});
			}
			$scope.workTypes.model = $scope.workTypes.options[0];
		}).error(function (error) {
		    if (!error) {
		        NotificationService.error(moduleConstants.internalErrorCaption);
		        return;
		    }
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
	}
	
	$scope.loadPicture = function(applicationId, type) {
	    var data = $scope.pictureData;
	    if (data == null) {
	        return;
	    }
		var request = {
			ApplicationId: applicationId,
			Image: data,
			Type: type
		};
		ApplicationPictureService.createApplicationPicture(request)
		.success(function (response) {
		    if (response.Success) {
		    }
		    else {
		        NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
		    }
		}).error(function (error) {
		    if (!error) {
		        NotificationService.error(moduleConstants.internalErrorCaption);
		        return;
		    }
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
	}
	
	$scope.fileChanged = function () {
	    file = document.getElementById("coop-picture").files[0];
	    var reader = new FileReader();

	    reader.addEventListener("load", function () {
	        if (reader.result.indexOf("jpg") != -1 || reader.result.indexOf("jpeg") != -1 || reader.result.indexOf("png") != -1) {
	            $scope.pictureData = reader.result;
	            $scope.cooperation.Image = reader.result;
	        }
	        else {
	            NotificationService.warning(moduleConstants.invalidPictureFormat);
	            document.getElementById("coop-picture").value = "";
	        }
	    }, false);

	    if (file && file.size <= (moduleConstants.pictureSizeLimitMb * 1000000)) {
	        reader.readAsDataURL(file);
	    }
	    else if (file && file.size > (moduleConstants.pictureSizeLimitMb * 1000000)) {
	        NotificationService.warning(moduleConstants.pictureSizeInvalid.replace("{0}", moduleConstants.pictureSizeLimitMb));
	        document.getElementById("coop-picture").value = "";
	    }
	}
	
	$scope.phoneMask = function () {
	    jQuery(function ($) {
	        $("#contactPhone").mask("(999) 999-9999");
	    });
	}

	// init
	$scope.loadWorkTypes();
	$scope.phoneMask();
	// init
});