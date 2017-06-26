mainModule.controller('NotFilteredApplicationsController', function ($rootScope, $scope, $log, $location, SessionService, moduleConstants, NotificationService, ApplicationPictureService, FormHelper, FilterService) {

    //variables 
    $scope.applications = [];
    $scope.currentPage = 0;
    $scope.currentCount = moduleConstants.pageRowsCount;
    $scope.isLoading = false;
    $scope.isPartLoading = false;
    $scope.pictures = [];
    $scope.currentApplicationType = true;
    $scope.applicationTypes = {
        model: null,
        availableOptions: [
          { id: true, name: 'Перевезення' },
          { id: false, name: 'Співробітництво' }
        ]
    };
    $scope.currentTypeCaption = "Перевезення";
    //variables

    //methods

    $scope.listApplications = function (page, count) {
        switch($scope.currentApplicationType)
        {
            case true:
                FilterService.getTransportationApplications("Filtered==false",
            $scope.currentPage, $scope.currentCount)
		.success(function (response) {
		    $scope.isLoading = false;
		    $scope.isPartLoading = false;
		    if (response.Success) {
		        for (var i = 0; i < response.Result.length; i++) {
		            $scope.applications.push({
		                id: FormHelper.getFormValue(response.Result[i].Id),
		                title: FormHelper.getFormValue(response.Result[i].Name),
		                url: "/transportation/" + response.Result[i].Id,
		            });
		            $scope.getPicture(response.Result[i].Id, $scope.currentApplicationType);
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
                break;
            case false:
                FilterService.getCooperationApplications("Filtered==false",
           $scope.currentPage, $scope.currentCount)
       .success(function (response) {
           $scope.isLoading = false;
           $scope.isPartLoading = false;
           if (response.Success) {
               for (var i = 0; i < response.Result.length; i++) {
                   $scope.applications.push({
                       id: FormHelper.getFormValue(response.Result[i].Id),
                       title: FormHelper.getFormValue(response.Result[i].Name),
                       url: "/cooperation/" + response.Result[i].Id,
                   });
                   $scope.getPicture(response.Result[i].Id, $scope.currentApplicationType);
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
    }

    $scope.loadMore = function () {
        $scope.isPartLoading = true;
        $scope.currentPage++;
        $scope.listApplications($scope.currentPage, $scope.currentCount);
    }

    $scope.initApplicationsList = function () {
        $scope.isLoading = true;
        $scope.listApplications($scope.currentPage, $scope.currentCount);
    }

    $scope.getPicture = function (id, type) {
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

    $scope.changeApplicationType = function(type, caption)
    {
        $scope.currentPage = 0;
        $scope.currentTypeCaption = caption;
        $scope.applications = [];
        $scope.currentApplicationType = type;
        $scope.listApplications($scope.currentPage, $scope.currentCount);
    }

    $scope.acceptApplication = function(id, type)
    {
        bootbox.confirm({
            message: moduleConstants.acceptTaskConfirmation,
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
                    FilterService.acceptApplication(type, id)
                    .success(function (response) {
                        if (response.Success) {
                            for (var i = 0; i < $scope.applications.length; i++)
                                if ($scope.applications[i].id === id) {
                                    $scope.applications.splice(i, 1);
                                    break;
                                }
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
            }
        });
    }

    $scope.declineApplication = function (id, type) {
        bootbox.confirm({
            message: moduleConstants.deleteApplicationConfirmation,
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
                    FilterService.declineApplication(type, id)
                    .success(function (response) {
                        if (response.Success) {
                            NotificationService.success(moduleConstants.deletingInfoSuccess);
                            for (var i = 0; i < $scope.applications.length; i++)
                                if ($scope.applications[i].id === id) {
                                    $scope.applications.splice(i, 1);
                                    break;
                                }
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
            }
        });
    }
    //methods

    //init controller
    $scope.initApplicationsList();

    //init controller
});