$(function() {
    "use strict";
	
$(window).on('load', function () {
		$('#preloader').delay(350).fadeOut('slow');
		$('body').delay(350).css({ 'overflow': 'visible' });
	})
	
	// Navigations
    $('nav.headnavbar').coreNavigation({
		menuPosition: "right", // left, right, center, bottom
		container: true, // true or false
		animated: true,
		animatedIn: 'flipInX',
		animatedOut: 'bounceOut',
		dropdownEvent: 'hover', // Hover, Click & Accordion
		onOpenDropdown: function(){
			console.log('open');
		},
		onCloseDropdown: function(){
			console.log('close');
		},
		onOpenMegaMenu: function(){
			console.log('Open Megamenu');
		},
		onCloseMegaMenu: function(){
			console.log('Close Megamenu');
		}
	});
	
	$(window).scroll(function() {    
		var scroll = $(window).scrollTop();

		if (scroll >= 50) {
			$(".header").addClass("header-fixed");
		} else {
			$(".header").removeClass("header-fixed");
		}
	});
	
	
	// Search Form Guest Script
	$('.select-guests-dropdown .btn-minus').on('click', function(e) {
		e.stopPropagation();
		var parent = $(this).closest('.form-select-guests');
		var input = parent.find('.select-guests-dropdown [name=' + $(this).data('input') + ']');
		var min = parseInt(input.attr('min'));
		var old = parseInt(input.val());
		if (old <= min) {
			return;
		}
		input.val(old - 1);
		$(this).next().html(old - 1);
		updateGuestCountText(parent);
	});
	$('.select-guests-dropdown .btn-add').on('click', function(e) {
		e.stopPropagation();
		var parent = $(this).closest('.form-select-guests');
		var input = parent.find('.select-guests-dropdown [name=' + $(this).data('input') + ']');
		var max = parseInt(input.attr('max'));
		var old = parseInt(input.val());
		if (old >= max) {
			return;
		}
		input.val(old + 1);
		$(this).prev().html(old + 1);
		updateGuestCountText(parent);
	});

	function updateGuestCountText(parent) {
		var adults = parseInt(parent.find('[name=adults]').val());
		var children = parseInt(parent.find('[name=children]').val());
		var adultsHtml = parent.find('.render .adults .multi').data('html');
		console.log(parent, adultsHtml);
		parent.find('.render .adults .multi').html(adultsHtml.replace(':count', adults));
		var childrenHtml = parent.find('.render .children .multi').data('html');
		parent.find('.render .children .multi').html(childrenHtml.replace(':count', children));
		if (adults > 1) {
			parent.find('.render .adults .multi').removeClass('d-none');
			parent.find('.render .adults .one').addClass('d-none');
		} else {
			parent.find('.render .adults .multi').addClass('d-none');
			parent.find('.render .adults .one').removeClass('d-none');
		}
		if (children > 1) {
			parent.find('.render .children .multi').removeClass('d-none');
			parent.find('.render .children .one').addClass('d-none');
		} else {
			parent.find('.render .children .multi').addClass('d-none');
			parent.find('.render .children .one').removeClass('d-none').html(parent.find('.render .children .one').data('html').replace(':count', children));
		}
	}
	
	// Adult & Child Number Script
	$(document).ready(function() {

	  var guestAmount = $('#guestNo');

	  $('#cnt-up').on('click', function() {
		guestAmount.val(Math.min(parseInt($('#guestNo').val()) + 1, 20));
	  });
	  $('#cnt-down').on('click', function() {
		guestAmount.val(Math.max(parseInt($('#guestNo').val()) - 1, 1));
	  });

	});
	
	$(document).ready(function() {

	  var guestAmount = $('#kidsNo');

	  $('#kcnt-up').on('click', function() {
		guestAmount.val(Math.min(parseInt($('#kidsNo').val()) + 1, 20));
	  });
	  $('#kcnt-down').on('click', function() {
		guestAmount.val(Math.max(parseInt($('#kidsNo').val()) - 1, 0));
	  });
	});
	
	// Adult & Child Number Script
	$(document).ready(function() {

	  var guestAmount = $('#roomNo');

	  $('#rom-up').on('click', function() {
		guestAmount.val(Math.min(parseInt($('#roomNo').val()) +1, 20));
	  });
	  $('#rom-down').on('click', function() {
		guestAmount.val(Math.max(parseInt($('#roomNo').val()) - 1, 0));
	  });

	});
	
	$(document).ready(function() {

	  var guestAmount = $('#kidsroomNo');

	  $('#krom-up').on('click', function() {
		guestAmount.val(Math.min(parseInt($('#kidsroomNo').val()) + 1, 20));
	  });
	  $('#krom-down').on('click', function() {
		guestAmount.val(Math.max(parseInt($('#kidsroomNo').val()) - 1, 0));
	  });
	});

	// Guests Dropdown Script
	$(".form-content").on("click", function () {
	  $(".select-guests-dropdown").slideToggle();
	});
	
	// Daterange Script
	$('input[name="dates"]').daterangepicker({
		locale: 'en',
		minDate: moment().format('MM-DD-YYYY'),
		format: 'DD-MM-YYYY'
	});
	$('input[name="dates"]').val('');
	$('input[name="dates"]').attr("placeholder", "Mulai - Berakhir");
	// Check In & Check Out Daterange Script
	$(function() {
	  $('input[name="checkout"]').daterangepicker({
		singleDatePicker: true,
	  });
		$('input[name="checkout"]').val('');
		$('input[name="checkout"]').attr("placeholder","Check Out");
	});
	
	$(function() {
	  $('input[name="checkin"]').daterangepicker({
		singleDatePicker: true,
		
	  });
		$('input[name="checkin"]').val('');
		$('input[name="checkin"]').attr("placeholder","Check In");
	});

	$(function () {
		$('input[name="enddate"]').daterangepicker({
			singleDatePicker: true,
		});
		$('input[name="enddate"]').val('');
		$('input[name="enddate"]').attr("placeholder", "End Date");
	});

	$(function () {
		$('input[name="startdate"]').daterangepicker({
			singleDatePicker: true,
			locale: 'en',
			minDate: moment().format('DD-MM-YYYY'),
			format: 'MM/DD/YYYY'
		});
		$('input[name="startdate"]').val(moment().format('MM/DD/YYYY'));
		$('input[name="startdate"]').attr("placeholder", "Start Date");
	});
	
	// Range Slider
	//$('input[type="range"].distance-radius').rangeslider({
	//	polyfill: false,
	//	onInit: function () {
	//		this.output = $(".distance-title span").html(this.$element.val());
	//	},
	//	onSlide: function (
	//		position, value) {
	//		this.output.html(value);
	//	}
	//});
	
	// 
	$('#tour-category').select2({
		placeholder: "Pilih Tipe",
		allowClear: true,
		dropdownCssClass: 'font-filter-dropdown',
		width: '100%'
	});
	
	
	// Bottom To Top Scroll Script
	$(window).on('scroll', function() {
		var height = $(window).scrollTop();
		if (height > 100) {
			$('#back2Top').fadeIn();
		} else {
			$('#back2Top').fadeOut();
		}
	});
	
	// Dropdown
	$(".drp-select a").on('click', function(){

		$(this).parents(".dropdown").find('.selection').text($(this).text());
		$(this).parents(".dropdown").find('.selection').val($(this).text());

	});
	
	$("#back2Top").on('click', function(event) {
		event.preventDefault();
		$("html, body").animate({ scrollTop: 0 }, "slow");
		return false;
	});
	
	// Filter Search Option
	$("#guest").click(function(){
		$("#g-showing").slideToggle("slow");
	});
	
	// Filter Search Option

	
	// smart-textimonials
	$('#smart-textimonials').slick({
	  slidesToShow:1,
	  arrows: false,
	  autoplay:true,
	  responsive: [
		{
		  breakpoint: 768,
		  settings: {
			arrows: false,
			slidesToShow:1
		  }
		},
		{
		  breakpoint: 480,
		  settings: {
			arrows: false,
			slidesToShow:1
		  }
		}
	  ]
	});
	
	// Property Slide
	$('.testi-slide').slick({
	  slidesToShow:2,
	  arrows: false,
	  autoplay:true,
	  responsive: [
		{
		  breakpoint: 1023,
		  settings: {
			arrows: false,
			slidesToShow:1
		  }
		},
		{
		  breakpoint: 768,
		  settings: {
			arrows: false,
			slidesToShow:1
		  }
		},
		{
		  breakpoint: 480,
		  settings: {
			arrows: false,
			slidesToShow:1
		  }
		}
	  ]
	});
	
	$('#lists-slide').owlCarousel({
		loop:true,
		margin:20,
		dots:true,
		center: true,
		autoplay:true,
		autoplayTimeout:3000,
		stagePadding:0,
		nav:false,
		responsiveClass:true,
		responsive:{
			0:{
				items:1,
				nav:false
			},
			600:{
				items:2,
				nav:false
			},
			1000:{
				items:3,
				nav:false,
				loop:true
			},
			1199:{
				items:3,
				nav:false,
				dots:true,
				loop:true
			}
		}
	});
	
	// Featured Slick Slider
	$('.featured-slick-slide').slick({
		centerMode: true,
		centerPadding: '80px',
		slidesToShow:2,
		responsive: [
		{
		breakpoint: 768,
		settings: {
		arrows:true,
		centerMode: true,
		centerPadding: '60px',
		slidesToShow:2
		}
		},
		{
		breakpoint: 480,
		settings: {
		arrows: false,
		centerMode: true,
		centerPadding: '40px',
		slidesToShow: 1
		}
		}
		]
	});
	
	$('#categorie-slide').owlCarousel({
		loop:true,
		margin:0,
		autoplay:true,
		autoplayTimeout:3000,
		dots:true,
		nav:false,
		responsiveClass:true,
		responsive:{
			0:{
				items:1,
				nav:false
			},
			600:{
				items:3,
				nav:false
			},
			1000:{
				items:3,
				nav:false,
				loop:false
			},
			1199:{
				items:5,
				nav:false,
				dots:true,
				loop:true
			}
		}
	});
	
	$('#ab-categorie-slide').owlCarousel({
		loop:true,
		margin:0,
		autoplay:true,
		autoplayTimeout:3000,
		dots:true,
		nav:false,
		responsiveClass:true,
		responsive:{
			0:{
				items:1,
				nav:false
			},
			600:{
				items:3,
				nav:false
			},
			1000:{
				items:3,
				nav:false,
				loop:false
			},
			1199:{
				items:4,
				nav:false,
				dots:true,
				loop:true
			}
		}
	});
	
	// Select Category
	$('#list-category').select2({
		placeholder: "Choose Category",
		allowClear: true
	});
	
	// Select Rooms
	$('#country').select2({
		placeholder: "Country",
		allowClear: true
	});
	
	// Select Cities
	$('#choose-city').select2({
		placeholder: "Pilih Kota",
		allowClear: true,
		dropdownCssClass: 'font-filter-dropdown'
	});
	
	// Home Slider
	$('.home-slider').slick({
	  centerMode:false,
	  slidesToShow:1,
	  responsive: [
		{
		  breakpoint: 768,
		  settings: {
			arrows:true,
			slidesToShow:1
		  }
		},
		{
		  breakpoint: 480,
		  settings: {
			arrows: false,
			slidesToShow:1
		  }
		}
	  ]
	});
	
	$('.click').slick({
	  slidesToShow:1,
	  slidesToScroll: 1,
	  autoplay:false,
	  autoplaySpeed: 2000,
	});
	
	var fieldUnit = $('.pr-price').children('input').attr('data-unit');
	$('.pr-price').children('input').before('<i class="data-unit">' + fieldUnit + '</i>');
	$("a.close").removeAttr("href").on('click', function() {
		function slideFade(elem) {
			var fadeOut = {
				opacity: 0,
				transition: 'opacity 0.5s'
			};
			elem.css(fadeOut).slideUp();
		}
		slideFade($(this).parent());
	});
	$(".price-add-wrapper").each(function() {
		var switcherSection = $(this);
		var switcherInput = $(this).find('.switch input');
		if (switcherInput.is(':checked')) {
			$(switcherSection).addClass('switch-on');
		}
		switcherInput.change(function() {
			if (this.checked === true) {
				$(switcherSection).addClass('switch-on');
			} else {
				$(switcherSection).removeClass('switch-on');
			}
		});
	});
	
	// Advance Single Slider
	$(function() { 
	// Card's slider
	  var $carousel = $('.slider-for');

	  $carousel
		.slick({
		  slidesToShow: 1,
		  slidesToScroll: 1,
		  arrows: false,
		  fade: true,
		  adaptiveHeight: true,
		  asNavFor: '.slider-nav'
		})
		.magnificPopup({
		  type: 'image',
		  delegate: 'a:not(.slick-cloned)',
		  closeOnContentClick: false,
		  tLoading: 'Ð—Ð°Ð³Ñ€ÑƒÐ·ÐºÐ°...',
		  mainClass: 'mfp-zoom-in mfp-img-mobile',
		  image: {
			verticalFit: true,
			tError: '<a href="%url%">Ð¤Ð¾Ñ‚Ð¾ #%curr%</a> Ð½Ðµ Ð·Ð°Ð³Ñ€ÑƒÐ·Ð¸Ð»Ð¾ÑÑŒ.'
		  },
		  gallery: {
			enabled: true,
			navigateByImgClick: true,
			tCounter: '<span class="mfp-counter">%curr% Ð¸Ð· %total%</span>', // markup of counte
			preload: [0,1] // Will preload 0 - before current, and 1 after the current image
		  },
		  zoom: {
			enabled: true,
			duration: 300
		  },
		  removalDelay: 300, //delay removal by X to allow out-animation
		  callbacks: {
			open: function() {
			  //overwrite default prev + next function. Add timeout for css3 crossfade animation
			  $.magnificPopup.instance.next = function() {
				var self = this;
				self.wrap.removeClass('mfp-image-loaded');
				setTimeout(function() { $.magnificPopup.proto.next.call(self); }, 120);
			  };
			  $.magnificPopup.instance.prev = function() {
				var self = this;
				self.wrap.removeClass('mfp-image-loaded');
				setTimeout(function() { $.magnificPopup.proto.prev.call(self); }, 120);
			  };
			  var current = $carousel.slick('slickCurrentSlide');
			  $carousel.magnificPopup('goTo', current);
			},
			imageLoadComplete: function() {
			  var self = this;
			  setTimeout(function() { self.wrap.addClass('mfp-image-loaded'); }, 16);
			},
			beforeClose: function() {
			  $carousel.slick('slickGoTo', parseInt(this.index));
			}
		  }
		});
	  $('.slider-nav').slick({
		slidesToShow:6,
		slidesToScroll:1,
		asNavFor: '.slider-for',
		dots: false,
		centerMode: false,
		focusOnSelect: true
	  });
	  
	  
	});

	
	// MagnificPopup
	$('body').magnificPopup({
		type: 'image',
		delegate: 'a.mfp-gallery',
		fixedContentPos: true,
		fixedBgPos: true,
		overflowY: 'auto',
		closeBtnInside: false,
		preloader: true,
		removalDelay: 0,
		mainClass: 'mfp-fade',
		gallery: {
			enabled: true
		}
	});
	
});