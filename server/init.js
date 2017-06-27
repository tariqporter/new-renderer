const jsdom = require("jsdom");
const virtualConsole = new jsdom.VirtualConsole();
const { JSDOM } = jsdom;
var customAngular = require("./custom-angular.js");

virtualConsole.sendTo(console);

var dom = (new JSDOM(
    `<!DOCTYPE html>
	            <body>
	            </body>
            </html>`, {
        //runScripts: "outside-only"
    }));

var window = dom.window;

customAngular.init(window);
customAngular.styles(window);

exports.window = window;
exports.document = window.document;
exports.angular = window.angular;
exports.dom = dom;