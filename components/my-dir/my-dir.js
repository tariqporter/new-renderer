//import { angular } from '../../server/init.js';

export default angular.module('cc.my-dir', [])
    .controller('myDirController', myDirController)
    .directive('ccMyDir', myDirDirective);

function myDirDirective() {
    return {
        restrict: 'E',
        scope: {},
        bindToController: {},
        controllerAs: '$ctrl',
        controller: myDirController,
        template: `<div class="some-class" ng-bind="$ctrl.um.msg"></div>
		<div class="some-class-2" ng-bind="$ctrl.um.absUrl"></div>
        <div ng-bind=$ctrl.um.date></div>`
    };
}

function myDirController($location) {
    var self = this;
    self.um = {
        msg: 'blah blah 22',
        absUrl: $location.absUrl(),
        date: null
    };

    var d = new Date();
    self.um.date = d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear() + " " +
        d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
}