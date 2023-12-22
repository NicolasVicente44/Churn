/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './**/*.cshtml',     // Include all Razor views in the project
        './wwwroot/**/*.html',  // Include HTML files in the wwwroot folder and subfolders
    ],
    theme: {
        extend: {},
    },
    plugins: [],
};
