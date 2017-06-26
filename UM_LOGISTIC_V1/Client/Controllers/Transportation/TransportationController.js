mainModule.controller('TransportationController', function ($rootScope, $scope, $log, $location, TransportationService, SessionService, moduleConstants, NotificationService, ApplicationPictureService, ClientTaskService, FormHelper, FilterService, ApplicationTrashService) {

    //variables 
    $scope.transportations = [];
    $scope.currentPage = 0;
    $scope.currentCount = moduleConstants.pageRowsCount;
    $scope.isLoading = false;
    $scope.isPartLoading = false;
    $scope.filterLoading = false;
    $scope.pictures = {};

    $scope.filter = {
        ShipmentLength: {
            value : null,
            isClear : true
        },
        ShipmentWidth: {
            value: null,
            isClear: true
        },
        ShipmentHeight: {
            value: null,
            isClear: true
        },
        ShipmentCapacity: {
            value: null,
            isClear: true
        },
        ShipmentWeight: {
            value: null,
            isClear: true
        },
    };
    //variables

    //methods

    $scope.listTransportations = function (page, count) {
        TransportationService.getTransportations(SessionService.getSessionUser(), SessionService.getSessionToken(),
            $scope.currentPage, $scope.currentCount)
		.success(function (response) {
		    $scope.isLoading = false;
		    $scope.isPartLoading = false;
		    if (response.Success) {
		        for (var i = 0; i < response.Result.length; i++) {
		            $scope.transportations.push({
		                id: FormHelper.getFormValue(response.Result[i].Id),
		                title: FormHelper.getFormValue(response.Result[i].Name),
		                contactPhone: FormHelper.getFormValue(response.Result[i].ContactPhone),
		                sendAddress: FormHelper.getFormValue(response.Result[i].SendAddress),
		                deliveryAddress: FormHelper.getFormValue(response.Result[i].DeliveryAddress),
		                createdOn: new Date(response.Result[i].CreatedOn).toLocaleString()
		            });
		            $scope.getPicture(response.Result[i].Id);
		        }
		    }
		    else {
		        NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
		    }
		}).error(function (error) {
		    $scope.isLoading = false;
		    $scope.isPartLoading = false;
		    if (!error) {
		        NotificationService.error(moduleConstants.internalErrorCaption);
		        return;
		    }
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
    }

    $scope.listFilteredTransportations = function (filter, page, count) {
        FilterService.getTransportationApplications(filter, page, count)
		.success(function (response) {
		    $scope.isPartLoading = false;
		    $scope.isLoading = false;
		    $scope.filterLoading = false;
		    if (response.Success) {
		        if (page == 0) {
		            $scope.transportations = [];
		        }
		        for (var i = 0; i < response.Result.length; i++) {
		            $scope.transportations.push({
		                id: FormHelper.getFormValue(response.Result[i].Id),
		                title: FormHelper.getFormValue(response.Result[i].Name),
		                contactPhone: FormHelper.getFormValue(response.Result[i].ContactPhone),
		                sendAddress: FormHelper.getFormValue(response.Result[i].SendAddress),
		                deliveryAddress: FormHelper.getFormValue(response.Result[i].DeliveryAddress),
		                createdOn: new Date(response.Result[i].CreatedOn).toLocaleString()
		            });
		            $scope.getPicture(response.Result[i].Id);
		        }
		    }
		    else {
		        NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
		    }
		}).error(function (error) {
		    $scope.isPartLoading = false;
		    $scope.isLoading = false;
		    if (!error) {
		        NotificationService.error(moduleConstants.internalErrorCaption);
		        return;
		    }
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
    }

    $scope.loadMore = function () {
        $scope.currentPage++;
        $scope.isPartLoading = true;
        $scope.listTransportations($scope.currentPage, $scope.currentCount);
    }

    $scope.initTransportationsList = function () {
		$scope.isLoading = true;
        $scope.listTransportations($scope.currentPage, $scope.currentCount);
    }

    $scope.getPicture = function (id) {
        var type = true;
        ApplicationPictureService.getApplicationPicture(id, type)
		.success(function (response) {
		    if (response.Success) {
		        $scope.pictures[id] = response.Result;
		    }
		    else {
		        $scope.pictures[id] = "";
		    }
		}).error(function (error) {
		    if (!error) {
		        NotificationService.error(moduleConstants.internalErrorCaption);
		        $scope.pictures[id] = "";
		        return;
		    }
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		    $scope.pictures[id] = "";
		});
    }

    $scope.acceptApplication = function (id) {
        var request = {};
        var userId = SessionService.getSessionUserId();
        if (!userId) {
            NotificationService.warning(moduleConstants.sessionUserIdNotFound);
            $scope.isLoading = false;
            $scope.isPartLoading = false;
            return;
        }
        request.UserId = userId;
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

    $scope.applyFilter = function () {
        $scope.getValuesFromSliders();
        var filter = $scope.filter;
        var stringFilter = $scope.generateFilterString(filter);
        $scope.currentPage = 0;
        //$scope.isLoading = true;
        $scope.filterLoading = true;
        $scope.listFilteredTransportations(stringFilter, $scope.currentPage, $scope.currentCount);
    }

    $scope.generateFilterString = function (filter) {
        var stringFilter = "";
        var values = null;
        for (var name in filter) {
            if (filter[name] != null && filter[name].isClear == false) {
                values = filter[name].value.split(',');
                if (values.length == 2) {
                    stringFilter += name + ">=" + values[0] + ";";
                    stringFilter += name + "<=" + values[1] + ";";
                }
                if (values.length == 1) {
                    stringFilter += name + "==" + values[0] + ";";
                }
            }
        }
        stringFilter += "Filtered==true;";
        return stringFilter;
    }

    $scope.initFilterView = function () {
        $scope.ShipmentLengthSlider = $("#filter-shipment-length").slider({
            id: "filter-shipment-length",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.ShipmentLengthSlider.on('slideStop', {
            value: $scope.filter.ShipmentLength
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.ShipmentWidthSlider = $("#filter-shipment-width").slider({
            id: "filter-shipment-width",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.ShipmentWidthSlider.on('slideStop', {
            value: $scope.filter.ShipmentWidth
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.ShipmentHeightSlider = $("#filter-shipment-height").slider({
            id: "filter-shipment-height",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.ShipmentHeightSlider.on('slideStop', {
            value: $scope.filter.ShipmentHeight
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.ShipmentCapacitySlider = $("#filter-shipment-capacity").slider({
            id: "filter-shipment-capacity",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.ShipmentCapacitySlider.on('slideStop', {
            value: $scope.filter.ShipmentCapacity
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.ShipmentWeightSlider = $("#filter-shipment-weight").slider({
            id: "filter-shipment-weight",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.ShipmentWeightSlider.on('slideStop', {
            value: $scope.filter.ShipmentWeight
        }, function (event) {
            event.data.value.isClear = false;
        });
    }

    $scope.getValuesFromSliders = function () {
        $scope.filter.ShipmentLength.value = $scope.ShipmentLengthSlider[0].value;
        $scope.filter.ShipmentWidth.value = $scope.ShipmentWidthSlider[0].value;
        $scope.filter.ShipmentHeight.value = $scope.ShipmentHeightSlider[0].value;
        $scope.filter.ShipmentCapacity.value = $scope.ShipmentCapacitySlider[0].value;
        $scope.filter.ShipmentWeight.value = $scope.ShipmentWeightSlider[0].value;
    }

    $scope.dropFilter = function (filter, name) {
        if (filter[name]) {
            filter[name].isClear = true;
            $scope[name + "Slider"].slider('setValue', [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]);
        }
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

    //methods

    //init controller
    $scope.initTransportationsList();

    //init controller
    $scope.initFilterView();

});