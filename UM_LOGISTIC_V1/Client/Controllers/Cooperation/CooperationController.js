mainModule.controller('CooperationController', function ($rootScope, $scope, $log, $location, CooperationService, SessionService, moduleConstants, NotificationService, ApplicationPictureService, ClientTaskService, FormHelper, FilterService, ApplicationTrashService) {

    //variables 
    $scope.cooperations = [];
    $scope.currentPage = 0;
    $scope.currentCount = moduleConstants.pageRowsCount;
    $scope.isLoading = false;
    $scope.isPartLoading = false;
    $scope.filterLoading = false;
    $scope.pictures = {};
    $scope.filter = {
        TransportLength: {
            value: null,
            isClear: true
        },
        TransportWidth: {
            value: null,
            isClear: true
        },
        TransportHeight: {
            value: null,
            isClear: true
        },
        TransportWeight: {
            value: null,
            isClear: true
        },
        TransportCapacity: {
            value: null,
            isClear: true
        },
        TransportArrow: {
            value: null,
            isClear: true
        }
    };
    //variables

    //methods

    $scope.listCooperations = function (page, count) {
        CooperationService.getCooperations(SessionService.getSessionUser(), SessionService.getSessionToken(),
            $scope.currentPage, $scope.currentCount)
		.success(function (response) {
		    $scope.isPartLoading = false;
		    $scope.isLoading = false;
		    if (response.Success) {
		        for (var i = 0; i < response.Result.length; i++) {
		            $scope.cooperations.push({
		                id: FormHelper.getFormValue(response.Result[i].Id),
		                title: FormHelper.getFormValue(response.Result[i].FullName),
		                contactPhone: FormHelper.getFormValue(response.Result[i].ContactPhone),
		                carModel: FormHelper.getFormValue(response.Result[i].CarModel),
		                workCost: FormHelper.getFormValue(response.Result[i].WorkCost),
		                residenceAddress: FormHelper.getFormValue(response.Result[i].ResidenceAddress),
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

    $scope.listFilteredCooperations = function (filter, page, count) {
        FilterService.getCooperationApplications(filter, page, count)
		.success(function (response) {
		    $scope.isLoading = false;
		    $scope.filterLoading = false;
		    if (response.Success) {
		        if (page == 0) {
		            $scope.cooperations = [];
		        }
		        for (var i = 0; i < response.Result.length; i++) {
		            $scope.cooperations.push({
		                id: FormHelper.getFormValue(response.Result[i].Id),
		                title: FormHelper.getFormValue(response.Result[i].FullName),
		                contactPhone: FormHelper.getFormValue(response.Result[i].ContactPhone),
		                carModel: FormHelper.getFormValue(response.Result[i].CarModel),
		                workCost: FormHelper.getFormValue(response.Result[i].WorkCost),
		                residenceAddress: FormHelper.getFormValue(response.Result[i].ResidenceAddress),
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
		    if (!error) {
		        NotificationService.error(moduleConstants.internalErrorCaption);
		        return;
		    }
		    NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
		});
    }

    $scope.loadMore = function () {
        $scope.isPartLoading = true;
        $scope.currentPage++;
        $scope.listCooperations($scope.currentPage, $scope.currentCount);
    }

    $scope.initCooperationsList = function () {
		$scope.isLoading = true;
        $scope.listCooperations($scope.currentPage, $scope.currentCount);
    }

    $scope.getPicture = function (id) {
        var type = false;
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
        request.TypeId = 3;
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
            $scope.pictures[id] = "";
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
        $scope.listFilteredCooperations(stringFilter, $scope.currentPage, $scope.currentCount);
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
        $scope.TransportLengthSlider = $("#filter-transport-length").slider({
            id: "filter-transport-length",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.TransportLengthSlider.on('slideStop', {
            value: $scope.filter.TransportLength
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.TransportWidthSlider = $("#filter-transport-width").slider({
            id: "filter-transport-width",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.TransportWidthSlider.on('slideStop', {
            value: $scope.filter.TransportWidth
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.TransportHeightSlider = $("#filter-transport-height").slider({
            id: "filter-transport-height",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.TransportHeightSlider.on('slideStop', {
            value: $scope.filter.TransportHeight
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.TransportCapacitySlider = $("#filter-transport-capacity").slider({
            id: "filter-transport-capacity",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.TransportCapacitySlider.on('slideStop', {
            value: $scope.filter.TransportCapacity
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.TransportWeightSlider = $("#filter-transport-weight").slider({
            id: "filter-transport-weight",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.TransportWeightSlider.on('slideStop', {
            value: $scope.filter.TransportWeight
        }, function (event) {
            event.data.value.isClear = false;
        });
        $scope.TransportArrowSlider = $("#filter-transport-arrow").slider({
            id: "filter-transport-arrow",
            min: moduleConstants.sliderMinRangeValue,
            max: moduleConstants.sliderMaxRangeValue,
            range: true,
            value: [moduleConstants.sliderMinRangeValue, moduleConstants.sliderMaxRangeValue]
        });
        $scope.TransportArrowSlider.on('slideStop', {
            value: $scope.filter.TransportArrow
        }, function (event) {
            event.data.value.isClear = false;
        });
    }

    $scope.getValuesFromSliders = function () {
        $scope.filter.TransportLength.value = $scope.TransportLengthSlider[0].value;
        $scope.filter.TransportWidth.value = $scope.TransportWidthSlider[0].value;
        $scope.filter.TransportHeight.value = $scope.TransportHeightSlider[0].value;
        $scope.filter.TransportCapacity.value = $scope.TransportCapacitySlider[0].value;
        $scope.filter.TransportWeight.value = $scope.TransportWeightSlider[0].value;
        $scope.filter.TransportArrow.value = $scope.TransportArrowSlider[0].value;
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
            Type: false,
            UserId: userId
        };
        ApplicationTrashService.createApplicationTrash(request).success(function (response) {
            if (response.Success == false) {
                NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
            }
            if (response.Success == true) {
                SessionService.addTrashElement(id, false, title);
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
        ApplicationTrashService.removeTrashElement(id, false)
        .success(function (response) {
            if (response.Success) {
                $scope.isLoading = false;
                SessionService.removeShopTrashElement(id, false);
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
    $scope.initCooperationsList();
    $scope.initFilterView();
    //init controller
});