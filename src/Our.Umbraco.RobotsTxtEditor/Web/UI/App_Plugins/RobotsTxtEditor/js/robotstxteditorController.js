
angular.module("umbraco")
    .controller("RobotsTxtEditorController",
        function ($http) {

            var vm = this;
            vm.text = $http.get("backoffice/RobotsTxtEditor/RobotsTxtEditorApi/GetRobotsText");
            //vm.text = "A jubilatory Emma and Lotte";
            console.log(vm.text);
        });