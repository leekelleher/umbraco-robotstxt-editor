angular.module("umbraco.resources")
    .factory("robotsTxtEditorResource", function ($http) {
        return {

            get: function () {
                return $http.get("backoffice/RobotsTxtEditor/RobotsTxtEditorApi/GetRobotsText");
            },

            save: function (vm) {
                return $http.post("backoffice/RobotsTxtEditor/RobotsTxtEditorApi/SaveRobotsText", vm);
            }
        };
    });