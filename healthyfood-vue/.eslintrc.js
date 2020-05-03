module.exports = {
    root: true,
    env: {
        node: true
    },
    extends: [
        'plugin:vue/essential',
        '@vue/standard',
        '@vue/typescript/recommended'
    ],
    parserOptions: {
        ecmaVersion: 2020
    },
    rules: {
        'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
        'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
        'indent': ['error', 4],
        'quotes': 'off',
        'semi': 'off',
        'space-before-function-paren': 'off',
        '@typescript-eslint/no-explicit-any': 'off',
        "@typescript-eslint/interface-name-prefix": [
            "error",
            { "prefixWithI": "always"}
        ]
    }
}
