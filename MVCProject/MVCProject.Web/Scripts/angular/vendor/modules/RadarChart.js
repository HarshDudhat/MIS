(function () {
    angular.module("RadarChart", [])
    .directive("radar", radar)
    function radar() {
        return {
            restrict: "E",
            scope: {
                csv: "=",
                config: "="
            },
            link: radarDraw
        };
    }
    function radarDraw(scope, element) {
        // watch for changes on scope.data
        scope.$watch("csv", function () {
            var csv = scope.csv;
            var config = scope.config;
            //var data = csv2json(csv);
            var data = csv;
            RadarChart.draw(element[0], data, config);  // call the D3 RadarChart.draw function to draw the vis on changes to data or config
        });
    }
})();