// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/// Plays corresponding audio based on whether victory or defeat message is present
document.addEventListener('DOMContentLoaded', function () {
    var audio;

    // Check if the page contains a specific message of the battle outcome
    var victoryElement = document.getElementById('victory-message');
    var defeatElement = document.getElementById('defeat-message');

    if (victoryElement) {
        audio = new Audio('/audio/pokemon-victory.mp3');
    } else if (defeatElement) {
        audio = new Audio('/audio/pokemon-defeat.mp3');
    }

    if (audio) {
        audio.play();
    }
});