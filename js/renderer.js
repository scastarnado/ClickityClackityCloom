const { ipcRenderer } = require('electron');

document.getElementById('toggle-dark-mode').addEventListener('click', async () => {
  const isDarkMode = await window.darkMode.toggle()
  document.getElementById('theme-source').innerHTML = isDarkMode ? 'Dark' : 'Light'
})

document.getElementById('reset-to-system').addEventListener('click', async () => {
  await window.darkMode.system()
  document.getElementById('theme-source').innerHTML = 'System'
})

document.addEventListener('DOMContentLoaded', function () {
  const darkModeButton = document.getElementById('toggle-dark-mode');
  const body = document.body;

  darkModeButton.addEventListener('click', function () {
    body.classList.toggle('dark-mode');
  });

  const resetToSystemButton = document.getElementById('reset-to-system');
  resetToSystemButton.addEventListener('click', function () {
    body.classList.remove('dark-mode');
  });
});