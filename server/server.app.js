const prerendering = require('aspnet-prerendering');
const customAngular = require("./custom-angular.js");
const app = require('../modules/app.js');

export default prerendering.createServerRenderer(function (params) {
    return new Promise(function (resolve, reject) {

        dom.reconfigure({ url: params.absoluteUrl });

        var $el2 = document.querySelectorAll('#main-entry');
        if ($el2.length > 0) {
            $el2[0].parentNode.removeChild($el2[0]);
        }
        var $el = document.createElement('div');
        $el.id = 'main-entry';
        $el.innerHTML = `${params.data.entryHtml}`;
        document.body.appendChild($el);

        angular.bootstrap($el, ['app']);

        setTimeout(function () {
            let html = document.querySelectorAll('#main-entry')[0].innerHTML;
            resolve({
                html: html,
                globals: {
                    transferData: { msg: 'Byeeeee' }
                }
            });
        }, 0);
    });
});