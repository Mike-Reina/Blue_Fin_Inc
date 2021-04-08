// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//The year on the footer will change automatically each year
var date = new Date;
document.getElementById("year").innerHTML = date.getFullYear();

//Order Delete
function confirmDelete(orderNo, isTrue) {
    var deleteSpan = 'deleteSpan_' + orderNo;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + orderNo;

    if (isTrue) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }

}