mainModule.service('SessionService', function ($cookieStore, ApplicationTrashService, moduleConstants, NotificationService) {
    var expiresDate = null;

    this.saveSessionToken = function (token, user) {
		var expiresDate = new Date();
		expiresDate.setDate(expiresDate.getDate() + 1);
        $cookieStore.put("token-session",token, {"expires": expiresDate});
		$cookieStore.put("user-session",user, {"expires": expiresDate});
    }
	
	this.removeSessionToken = function () {
        $cookieStore.remove("token-session");
    }
	
	this.removeSessionUser = function () {
        $cookieStore.remove("user-session");
    }
	
	this.isSessionValid = function () {
        var sessionToken = $cookieStore.get("token-session");
		return sessionToken != undefined ? true : false;
    }
	
	this.closeSession = function () {
        this.removeSessionToken();
		this.removeSessionUser();
		this.clearProfileData();
    }
	
	this.getSessionToken = function() {
		return $cookieStore.get("token-session");
	}
	
	this.getSessionUser = function() {
		return $cookieStore.get("user-session");
	}
	
	this.getSessionProfileName = function() {
		return $cookieStore.get("profile-name");
	}
	
	this.getSessionUserId = function() {
		return $cookieStore.get("user-id");
	}

	this.isStaff = function () {
	    return $cookieStore.get("profile-role") != undefined ? $cookieStore.get("profile-role") == "0" || $cookieStore.get("profile-role") == "1" : false;
	}
	
	this.isAdmin = function() {
		return $cookieStore.get("profile-role") != undefined ? $cookieStore.get("profile-role") == "0" : false;
	}
	
	this.saveProfileData = function(accountData) {
	    this.expiresDate = new Date();
	    this.expiresDate.setDate(this.expiresDate.getDate() + 1);
		$cookieStore.put("profile-name", accountData.Account.FullName, { "expires": this.expiresDate });
		$cookieStore.put("profile-role", accountData.Role.Number, { "expires": this.expiresDate });
		$cookieStore.put("user-id", accountData.Id, { "expires": this.expiresDate });
		this.saveShopTrash(this.expiresDate, accountData.Id);
	}

	this.saveShopTrash = function (expiresDate, userId) {
	    var shopTrash = [];
	    $cookieStore.put("shop-trash", shopTrash, { "expires": expiresDate });
	    ApplicationTrashService.getApplicationTrashElements(userId).success(function (response) {
	        if (response.Success) {
	            shopTrash = response.Result;
	            angular.forEach(shopTrash, function (value, key) {
	                if (value.Title == '' || value.Title == null) {
	                    value.Title = moduleConstants.emptyFormValue;
	                }
	            });
	            $cookieStore.put("shop-trash", shopTrash, { "expires": expiresDate });
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

	this.addTrashElement = function (applicationId, type, title) {
	    var shopTrash = $cookieStore.get("shop-trash");
	    if (shopTrash == undefined) {
	        shopTrash = [];
	    }
	    shopTrash.push({
	        Id: applicationId,
	        Title: title,
            Type: type
	    });
	    $cookieStore.put("shop-trash", shopTrash, { "expires": this.expiresDate });
	}

	this.isExistsTrashElement = function (id, type) {
	    var shopTrash = $cookieStore.get("shop-trash");
	    if (shopTrash == undefined) {
	        return false;
	    }
	    var result = $.grep(shopTrash, function (e) { return e.Id == id && e.Type == type; });
	    return result.length > 0;
	}

	this.getShopTrashCount = function () {
	    var shopTrash = $cookieStore.get("shop-trash");
	    if (shopTrash == undefined) {
	        return 0;
	    }
	    return shopTrash.length;
	}

	this.getShopTrashElements = function () {
	    var shopTrash = $cookieStore.get("shop-trash");
	    if (shopTrash == undefined) {
	        return [];
	    }
	    return shopTrash;
	}

	this.removeShopTrashElement = function (id, type) {
	    var shopTrash = $cookieStore.get("shop-trash");
	    if (shopTrash != undefined) {
	        for (var i = 0; i < shopTrash.length; i++) {
	            if (shopTrash[i].Id == id && shopTrash[i].Type == type) {
	                shopTrash.splice(i, 1);
	                break;
	            }
	        }
	        $cookieStore.put("shop-trash", shopTrash, { "expires": this.expiresDate });
	    }
	}

	this.getShopTrash = function (userId, cb) {
	    var shopTrash = [];
	    $cookieStore.put("shop-trash", shopTrash, { "expires": expiresDate });
	    ApplicationTrashService.getApplicationTrashElements(userId).success(function (response) {
	        if (response.Success) {
	            shopTrash = response.Result;
	            angular.forEach(shopTrash, function (value, key) {
	                if (value.Title == '' || value.Title == null) {
	                    value.Title = moduleConstants.emptyFormValue;
	                }
	            });
	            $cookieStore.put("shop-trash", shopTrash, { "expires": this.expiresDate });
	            return cb.call(this, shopTrash);
	        }
	        else {
	            NotificationService.error(response.Error != null ? JSON.stringify(response.Error) : moduleConstants.internalErrorCaption);
	            return cb.call(this, null);
	        }
	    }).error(function (error) {
	        if (!error) {
	            NotificationService.error(moduleConstants.internalErrorCaption);
	            return cb.call(this, error);
	        }
	        NotificationService.error(JSON.stringify(error && error.ExceptionMessage));
	        return cb.call(this, error);
	    });
	}
	
	this.clearProfileData = function() {
	    $cookieStore.remove("profile-name");
	    $cookieStore.remove("profile-role");
	    $cookieStore.remove("user-id");
	    $cookieStore.remove("shop-trash");
	    this.expiresDate = null;
	}
	
});