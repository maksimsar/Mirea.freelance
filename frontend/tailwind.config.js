/** @type {import('tailwindcss').Config} */
module.exports = {
  purge: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  content: ['./src/**/*.{vue,js,ts,jsx,tsx}', './public/index.html'],
  theme: {
    extend: {},
  },
  plugins: [],
}

