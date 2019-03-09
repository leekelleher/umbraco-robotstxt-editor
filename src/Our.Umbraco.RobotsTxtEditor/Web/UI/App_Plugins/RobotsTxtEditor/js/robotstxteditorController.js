angular.module("umbraco")
    .controller("RobotsTxtEditorController",
    function (robotsTxtEditorResource) {

        var vm = this;

        vm.getData = getData;
        vm.saveData = saveData;

        vm.loading = true;
        vm.data = null;

        function getData() {
            robotsTxtEditorResource.get().then(function (response) {
                vm.data = response.data;
                vm.loading = false;
            });
        }

        function saveData() {
            robotsTxtEditorResource.save(vm).then(function (response) {
                var success = response.data;
                console.log("API post returned " + success);
            });
        }

        vm.getData();

    });