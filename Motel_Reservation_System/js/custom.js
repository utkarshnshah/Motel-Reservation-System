(function ($) {

	new WOW().init();

	jQuery(window).load(function() { 
		jQuery("#preloader").delay(100).fadeOut("slow");
        jQuery("#load").delay(100).fadeOut("slow");
	});

	//jQuery to collapse the navbar on scroll
	$(window).scroll(function() {
		if ($(".navbar").offset().top > 50) {
			$(".navbar-fixed-top").addClass("top-nav-collapse");
		} else {
			$(".navbar-fixed-top").removeClass("top-nav-collapse");
		}
	});

	//jQuery for page scrolling feature - requires jQuery Easing plugin
	$(function() {
		$('.navbar-nav li a').bind('click', function(event) {
			var $anchor = $(this);
			$('html, body').stop().animate({
				scrollTop: $($anchor.attr('href')).offset().top
			}, 1500, 'easeInOutExpo');
			event.preventDefault();
        });

		$('.page-scroll a').bind('click', function(event) {
			var $anchor = $(this);
			$('html, body').stop().animate({
				scrollTop: $($anchor.attr('href')).offset().top
			}, 1500, 'easeInOutExpo');
			event.preventDefault();
		});
	});

    //Loading Date Picker
	$(function () {
        $("[id$=txtBookingFromDate]").datepicker({showOn: 'focus'});
	    $("[id$=txtBookingToDate]").datepicker({showOn: 'focus'});

        $("[id$=txtUpdateFromDate]").datepicker({ showOn: 'focus' });
        $("[id$=txtUpdateToDate]").datepicker({ showOn: 'focus' });

        // Getting date from text box text value and loading it in datepicker.
        var fromDate = $("[id$=txtUpdateFromDate]").val();
        // setting the format of the date.
        $("[id$=txtUpdateFromDate]").datepicker("option", "dateFormat", "dd/mm/yy").val();
        $("[id$=txtUpdateFromDate]").datepicker('setDate', fromDate);

        // Getting date from text box text value and loading it in datepicker.
        var toDate = $("[id$=txtUpdateToDate]").val();
        // setting the format of the date.
        $("[id$=txtUpdateToDate]").datepicker("option", "dateFormat", "dd/mm/yy").val();
        $("[id$=txtUpdateToDate]").datepicker('setDate', toDate);

        $("[id$=txtBookingFromDate]").datepicker("option", "dateFormat", "dd/mm/yy").val();
        $("[id$=txtBookingToDate]").datepicker("option", "dateFormat", "dd/mm/yy").val();
    });
})(jQuery);

$(document).ready(function () {
    $("[id$=chkPickUp]").change(function () {
        if (this.checked)
            $("[id$=divPickDrop]").fadeIn('slow').removeClass('hidden'),
            $("[id$=lblPickUpAddress]").fadeIn('slow').removeClass('hidden'),
            $("[id$=txtPickUpAddress]").fadeIn('slow').removeClass('hidden');
        else
            $("[id$=lblPickUpAddress]").fadeOut('slow').addClass('hidden'),
            $("[id$=txtPickUpAddress]").fadeOut('slow').addClass('hidden');
    });
    $("[id$=chkDropUp]").change(function () {
        if (this.checked)
            $("[id$=divPickDrop]").fadeIn('slow').removeClass('hidden'),
            $("[id$=lblDropUpAddress]").fadeIn('slow').removeClass('hidden'),
            $("[id$=txtDropUpAddress]").fadeIn('slow').removeClass('hidden');
        else
            $("[id$=lblDropUpAddress]").fadeOut('slow').addClass('hidden'),
            $("[id$=txtDropUpAddress]").fadeOut('slow').addClass('hidden');
    });

    $("[id$=chkUpdatePickUp]").change(function () {
        if (this.checked)
            $("[id$=divUpdatePickDrop]").fadeIn('slow').removeClass('hidden'),
                $("[id$=lblUpdatePickUpAddress]").fadeIn('slow').removeClass('hidden'),
                $("[id$=txtUpdatePickUpAddress]").fadeIn('slow').removeClass('hidden');
        else
            $("[id$=lblUpdatePickUpAddress]").fadeOut('slow').addClass('hidden'),
                $("[id$=txtUpdatePickUpAddress]").fadeOut('slow').addClass('hidden');
    });
    $("[id$=chkUpdateDropUp]").change(function () {
        if (this.checked)
            $("[id$=divUpdatePickDrop]").fadeIn('slow').removeClass('hidden'),
                $("[id$=lblUpdateDropUpAddress]").fadeIn('slow').removeClass('hidden'),
                $("[id$=txtUpdateDropUpAddress]").fadeIn('slow').removeClass('hidden');
        else
            $("[id$=lblUpdateDropUpAddress]").fadeOut('slow').addClass('hidden'),
            $("[id$=txtUpdateDropUpAddress]").fadeOut('slow').addClass('hidden');
    });
    $("[id$=btnCancelBooked]").click(function () {
        $("#confirmCancel").fadeIn('slow').removeClass('hidden'),
        $("#updateOrCancel").fadeOut('slow').addClass('hidden');
    });
    $("[id$=btnNoCancel]").click(function () {
        $("#confirmCancel").fadeOut('slow').addClass('hidden'),
        $("#updateOrCancel").fadeIn('slow').removeClass('hidden');
    });
});