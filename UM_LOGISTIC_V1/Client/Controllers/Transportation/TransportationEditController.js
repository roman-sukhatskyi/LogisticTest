mainModule.controller('TransportationEditController', function ($rootScope, $scope, $location, $log, $stateParams, $location, TransportationService, SessionService, moduleConstants, NotificationService, ApplicationPictureService, FormHelper) {

    if (!$stateParams.id) {
        $location.path(moduleConstants.notFoundPath);
        return;
    }

    //variables 
    $scope.transportationToEdit = {
    };
    $scope.isLoading = false;
    $scope.pictureData = null;
    $scope.pictures = [];
    $scope.htmlPicuresContent = '';

    //variables

    //methods
    $scope.updateTransportation = function () {
        if (!$scope.transForm.$valid) {
            return;
        }
        $scope.isLoading = true;
        $scope.transportationToEdit.CreatedBy = SessionService.getSessionUserId();
        TransportationService.updateTransportation($scope.transportationToEdit)
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
        file = document.getElementById("trans-picture").files[0];
        var reader = new FileReader();

        reader.addEventListener("load", function () {
            if (reader.result.indexOf("jpg") != -1 || reader.result.indexOf("jpeg") != -1 || reader.result.indexOf("png") != -1) {
                $scope.pictureData = reader.result;
                $scope.loadPicture($stateParams.id, true);
                document.getElementById("trans-picture").value = "";
            }
            else {
                NotificationService.warning(moduleConstants.invalidPictureFormat);
                document.getElementById("trans-picture").value = "";
            }
        }, false);

        if (file && file.size <= (moduleConstants.pictureSizeLimitMb * 1000000)) {
            reader.readAsDataURL(file);
        }
        else if (file && file.size > (moduleConstants.pictureSizeLimitMb * 1000000)) {
            NotificationService.warning(moduleConstants.pictureSizeInvalid.replace("{0}", moduleConstants.pictureSizeLimitMb));
            document.getElementById("trans-picture").value = "";
        }
    }

    $scope.phoneMask = function () {
        jQuery(function ($) {
            $("#contactPhone").mask("(999) 999-9999");
        });
    }

    $scope.getTransportation = function () {
        $scope.isLoading = true;
        var user = SessionService.getSessionUser();
        var token = SessionService.getSessionToken();
        var id = $stateParams.id;
        TransportationService.getTransportation(user, token, id)
		.success(function (response) {
		    $scope.isLoading = false;
		    if (response.Success) {
		        if (response.Result != null) {
		            $scope.transportationToEdit.Id = response.Result.Id;
		            $scope.transportationToEdit.Name = response.Result.Name;
		            $scope.transportationToEdit.ContactPhone = response.Result.ContactPhone;
		            $scope.transportationToEdit.SendAddress = response.Result.SendAddress;
		            $scope.transportationToEdit.DeliveryAddress = response.Result.DeliveryAddress;
		            $scope.transportationToEdit.CompleteDate = new Date(response.Result.CompleteDate);
		            $scope.transportationToEdit.ShipmentType = response.Result.ShipmentType;
		            $scope.transportationToEdit.ShipmentLength = response.Result.ShipmentLength;
		            $scope.transportationToEdit.ShipmentWidth = response.Result.ShipmentWidth;
		            $scope.transportationToEdit.ShipmentHeight = response.Result.ShipmentHeight;
		            $scope.transportationToEdit.ShipmentCapacity = response.Result.ShipmentCapacity;
		            $scope.transportationToEdit.ShipmentWeight = response.Result.ShipmentWeight;
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
        var type = true;
        ApplicationPictureService.getApplicationPicturesHtml(id, type)
		.success(function (response) {
		    if (response && response.length > 0) {
		        for (var i = 0; i < response.length; i++) {
		            $scope.htmlPicuresContent += response[i];
		        }
		        $('#gallery-transportation').append($scope.htmlPicuresContent);
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
    $scope.getTransportation();
    $scope.getPicturesHtml($stateParams.id);
    //
});