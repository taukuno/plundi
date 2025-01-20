module.exports = {
    content: [
        './**/*.cs',
        './**/*.html',
        './**/*.razor'
    ],
    safelist: [
        {
            pattern: /(bg|border|divide|text)-(slate|gray|zinc|neutral|stone|red|orange|amber|yellow|lime|green|emerald|teal|cyan|sky|blue|indigo|violet|purple|fuchsia|pink|rose)-(50|100|200|300|400|500|600|700|800|900|950)/
        },
        {
            pattern: /grid-cols-(1|2|3|4|5|6|7|8|9|10|11|12|13|14|15)/
        }
    ],
    theme: {
        extend: {
            gridTemplateColumns: {
                '13': 'repeat(13, minmax(0, 1fr))',
                '14': 'repeat(14, minmax(0, 1fr))',
                '15': 'repeat(15, minmax(0, 1fr))',
            }
        }
    },
    plugins: [],
}
