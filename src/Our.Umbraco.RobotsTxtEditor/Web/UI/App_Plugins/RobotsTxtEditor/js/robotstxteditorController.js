angular.module("umbraco")
    .controller("RobotsTxtEditorController",
        function (robotsTxtEditorResource, notificationsService) {

            var vm = this;

            vm.getData = getData;
            vm.saveData = saveData;

            vm.loading = true;
            vm.data = null;
            vm.errors = [];

            function getData() {
                robotsTxtEditorResource.get().then(function (response) {
                    vm.data = response.data;

                    if (vm.data.fileExists === false) {
                        vm.data.fileContents = ""
                            + "# To add a comment to the file, start the line with the # character.\n"
                            + "# User-Agent is used to target a particular web crawler.\n"
                            + "# Any rules declared below it will apply to that User-Agent.\n"
                            + "# To hide a file or folder from the User-Agent, type the word 'Disallow' followed by a semi-colon.\n"
                            + "\n"
                            + "User-Agent: *\n"
                            + "\n"
                            + "Disallow: /bin/\n"
                            + "Disallow: /config/\n"
                            + "Disallow: /umbraco/\n"
                            + "Disallow: /views/";
                    }

                    if (vm.editor !== undefined) {
                        vm.editor.setValue(vm.data.fileContents);
                        vm.editor.navigateFileEnd();
                        vm.editor.focus();
                    }

                    vm.loading = false;
                });
            }

            function saveData() {
                robotsTxtEditorResource.save(vm.data).then(function (response) {
                    var data = response.data;
                    if (data.success === true) {
                        vm.data.fileExists = true;
                        vm.errors = [];
                        notificationsService.success("Saved", "Text saved to Robots.txt");
                    } else {
                        vm.errors = data.errorMessages;
                        notificationsService.error("Validation Error", "Robots.txt has not been updated");
                    }
                });
            }

            function initEditor() {
                vm.aceOption = {
                    mode: "text",
                    theme: "chrome",
                    showPrintMargin: false,
                    wrap: true,
                    advanced: {
                        fontSize: "14px"
                    },
                    onLoad: function (_editor) {
                        vm.editor = _editor;
                        vm.getData();
                    }
                };
            }

            initEditor();
        });
