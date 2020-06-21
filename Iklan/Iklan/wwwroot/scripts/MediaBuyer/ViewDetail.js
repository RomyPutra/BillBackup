var directionsDisplay,
    directionsService,
    map;

$(document).ready(function () {
    initMap();
    displayMap();
    $("#owl-demo").owlCarousel({
        navigation: true,
        paginationSpeed: 1000,
        goToFirstSpeed: 2000,
        singleItem: true,
        autoHeight: true,
        transitionStyle: "fade",
        items: 1,
        loop: true,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true
    });
    $('.play').on('click', function () {
        owl.trigger('play.owl.autoplay', [5000])
    })
    $('.stop').on('click', function () {
        owl.trigger('stop.owl.autoplay')
    })

    $("#startdate").on("change", function (e) {
      
        let duration = 30;
        var curDate = new Date(e.currentTarget.value);
        var newDate = new Date();
        var oldDatecart = new Date();
        if (oldDatecart.getDate() != curDate.getDate()) {
            newDate.setMonth(curDate.getMonth());
            newDate.setDate(curDate.getDate() + duration);
            oldDatecart = curDate;
        } else {
            newDate.setDate(oldDatecart.getDate() + duration);
        }

        $('input[name="enddate"]').daterangepicker({
            singleDatePicker: true,
            locale: 'en',
            minDate: newDate,
            format: 'DD/MM/YYYY'
        });

        if ($("#startdate").val() != null && $("#startdate").val() != "") {
            var diff = date_diff_indays($("#startdate").val(), $("#enddate").val());
            var price = $("#HargaAwalH").val();

            var totAmt = diff * (price * 1);

            $("#totAmt").text("Rp " + addCommas(totAmt.toString()));
            $("#totDuration").text(addCommas(diff.toString()));
        }
    });

    $("#enddate").on("change", function (e) {
        if ($("#startdate").val() != null && $("#startdate").val() !="") {
            var diff = date_diff_indays($("#startdate").val(), $("#enddate").val());
            var price = $("#HargaAwalH").val();

            var totAmt = diff * (price * 1);

            $("#totAmt").text("Rp " + addCommas(totAmt.toString()));

            $("#totDuration").text(addCommas(diff.toString()));
        }
       
    });

});

function addCommas(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function initMap() {
    var LatLng = { lat: -7.411299, lng: 112.562634 };
    var directionsService = new google.maps.DirectionsService;
    var directionsDisplay = new google.maps.DirectionsRenderer;
    var map = new google.maps.Map(document.getElementById('map-main'), {
        zoom: 4,
        center: LatLng
    });
}

function displayMap() {
    var LatLng = { lat: Number($("#LatitudeH").val()), lng: Number($("#longitudeH").val()) };
    var directionsService = new google.maps.DirectionsService;
    var directionsDisplay = new google.maps.DirectionsRenderer;
    var map = new google.maps.Map(document.getElementById('map-main'), {
        zoom: 12,
        center: LatLng
    });

    var marker = new google.maps.Marker({
        position: LatLng,
        map: map,
        title: $("#alamat").text()
    });

    var infowindow = new google.maps.InfoWindow({});
    google.maps.event.addListener(marker, 'click', (function (marker) {
        return function () {
            var contentString = '<div id="content" style="display: flex;">' +
                '<div class="col-md-5">' +
                '<img src="' + $("#imgH").val() + '" class="listimage" style="width:100%;height:150px;" />' +
                '</div>' +
                '<div class="col-md-7" style="text-align: left;">' +
                '<p>' + $("#alamat").text() + '</p>' +
                '<p style="margin-bottom: 5px;">' + $("#kota").text() + '</p>' +
                '<p style="margin-bottom: 5px;">H/V : ' + $("#HorV").text() + '</p>' +
                '<p style="margin-bottom: 5px;">Tipe Billboard : ' + $("#tipe").text()  + '</p>' +
                '<p style="margin-bottom: 5px;">Rp. <span class="price">' + $("#priceH").val() + '</span></p>' +
                '</div>' +
                '</div>';
            infowindow.setContent($("#alamat").text());
            infowindow.setContent(contentString);
            infowindow.open(map, marker);
        }
    })(marker));
}

function addToCartClicked() {
    var savedata = {
        "SiteID": $("#SiteIDH").val(),
        "SiteItemID": $("#SiteItemIDH").val(),
        "SitePriceID": $("#SitePriceIDH").val(),
        "Price": $("#HargaAwalH").val(),
        "StartDate": $("#startdate").val(),
        "EndDate": $("#enddate").val()
    };
    console.log(JSON.stringify(savedata));

    $.ajax({
        type: "POST",
        data: savedata,
        url: urlAddToCart,
        success: function (result) {
            console.log(JSON.stringify(result));

            if (result.response == false) {
                alert(result.message);
            }
            else {
                alert(result.message);

            }
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bookingNowClicked() {
    var savedata = {
        "SiteID": $("#SiteIDH").val(),
        "SiteItemID": $("#SiteItemIDH").val(),
        "SitePriceID": $("#SitePriceIDH").val(),
        "Price": $("#HargaAwalH").val(),
        "StartDate": $("#startdate").val(),
        "EndDate": $("#enddate").val()
    };
    console.log(JSON.stringify(savedata));

    swal({
        type: 'warning',
        title: 'Are you sure?',
        html: 'You want to submit this data',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {
            $.ajax({
                type: "POST",
                data: savedata,
                url: urlAddtoBook,
                success: function (result) {
                    console.log(JSON.stringify(result));

                    if (result.response == false) {
                        alert(result.message);
                    }
                    else {
                        alert(result.message);

                    }
                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });
        }
    });

   
}


var date_diff_indays = function (date1, date2) {
    dt1 = new Date(date1);
    dt2 = new Date(date2);
    return Math.floor((Date.UTC(dt2.getFullYear(), dt2.getMonth(), dt2.getDate()) - Date.UTC(dt1.getFullYear(), dt1.getMonth(), dt1.getDate())) / (1000 * 60 * 60 * 24));
}