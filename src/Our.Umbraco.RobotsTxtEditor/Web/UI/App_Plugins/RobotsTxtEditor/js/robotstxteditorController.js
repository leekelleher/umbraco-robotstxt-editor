angular.module("umbraco")
    .controller("RobotsTxtEditorController",
        function ($scope, $routeParams, $timeout, robotsTxtEditorResource, notificationsService, angularHelper) {

            var vm = this;

            vm.getData = getData;
            vm.saveData = saveData;
            vm.reloadData = reloadData;

            vm.loading = true;
            vm.data = null;

            //vm.editorControls = {};

            //// insert buttons
            //vm.editorControls.insertDefaultButton = {
            //    labelKey: "Insert",
            //    addEllipsis: "true",
            //    handler: function() {
            //        // vm.openInsertOverlay();
            //    }
            //};

            //vm.editorControls.insertSubButtons = [
            //    {
            //        labelKey: "Disallow rule",
            //        addEllipsis: "true",
            //        handler: function () {
            //            // vm.openPageFieldOverlay();
            //        }
            //    },
            //    {
            //        labelKey: "User-Agent rule",
            //        addEllipsis: "true",
            //        handler: function () {
            //            // vm.openMacroOverlay()
            //        }
            //    }
            //];

            function getData() {
                robotsTxtEditorResource.get().then(function (response) {
                    vm.data = response.data;

                    if (vm.data.FileExists === false) {
                        // TODO: See if there is a nice way of doing this whilst maintaining correct format
                        var defaultValue = [];
                        defaultValue.push("# To add a comment to the file, start the line with the # character.\n");
                        defaultValue.push("# User-Agent is used to target a particular web crawler.\n");
                        defaultValue.push("# Any rules declared below it will apply to that User-Agent.\n");
                        defaultValue.push("# To hide a file or folder from the User-Agent, type the word 'Disallow' followed by a semi-colon.\n");
                        defaultValue.push("\n");
                        defaultValue.push("# Below is the default recommended robots.txt content for Umbraco v8.\n");
                        defaultValue.push("\n");
                        defaultValue.push("User-Agent: *\n");
                        defaultValue.push("\n");
                        defaultValue.push("Disallow: /bin/\n");
                        defaultValue.push("Disallow: /config/\n");
                        defaultValue.push("Disallow: /umbraco/\n");
                        defaultValue.push("Disallow: /views/\n");

                        vm.data.FileContents = defaultValue.join("");
                    }

                    if (vm.editor !== undefined) {
                        vm.editor.setValue(vm.data.FileContents);
                    }

                    vm.loading = false;
                });
            }

            function saveData() {
                robotsTxtEditorResource.save(vm.data).then(function (response) {
                    var success = response.data;
                    console.log("API post returned ", success);
                    if (success) {
                        vm.data.FileExists = true;
                        notificationsService.success("Saved", "Text saved to Robots.txt");
                    }
                });
            }

            function reloadData() {
                vm.loading = false;
                getData();
            }

            function initEditor() {
                vm.aceOption = {
                    mode: "text",
                    theme: "chrome",
                    showPrintMargin: false,
                    wrap: true,
                    advanced: {
                        fontSize: "14px",
                    },
                    onLoad: function (_editor) {
                        vm.editor = _editor;

                        // initial cursor placement
                        // Keep cursor in name field if we are create a new script
                        // else set the cursor at the bottom of the code editor
                        if (!$routeParams.create) {
                            $timeout(function () {
                                vm.editor.navigateFileEnd();
                                vm.editor.focus();
                            });
                        }

                        vm.editor.on("change", changeAceEditor);
                    }
                };

                function changeAceEditor() {
                    setFormState("dirty");
                }

                function setFormState(state) {
                    // get the current form
                    var currentForm = angularHelper.getCurrentForm($scope);

                    // set state
                    if (state === "dirty") {
                        currentForm.$setDirty();
                    } else if (state === "pristine") {
                        currentForm.$setPristine();
                    }
                }
            }

            initEditor();
            vm.getData();

        });