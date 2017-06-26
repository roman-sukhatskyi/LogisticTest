var mainModule = angular.module("mainModule", ["bootstrapLightbox", "ui.router", "ngRoute", "ngCookies", "ngMessages", /*"ngMap"*//*"bootstrapLightbox"*/]);

mainModule.config(['$stateProvider', '$locationProvider', '$urlRouterProvider', '$cookiesProvider', function ($stateProvider, $locationProvider, $urlRouterProvider, $cookiesProvider) {

    $stateProvider
        .state('login', {
            url: '/login',
            templateUrl: '/views/login',
            controller: 'LoginController'
        })
        .state('cooperations', {
            url: '/cooperations',
            templateUrl: '/views/cooperation',
            controller: 'CooperationController'
        })
        .state('cooperationsDetail', {
            url: '/cooperation/:id',
            templateUrl: '/views/cooperationDetail',
            controller: 'CooperationApplicationDetailController'
        })
        .state('index', {
            url: '/',
            templateUrl: '/views/home',
            controller: 'HomeController'
        })
        .state('coop_application', {
            url: '/coop_application',
            templateUrl: '/views/coop_application',
            controller: 'CooperationApplicationController'
        })
		.state('transportations', {
            url: '/transportations',
            templateUrl: '/views/transportation',
            controller: 'TransportationController'
		})
        .state('transportationsDetail', {
            url: '/transportation/:id',
            templateUrl: '/views/transportationDetail',
            controller: 'TransportationApplicationDetailController'
        })
		.state('trans_application', {
            url: '/trans_application',
            templateUrl: '/views/trans_application',
            controller: 'TransportationApplicationController'
        })
		.state('home', {
            url: '/home',
            templateUrl: '/views/home',
            controller: 'HomeController'
		})
        .state('register', {
            url: '/register',
            templateUrl: '/views/register',
            controller: 'RegisterAccountController'
        })
        .state('not_filtered_applications', {
            url: '/not_filtered_applications',
            templateUrl: '/views/not-filtered-applications',
            controller: 'NotFilteredApplicationsController'
        })
        .state('accounts', {
            url: '/accounts',
            templateUrl: '/views/accounts',
            controller: 'UserController'
        })
        .state('accounts_c', {
            url: '/accounts_c',
            templateUrl: '/views/account-create',
            controller: 'CreateUserController'
        })
        .state('call_feedback', {
            url: '/call_feedback',
            templateUrl: '/views/call-feedback',
            controller: 'CallFeedbackController'
        })
        .state('tasks', {
            url: '/tasks',
            templateUrl: '/views/tasks',
            controller: 'TasksController'
        })
        .state('my_applications', {
            url: '/my_applications',
            templateUrl: '/views/my-applications',
            controller: 'MyApplicationsController'
        })
        .state('transportationEdit', {
            url: '/transportation/edit/:id',
            templateUrl: '/views/trans_edit_application',
            controller: 'TransportationEditController'
        })
        .state('cooperationEdit', {
            url: '/cooperation/edit/:id',
            templateUrl: '/views/coop_edit_application',
            controller: 'CooperationEditController'
        })
        .state('maps', {
            url: '/maps',
            templateUrl: '/views/transport-map',
            controller: 'TransportMapController'
        })
        .state('404', {
            url: '/404',
            templateUrl: '/views/404'
        });

    $urlRouterProvider.otherwise('/404');

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });

    var expiresDate = new Date();
    expiresDate.setDate(expiresDate.getDate() + 1);
    $cookiesProvider.defaults.expires = expiresDate;
}]);
 
mainModule.run(['$rootScope', '$state', '$location', 'SessionService', 'moduleConstants',
  function ($rootScope, $state, $location, SessionService, moduleConstants) {
	  $rootScope.$on('$locationChangeStart',
      function (event, next, current) {
      }
    );
  }]);
 
 
 
  