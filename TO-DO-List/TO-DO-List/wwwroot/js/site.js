// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function deleteActivity(id) {
    let url = 'https://localhost:7043/Activity/Delete';
    fetch(`${url}/${id}`, {
        method: 'POST'
    })
    .then(() => {
        window.location.reload();
    });
}