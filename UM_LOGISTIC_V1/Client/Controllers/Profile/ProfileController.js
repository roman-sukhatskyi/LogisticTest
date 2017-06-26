mainModule.controller('ProfileController', function ($scope, $log, $location, UserService,
	SessionService, moduleConstants, LoginService, AccountService, NotificationService, FilterService, ClientTaskService, ApplicationTrashService, EventService) {
	
    $scope.notFilteredApplicationsCount = 0;
    $scope.clientTasksCount = 0;
    $scope.applicationsInTrash = 0;
    $scope.userImage = "";
    $scope.appTrashElements = [];
	//methods
	
	$scope.logoutUser = function() {
		SessionService.closeSession();
		$scope.changeProfileData(false, null, false);
		$scope.openLoginPage();
		EventService.stopHubConnection();
	}
	
	$scope.initProfileName = function() {
		var sessionProfileName = SessionService.getSessionProfileName();
		$scope.profileName = sessionProfileName != undefined ? sessionProfileName : moduleConstants.anonymousUserCaption;
	}

	$scope.getNotFilteredApplicationsCount = function () {
	    FilterService.getNotFilteredApplicationsCount().success(function (response) {
	        if (response.Success) {
	            $scope.notFilteredApplicationsCount = response.Result;
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

	$scope.getClientTasksCount = function () {
	    var userId = SessionService.getSessionUserId();
	    if (!userId) {
	        $scope.clientTasksCount = 0;
	        return;
	    }
	    ClientTaskService.getClientTasksCount('OwnerId==' + userId + ';').success(function (response) {
	        if (response.Success) {
	            $scope.clientTasksCount = response.Result;
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

	$scope.getApplicationsInTrashCount = function () {
	    var userId = SessionService.getSessionUserId();
	    $scope.applicationsInTrash = SessionService.getShopTrashCount();
	}
	
	$scope.initProfileLoginActions = function() {
		var sessionProfileName = SessionService.getSessionProfileName();
		$scope.isAuthorized = sessionProfileName != undefined ? true : false; 
	}

	$scope.initProfileMenu = function () {
	    var isStaff = SessionService.isStaff();
	    $scope.isStaff = isStaff;
	}
	
	$scope.openLoginPage = function() {
		$location.path(moduleConstants.loginPath);
	}
	
	$scope.openSettingsPage = function() {
		$location.path(moduleConstants.settingsPath);
	}
	
	$scope.openHomePage = function() {
		$location.path(moduleConstants.homePath);
	}
	
	$scope.changeProfileData = function(isAuthorized, profileName, isStaff) {
		$scope.profileName = isAuthorized == true ? profileName : moduleConstants.anonymousUserCaption;
		$scope.isAuthorized = isAuthorized;
		$scope.isStaff = isStaff;
	}
	
	$scope.saveProfile = function(response) {
		SessionService.saveSessionToken(response.Token, response.Result.UserName);
		SessionService.saveProfileData(response.Result);
		$scope.changeProfileData(true, response.Result.Account.FullName, SessionService.isStaff());
		if (response.Result) {
		    $scope.saveProfileImage(response.Result.Account.Image)
		}
	}
	
	$scope.getApplicationsInTrashElements = function () {
	    var userId = SessionService.getSessionUserId();
	    $scope.appTrashElements = SessionService.getShopTrashElements();
	}

	$scope.saveProfileImage = function (profileImage) {
	    $scope.userImage = profileImage;
	}

	$scope.$on("userAuthorized", function(event, args) {
	    $scope.saveProfile(args);
	    $scope.initializeManagerNotifications();
	    $scope.subscribeToOnlineStateNotifications();
	    SessionService.getShopTrash(SessionService.getSessionUserId(), function (trash) {
	        $scope.appTrashElements = trash;
	        $scope.getApplicationsInTrashCount();
	    });
	});

	$scope.$on("trashElementAdded", function (event, args) {
	    $scope.getApplicationsInTrashCount();
	});

	$scope.$on("trashElementRemoved", function (event, args) {
	    $scope.getApplicationsInTrashCount();
	});

	$scope.initializeManagerNotifications = function () {
	    var isStaff = SessionService.isStaff();
	    if (isStaff != true) {
	        return;
	    }
	    EventService.initializeEventsHub();
	    EventService.subscribeToNotifications();
	    EventService.startHubConnection();
	}

	$scope.subscribeToOnlineStateNotifications = function () {
	    var isAdmin = SessionService.isAdmin();
	    if (isAdmin == true) {
	        EventService.subscribeToOnlineStateChangedNotifications();
	    }
	}

	$scope.$on("userRegistrated", function (event, args) {
	    $scope.saveProfile(args);
	    $scope.initializeManagerNotifications();
	    $scope.subscribeToOnlineStateNotifications();
	    SessionService.getShopTrash(SessionService.getSessionUserId(), function (trash) {
	        $scope.appTrashElements = trash;
	        $scope.getApplicationsInTrashCount();
	    });
	});

	$scope.showProfileButtons = function () {
	    $("#feed-callback-button").css("display", "");
	    $("#profile-menu-button").css("display", "");
	    $("#profile-work-button").css("display", "");
	    $("#profile-shop-button").css("display", "");
	}
	
	//methods
	
	//init controller
	$scope.initProfileName();
	
	$scope.initProfileLoginActions();

	$scope.initProfileMenu();

	$scope.getNotFilteredApplicationsCount();

	$scope.getClientTasksCount();

	$scope.getApplicationsInTrashCount();

	$scope.showProfileButtons();

	$scope.initializeManagerNotifications();

	$scope.subscribeToOnlineStateNotifications();

    //$scope.getApplicationsInTrashElements();
	SessionService.getShopTrash(SessionService.getSessionUserId(), function (trash) {
	    $scope.appTrashElements = trash;
	    $scope.getApplicationsInTrashCount();
	});
	//init controller
});