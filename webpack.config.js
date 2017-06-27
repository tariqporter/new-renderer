const path = require('path');
const webpack = require('webpack');

module.exports = {
    entry: {
        app: './server/server.app.js'
    },
    output: {
        path: path.join(__dirname, 'build'),
        filename: '[name].bundle.js',
        libraryTarget: 'commonjs'
    },
    resolve: {
        alias: {
            'init': path.resolve(__dirname, './server/init.js')
        }
    },
    plugins: [
        new webpack.ProvidePlugin({
            window: ['init', 'window'],
            document: ['init', 'document'],
            angular: ['init', 'angular'],
            dom: ['init', 'dom']
        })
    ],
    target: 'node',
    devtool: 'inline-source-map'
};