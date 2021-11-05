const colors = require('tailwindcss/colors');
module.exports = {
  purge: {
    enabled: false,
    content: ['./**/*.html', './**/*.razor']
  },
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        'dark-purple': 'rgb(2, 0, 36)'
      }
    }
  },
  variants: {
    extend: {}
  },
  plugins: []
};
