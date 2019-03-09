angular.module("umbraco.resources")
    .factory("robotsTxtEditorResource", function ($http) {
        return {

            get: function () {
                return $http.get("backoffice/RobotsTxtEditor/RobotsTxtEditorApi/GetRobotsText");
            }

            //save: function (settings) {
            //    return $http.post("Analytics/SettingsApi/PostSettings", angular.toJson(settings));
            //}
        };
    });