angular.module("umbraco")
    .controller("RobotsTxtEditorController",
        function ($scope, robotsTxtEditorResource) {
            var vm = this;
            robotsTxtEditorResource.get().then(function (response) {
                vm.text = response.data;
                console.log(vm.text);
            });
        });