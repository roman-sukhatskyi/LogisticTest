mainModule.controller('CooperationEditController', function ($rootScope, $scope, $location, $log, $stateParams, $location, CooperationService, SessionService, moduleConstants, NotificationService, ApplicationPictureService, FormHelper) {

    if (!$stateParams.id) {
        $location.path(moduleConstants.notFoundPath);
        return;
    }

    //variables 
    $scope.cooperationToEdit = {
    };
    $scope.isLoading = false;
    $scope.pictureData = null;
    $scope.workTypes = {
        model: null,
        options: []
    };
    $scope.htmlPicuresContent = '';

    //variables

    //methods
    $scope.updateCooperation = function () {
        if (!$scope.coopForm.$valid) {
            return;
        }
        $scope.isLoading = true;
        $scope.cooperationToEdit.CreatedBy = SessionService.getSessionUserId();
        $scope.cooperationToEdit.WorkTypeId = $scope.workTypes.model.id;
        CooperationService.updateCooperation($scope.cooperationToEdit)
		.success(function (response) {
		    $scope.isLoading = false;
		    if (response.Success) {
		        $location.path(moduleConstants.myApplicationsPath);
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

    $scope.loadPicture = function (applicationId, type) {
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
		        NotificationService.success(moduleConstants.uploadPictureSuccess);
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
                $scope.loadPicture($stateParams.id, false);
                document.getElementById("coop-picture").value = "";
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

    $scope.loadWorkTypes = function (currentId) {
        CooperationService.getWorkTypes()
		.success(function (response) {
		    for (var i = 0; i < response.length; i++) {
		        $scope.workTypes.options.push({
		            id: response[i].Id,
		            name: response[i].Name
		        });
		    }
		    for (var i = 0; i < $scope.workTypes.options.length; i++) {
		        if ($scope.workTypes.options[i].id == currentId) {
		            $scope.workTypes.model = $scope.workTypes.options[i];
		            break;
		        }
		    }
		}).error(function (error) {
		    if (!error) {
		        NotificationService.error(moduleConstants.internalErrorCaption);
		        return;
		    }
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
    }

    $scope.getCooperation = function () {
        $scope.isLoading = true;
        var user = SessionService.getSessionUser();
        var token = SessionService.getSessionToken();
        var id = $stateParams.id;
        CooperationService.getCooperation(user, token, id)
		.success(function (response) {
		    $scope.isLoading = false;
		    if (response.Success) {
		        if (response.Result != null) {
		            $scope.loadWorkTypes(response.Result.WorkType.Id);
		            $scope.cooperationToEdit.Id = response.Result.Id;
		            $scope.cooperationToEdit.FullName = response.Result.FullName;
		            $scope.cooperationToEdit.ResidenceAddress = response.Result.ResidenceAddress;
		            $scope.cooperationToEdit.ParkingPlace = response.Result.ParkingPlace;
		            $scope.cooperationToEdit.ContactPhone = response.Result.ContactPhone;
		            $scope.cooperationToEdit.IsPhysicalPerson = response.Result.IsPhysicalPerson;
		            $scope.cooperationToEdit.IsBussinessPerson = response.Result.IsBussinessPerson;
		            $scope.cooperationToEdit.CarModel = response.Result.CarModel;
		            $scope.cooperationToEdit.TransportLength = response.Result.TransportLength;
		            $scope.cooperationToEdit.TransportWidth = response.Result.TransportWidth;
		            $scope.cooperationToEdit.TransportHeight = response.Result.TransportHeight;
		            $scope.cooperationToEdit.TransportWeight = response.Result.TransportWeight;
		            $scope.cooperationToEdit.TransportCapacity = response.Result.TransportCapacity;
		            $scope.cooperationToEdit.TransportArrow = response.Result.TransportArrow;
		            $scope.cooperationToEdit.WorkCost = response.Result.WorkCost;
		            //$scope.cooperationToEdit.WorkTypeId = response.Result.WorkType.Id;
		            $scope.workTypes.model = { id: response.Result.WorkType.Id, name: response.Result.WorkType.Id, name };
		            $scope.cooperationToEdit.DeliveryCost = response.Result.DeliveryCost;
		        }
		        else {
		            $location.path(moduleConstants.notFoundPath);
		        }
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

    $scope.getPicturesHtml = function (id) {
        var type = false;
        ApplicationPictureService.getApplicationPicturesHtml(id, type)
		.success(function (response) {
		    if (response && response.length > 0) {
		        for (var i = 0; i < response.length; i++) {
		            $scope.htmlPicuresContent += response[i];
		        }
		        $('#gallery-cooperation').append($scope.htmlPicuresContent);
		        $scope.initLightboxNative();
		    }
		}).error(function (error) {
		    if (!error) {
		        NotificationService.error(moduleConstants.internalErrorCaption);
		        return;
		    }
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
    }

    $scope.initLightboxNative = function () {
        var lightbox = new Lightbox();
        lightbox.load({
            boxId: false,
            dimensions: true,
            captions: true,
            prevImg: false,
            nextImg: false,
            hideCloseBtn: false,
            closeOnClick: true,
            loadingAnimation: 200,
            animElCount: 4,
            preload: true,
            carousel: true,
            animation: 400,
            nextOnClick: true,
            responsive: true,
            maxImgSize: 0.8,
            // callbacks
            onopen: function (image) {
                $(".navbar").removeClass("navbar-fixed-top");
            },
            onclose: function (image) {
                $(".navbar").addClass("navbar-fixed-top");
            },
            onload: function (event) {
            },
            onresize: function (image) {
            },
            onloaderror: function (event) {
                if (event._happenedWhile === 'prev')
                    lightbox.prev()
                else
                    lightbox.next()
            },
            onimageclick: function (image) {
            }
        });
    }

    //init
    $scope.phoneMask();
    $scope.getCooperation();
    $scope.getPicturesHtml($stateParams.id);
    //
});