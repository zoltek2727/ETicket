//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#filterEvents2').autocomplete({
        source: '/Events/Search'
    });
});

$(document).ready(function () {
    $('#barCountries').get({
        source: '/Countries/Index'
    });
});

$(document).ready(function () {
    $('#cartAmount').get({
        source: '/Cart/GetCartAmount'
    });
});

$(document).ready(function () {
    //FANCYBOX
    //https://github.com/fancyapps/fancyBox
    $(".fancybox").fancybox({
        openEffect: "none",
        closeEffect: "none"
    });
});

$.ajax({
    type: "GET",
    url: "/Cart/GetCartAmount",
    contentType: "application/json",
    dataType: "json",
    success: function (response) {
        var cartAmount = $("#cartAmount");
        cartAmount.empty();
        $('<a href="/Cart/Index" method="get">').append('<span class="glyphicon glyphicon-shopping-cart" aria-hidden="true"></span> Cart <span class="badge">' + response + '</span>').appendTo(cartAmount);
    },
    failure: function (response) {
        alert(response);
    }
});

$.ajax({
    type: "GET",
    url: "/Countries/Search",
    contentType: "application/json",
    dataType: "json",
    success: function (response) {
        var countriesDDL = $("#countriesDDL");
        countriesDDL.empty();
        $.each(response, function (i, item) {
            $('<li>').append('<a href="/Events/Index?eventFilter=' + item.countryId + '"><span class="glyphicon glyphicon-hand-right"></span> ' + item.countryName + '</option>').appendTo(countriesDDL);
        });
    },
    failure: function (response) {
        alert(response);
    }
});

$.ajax({
    type: "GET",
    url: "/PerformerCategories/Search",
    contentType: "application/json",
    dataType: "json",
    success: function (response) {
        var eventsDDL = $("#eventsDDL");
        eventsDDL.empty();
        $.each(response, function (i, item) {
            $('<li>').append('<a href="/Events/Index?categoryFilter=' + item.performerCategoryId + '"><span class="glyphicon glyphicon-hand-right"></span> ' + item.performerCategoryName + '</option>').appendTo(eventsDDL);
        });
    },
    failure: function (response) {
        alert(response);
    }
});