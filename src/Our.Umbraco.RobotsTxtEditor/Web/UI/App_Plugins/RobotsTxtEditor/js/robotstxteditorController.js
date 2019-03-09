angular.module("umbraco")
    .controller("RobotsTxtEditorController",
    function ($scope, robotsTxtEditorResource) {

        var vm = this;
        robotsTxtEditorResource.get().then(function (response) {
            vm.data = response.data;
            console.log(vm);
        });

        vm.save = function () {
            robotsTxtEditorResource.save(vm).then(function (response) {
                var success = response.data;
                console.log(success);
            });
           
        };
    });