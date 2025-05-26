/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        background: '#1e1e2f',
        cards: '#33334d',
        text_primary: '#f4f4f4',
        text_secondary: '#b0b0c3',
        text_accents: '#2e2e2e',
        divider: '#44445d',
        accent1: '#d16ba5',
        accent2: '#88d498',
        accent3: '#9d79bc',
        error: '#f15c6d',
      },
    },
    fontFamily: {
      sans: ['Inter', 'sans-serif'],
    }
  },
  plugins: [],
}