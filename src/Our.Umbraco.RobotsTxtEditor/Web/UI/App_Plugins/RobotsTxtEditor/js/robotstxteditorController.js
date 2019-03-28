angular.module("umbraco")
    .controller("RobotsTxtEditorController",
        function ($scope, $routeParams, $timeout, robotsTxtEditorResource, notificationsService, angularHelper) {

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
                
                    if(vm.editor !== null) {
                        vm.editor.setValue(vm.data.FileContents);
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
                    advanced: {
                        fontSize: "14px",
                        enableSnippets: true,
                        enableBasicAutocompletion: true,
                        enableLiveAutocompletion: false
                    },
                    onLoad: function (_editor) {
                        vm.editor = _editor;
                        //Update the auto-complete method to use ctrl+alt+space
                        _editor.commands.bindKey("ctrl-alt-space", "startAutocomplete");

                        //Unassigns the keybinding (That was previously auto-complete)
                        //As conflicts with our own tree search shortcut
                        _editor.commands.bindKey("ctrl-space", null);

                        // TODO: Move all these keybinding config out into some helper/service
                        _editor.commands.addCommands([
                            //Disable (alt+shift+K)
                            //Conflicts with our own show shortcuts dialog - this overrides it
                            {
                                name: "unSelectOrFindPrevious",
                                bindKey: "Alt-Shift-K",
                                exec: function () {
                                    //Toggle the show keyboard shortcuts overlay
                                    $scope.$apply(function () {
                                        vm.showKeyboardShortcut = !vm.showKeyboardShortcut;
                                    });
                                },
                                readOnly: true
                            }
                        ]);

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
                }

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