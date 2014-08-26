$(document).ready(function () {

    // configure header
    $('#header').each(function () {
        $(this).children().wrapAll('<div class="container" />');
        $('h1').wrap('<div class="span-10" />');
    });

    $('#authorization').each(function () {
        $(this).wrap('<div class="span-14 quiet right last" />');
    });

    $('#primary-navigation').each(function () {
        $(this).children().wrapAll('<div class="container" />');
    });

    $('#page-contents').each(function () {
        $(this).children().wrapAll('<div class="container" />');
    });

    $('#footer').each(function () {
        $(this).children().wrapAll('<div class="container" />');
    });

    $('#main').each(function () {
        $(this).addClass('span-20 last');
    });

    $('#search').each(function () {
        $(this).addClass('span-12 last right');
    });

});