const colors = require('tailwindcss/colors');
module.exports = {
  purge: {
    enabled: true,
    content: ['./**/*.html', './**/*.razor']
  },
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        'dark-purple': 'rgb(2, 0, 36)'
      },
      spacing: {
        125: '31.25rem',
        200: '50rem'
      }
    }
  },
  variants: {
    extend: {}
  },
  plugins: [
    require('@tailwindcss/forms')({
      strategy: 'class'
    })
  ]
};
