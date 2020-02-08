'use strict';

const path = require('path');

module.exports = {
    devServer: {
        port: 8090,
        compress: true,
        contentBase: [
            path.join(__dirname, 'dist'),
            path.join(__dirname, 'wwwroot')
        ]
    }
}