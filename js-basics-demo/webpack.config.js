'use strict';

const path = require('path');

module.exports = {
    entry: {
        main: './src/index.js'
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, 'dist')
    },
    resolve: {
        extensions: ['.ts', '.js', '.tsx']
    },
    module: {
        rules: [
            {
                loader: 'ts-loader',
                test: /\.tsx?$/
            }
        ]
    },
    devServer: {
        port: 8090,
        compress: true,
        contentBase: [
            path.join(__dirname, 'dist'),
            path.join(__dirname, 'wwwroot')
        ]
    }
}