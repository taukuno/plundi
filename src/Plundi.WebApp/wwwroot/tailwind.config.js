module.exports = {
    content: [
        './**/*.html',
        './**/*.razor'
    ],
    safelist: [
        {
            pattern: /(bg|border|text)-(slate|gray|zinc|neutral|stone|red|orange|amber|yellow|lime|green|emerald|teal|cyan|sky|blue|indigo|violet|purple|fuchsia|pink|rose)-(50|100|200|300|400|500|600|700|800|900|950)/
        }
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}
