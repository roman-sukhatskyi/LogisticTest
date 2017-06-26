app.controller('TemplateController', function ($scope, TemplateService) {
    $scope.message = TemplateService.printHelloWorld();
});