angular.module("umbraco")
    .controller("RobotsTxtEditorController",
    function (robotsTxtEditorResource, notificationsService) {

        var vm = this;

        vm.getData = getData;
        vm.saveData = saveData;
        vm.reloadData = reloadData;

        vm.loading = true;
        vm.data = null;

        function getData() {
            robotsTxtEditorResource.get().then(function (response) {
                vm.data = response.data;

                if (vm.data.FileExists === false) {
                    // TODO: See if there is a nice way of doing this whilst maintaining correct format
                    var defaultValue = [];
                    defaultValue.push("# default robots.txt content for Umbraco v8");
                    defaultValue.push("\n\n");
                    defaultValue.push("User-Agent: *\n");
                    defaultValue.push("Disallow: /bin/\n");
                    defaultValue.push("Disallow: /config/\n");
                    defaultValue.push("Disallow: /umbraco/\n");

                    vm.data.FileContents = defaultValue.join("");
                }

                vm.loading = false;
            });
        }

        function saveData() {
            robotsTxtEditorResource.save(vm.data).then(function (response) {
                var success = response.data;
                console.log("API post returned " + success);
                if (success) {
                    vm.data.FileExists = true;
                    notificationsService.success('Saved', 'Text saved to Robots.txt');
                }
            });
        }

        function reloadData() {
            vm.loading = false;
            getData();
        }

        vm.getData();

    });