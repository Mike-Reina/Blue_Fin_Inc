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

//Stock
//$(document).on('click', '.go', function (event) {

//    var $btn = $(this);
//    var numberIn = $btn.siblings('input.input-stock').val();
//    var name = $btn.siblings('input.input-pname').val();

//    if (numberIn < 1) {
//        alert("Number must be greater than 0!");
//    }
//    else {
//        alert("You have added " + numberIn + " stock to product: " + name);
//    }
//});

