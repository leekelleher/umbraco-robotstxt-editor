angular.module("umbraco")
    .controller("RobotsTxtEditorController",
    function (robotsTxtEditorResource) {

        var vm = this;

        vm.getData = getData;
        vm.saveData = saveData;
        vm.reloadData = reloadData;

        vm.loading = true;
        vm.data = null;

        function getData() {
            robotsTxtEditorResource.get().then(function (response) {
                vm.data = response.data;
                vm.loading = false;
            });
        }

        function saveData() {
            robotsTxtEditorResource.save(vm.data).then(function (response) {
                var success = response.data;
                console.log("API post returned " + success);
                if (success) {
                    vm.data.FileExists = true;
                }
            });
        }

        function reloadData() {
            vm.loading = false;
            getData();
        }

        vm.getData();

    });