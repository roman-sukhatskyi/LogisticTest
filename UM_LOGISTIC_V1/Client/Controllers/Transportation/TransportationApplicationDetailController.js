mainModule.controller('TransportationApplicationDetailController', function ($rootScope, $location, $scope, $stateParams, $log, $location, TransportationService, SessionService, moduleConstants, NotificationService, ApplicationPictureService, FormHelper, ClientTaskService, ApplicationTrashService) {

    if (!$stateParams.id) {
        $location.path(moduleConstants.notFoundPath);
        return;
    }
    //variables 
    $scope.transportationToView = {
        Id: 0,
        Name: "",
        ContactPhone: "",
        SendAddress: "",
        DeliveryAddress: "",
        CompleteDate: null,
        ShipmentType: "",
        ShipmentLength: 0,
        ShipmentWidth: 0,
        ShipmentHeight: 0,
        ShipmentCapacity: 0,
        ShipmentWeight: 0,
    };
    $scope.isLoading = false;
    $scope.pictures = [];
    $scope.logo = {};
    $scope.htmlPicuresContent = "";
    //variables

    //methods
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
		            $scope.transportationToView.Id = FormHelper.getFormValue(response.Result.Id);
		            $scope.transportationToView.Name = FormHelper.getFormValue(response.Result.Name);
		            $scope.transportationToView.ContactPhone = FormHelper.getFormValue(response.Result.ContactPhone);
		            $scope.transportationToView.SendAddress = FormHelper.getFormValue(response.Result.SendAddress);
		            $scope.transportationToView.DeliveryAddress = FormHelper.getFormValue(response.Result.DeliveryAddress);
		            $scope.transportationToView.CompleteDate = new Date(response.Result.CompleteDate).toLocaleString()
		            $scope.transportationToView.ShipmentType = FormHelper.getFormValue(response.Result.ShipmentType);
		            $scope.transportationToView.ShipmentLength = FormHelper.getFormValue(response.Result.ShipmentLength);
		            $scope.transportationToView.ShipmentWidth = FormHelper.getFormValue(response.Result.ShipmentWidth);
		            $scope.transportationToView.ShipmentHeight = FormHelper.getFormValue(response.Result.ShipmentHeight);
		            $scope.transportationToView.ShipmentCapacity = FormHelper.getFormValue(response.Result.ShipmentCapacity);
		            $scope.transportationToView.ShipmentWeight = FormHelper.getFormValue(response.Result.ShipmentWeight);
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

    $scope.getPictures = function (id) {
        var type = true;
        ApplicationPictureService.getApplicationPictures(id, type)
		.success(function (response) {
		    if (response.Success) {
		        if(response.Result != null) {
		            for(var i = 0; i < response.Result.length; i ++) {
		                $scope.pictures.push({ url: response.Result[i], number: i });
		            }
		            if ($scope.pictures.length > 0) {
		                $scope.logo = $scope.pictures[0];
		            }
		            $scope.initLightboxNative();
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

    $scope.initLightboxNative = function() 
    {
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
    $scope.acceptApplication = function (id) {
        var request = {};
        request.UserId = SessionService.getSessionUserId();
        request.ApplicationId = id;
        request.TypeId = 2;
        ClientTaskService.createApplicationTask(request)
        .success(function (response) {
            if (response.Success) {
                NotificationService.success(moduleConstants.callFeedbackAccepted);
                $scope.isLoading = false;
            }
            else {
                $scope.isLoading = false;
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

    $scope.moveIntoTrash = function (id, title) {
        var userId = SessionService.getSessionUserId();
        if (!userId) {
            NotificationService.warning(moduleConstants.sessionUserIdNotFound);
            $scope.isLoading = false;
            $scope.isPartLoading = false;
            return;
        }
        var request = {
            ApplicationId: id,
            Type: true,
            UserId: userId
        };
        ApplicationTrashService.createApplicationTrash(request).success(function (response) {
            if (response.Success == false) {
                NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
            }
            if (response.Success == true) {
                SessionService.addTrashElement(id, true, title);
                $rootScope.$broadcast("trashElementAdded", null);
                NotificationService.success(moduleConstants.applicationTrashAddedInfo);
            }
        }).error(function (error) {
            if (!error) {
                NotificationService.error(moduleConstants.internalErrorCaption);
                return;
            }
            NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
        });

    }

    $scope.isExistElementInTrash = function (id, type) {
        return SessionService.isExistsTrashElement(id, type);
    }

    $scope.removeFromTrash = function (id) {
        var userId = SessionService.getSessionUserId();
        if (!userId) {
            NotificationService.warning(moduleConstants.sessionUserIdNotFound);
            $scope.isLoading = false;
            $scope.isPartLoading = false;
            return;
        }
        ApplicationTrashService.removeTrashElement(id, true)
        .success(function (response) {
            if (response.Success) {
                $scope.isLoading = false;
                SessionService.removeShopTrashElement(id, true);
                $rootScope.$broadcast("trashElementRemoved", null);
            }
            else {
                $scope.isLoading = false;
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

    $scope.getTransportation();
    $scope.getPictures($stateParams.id);
    $scope.getPicturesHtml($stateParams.id);
});