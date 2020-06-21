var BillBoardTypeds = [];
var cityds = [];

$(document).ready(function () {
    init();

    $.when(GetCitySearch()).done(function () {
        $.when(GetBillBoardTypeSearch()).done(function () {
            fillBillBoardTypeSearch();
            fillCitySearch();

            const urlParams = new URLSearchParams(window.location.search);
            const type = urlParams.get('type');

            if (type != null && type != "") {
                $("#type option[value='" + type + "']").attr("selected", "selected");
               
            }

            const city = urlParams.get('city');

            if (city != null && city != "") {
                $("#city option[value='" + city + "']").attr("selected", "selected");
            }

            const panjang = urlParams.get('panjang');

            if (panjang != null && panjang != "") {
                $("#Panjang").val(panjang);
            }

            const lebar = urlParams.get('lebar');

            if (lebar != null && lebar != "") {
                $("#Lebar").val(lebar);
            }

            const minprice = urlParams.get('minprice');

            if (minprice != null && minprice != "") {
                $("#priceMinimum").val(minprice);
                priceMinimumChanged();
            }

            const maxprice = urlParams.get('maxprice');

            if (maxprice != null && maxprice != "") {
                $("#priceMaximum").val(maxprice);
                priceMaximumChanged();
            }

            const location = urlParams.get('location');

            if (location != null && location != "") {
                $("#Location").val(location);
            }

            const sorting = urlParams.get('sorting');
            if (sorting != null && sorting != "") {
                if (sorting == "AtoZ") {
                    sorting = "A-Z";
                }
                else if (sorting == "ZtoA") {
                    sorting = "Z-A";
                }
                $("#sortingcbo").text(sorting);
            }
        });
    });
    $('body').on('focus', 'input[type=number]', function (e) {
        $(this).on('wheel.disableScroll', function (e) {
            e.preventDefault()
        })
    });
    $('body').on('blur', 'input[type=number]', function (e) {
        $(this).off('wheel.disableScroll')
    })
   
})


function init() {
    $('input[type="range"]#priceMaximum').rangeslider({
        polyfill: false,
        onInit: function () {
            this.output = $("#priceMaximumStr span").html(this.$element.val());
        },
        onSlide: function (
            position, value) {
            this.output.html(value);
        }
    });


    $('input[type="range"]#priceMinimum').rangeslider({
        polyfill: false,
        onInit: function () {
            this.output = $("#priceMinimumStr span").html(this.$element.val());
        },
        onSlide: function (
            position, value) {
            this.output.html(value);
        }
    });


    $('#city').select2({
        placeholder: "Select City",
        allowClear: true
    });

    $('#type').select2({
        placeholder: "Select Type",
        allowClear: true
    });
}

function GetBillBoardTypeSearch() {
    return $.ajax({
        url: urlGetBillBoardType,
        success: function (result) {
            BillBoardTypeds = [];
            BillBoardTypeds = result;
            // console.log(JSON.stringify(BillBoardTypeds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function GetCitySearch() {
    return $.ajax({
        url: urlGetCity,
        success: function (result) {
            cityds = [];
            cityds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillBillBoardTypeSearch() {
    $("#type").empty();
    var data = BillBoardTypeds;
    $("#type").append('<option value="" selected disabled>Please select</option>');
    for (var i = 0; i < data.length; i++) {
        $("#type").append('<option value="' + data[i].type + '">' + data[i].type + '</option>');
    }

}

function fillCitySearch() {
    $("#city").empty();
    var data = cityds;
    console.log(data);
    $("#city").append('<option value="" selected disabled>Please select</option>');
    for (var i = 0; i < data.length; i++) {
        $("#city").append('<option value="' + data[i].kota + '">' + data[i].kota + '</option>');
    }

}


function initPopup() {
    var minStartDate = new Date();

    var convStartDate = moment(minStartDate, "MM/DD/YYYY");
    var today = moment(convStartDate, "DD/MM/YYYY");

    $('input[name="startdate"]').daterangepicker({
        singleDatePicker: true,
        locale: 'id',
        minDate: today,
        //format: 'dd/mm/yyyy'
    });

    $('input[name="startdate"]').val('');
    $('input[name="startdate"]').attr("placeholder", "Start Date");

    var elementStart = $("#startdate").val();

    var tempValueStart = moment(elementStart, "MM/DD/YYYY");

    var fillStartt = moment(tempValueStart, "DD/MM/YYYY");

    $('input[name="startdate"]').val(fillStartt);

    //init end
    $('input[name="enddate"]').daterangepicker({
        singleDatePicker: true,
        locale: 'id'
    });
    $('input[name="enddate"]').val('');
    $('input[name="enddate"]').attr("placeholder", "End Date");

    $("#startdate").on("change", function (e) {

        var minDuration = 30;

        if ($("#startdate").val() != null && $("#startdate").val() != "") {

            var realCurDate = moment(e.currentTarget.value, "MM/DD/YYYY");

            var curDate = moment(realCurDate, "DD/MM/YYYY") ;

            var realMyDate = moment($("#startdate").val(), "MM/DD/YYYY");

            var mydate = moment(realMyDate, "DD/MM/YYYY");

            if (mydate == "Invalid Date") {
                var tempMydate = $("#enddate").val();
                mydate = moment(tempMydate, "DD/MM/YYYY");
            } else {
                var tempMyDate = $("#enddate").val();
                console.log(tempMyDate);
                mydate = moment(tempMyDate, "DD/MM/YYYY");

                if (mydate == "Invalid Date") {
                    var tempDate = $("#enddate").val();
                    mydate = moment(tempMydate, "DD/MM/YYYY");
                }

            }

            if (curDate == "Invalid Date") {
                curDate = moment(e.currentTarget.value, 'DD/MM/YYYY');
            }
            var end = moment(mydate, "MM/DD/YYYY");
            var start = moment(curDate, "MM/DD/YYYY");

            var startCal = moment(curDate, "DD/MM/YYYY");


            var diff = date_diff_indays(startCal, end);


            //Kalau hasil kurang dari minimal durasi harus di hitung ulang untuk enddate nya
            //dan itu ribet cuk :')
            if (diff < minDuration) {
                diff = minDuration;
                end = moment(start, "MM/DD/YYYY").add('days', diff);

                var displayEnd = moment(end).format("DD/MM/YYYY");

                $('input[name="enddate"]').val(displayEnd);

            } else {
                diff = diff;
            }

            var price = $("#HargaAwalH").val();

            var totAmt = diff * (price * 1);

            $("#totAmt").text("Rp " + addCommas(totAmt.toString()));

            $("#totDuration").text(addCommas(diff.toString()));

            var displayStart = moment(startCal).format("DD/MM/YYYY");

            $('input[name="startdate"]').val(displayStart);
        }

    });

    $("#enddate").on("change", function (e) {

        if ($("#startdate").val() != null && $("#startdate").val() != "") {

            var curDate = moment(e.currentTarget.value, "MM/DD/YYYY");

            var mydate = moment($("#enddate").val(), "MM/DD/YYYY");

            if (mydate == "Invalid Date") {
                var tempMydate = $("#enddate").val();
                mydate = moment(tempMydate, "DD/MM/YYYY");
            } else {
                var tempMyDate = $("#enddate").val();
                console.log(tempMyDate);
                mydate = moment(tempMyDate, "MM/DD/YYYY");

                if (mydate == "Invalid Date") {
                    var tempDate = $("#enddate").val();
                    mydate = moment(tempMydate, "MM/DD/YYYY");
                }

            }

            if (curDate == "Invalid Date") {
                curDate = moment(e.currentTarget.value, 'DD/MM/YYYY');
            }
            var end = moment(mydate, "MM/DD/YYYY");
            var start = moment(curDate, "MM/DD/YYYY");


            var startCalVal = $("#startdate").val();
            var startCal = moment(startCalVal, "DD/MM/YYYY");

            var diff = date_diff_indays(startCal, end);
            var price = $("#HargaAwalH").val();

            var totAmt = diff * (price * 1);

            $("#totAmt").text("Rp " + addCommas(totAmt.toString()));

            $("#totDuration").text(addCommas(diff.toString()));

            var displayStart = moment(start).format("DD/MM/YYYY");

            $('input[name="enddate"]').val(displayStart);

        }

    });

    if ($("#enddate").val() == null || $("#enddate").val() == "") {

        const urlParams = new URLSearchParams(window.location.search);
        var filterfrom = urlParams.get('filterfrom');

        if (filterfrom === null || filterfrom === "") {
            var today = new Date();
            filterfrom = today;
        }

        var tempStartDate = moment(filterfrom, "MM/DD/YYYY");
        var valueStart = moment(tempStartDate, "DD/MM/YYYY");


        var duration = 30;
        var curDate = new Date();
        var newDate = new Date();
        var oldDatecart = new Date();

        if (filterfrom != null && filterfrom != "") {
            $('input[name="startdate"]').val(moment(valueStart).format('DD/MM/YYYY'));
            curDate = new Date(filterfrom);
        }

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
            minDate: moment(newDate).format('MM/DD/YYYY'),
            format: 'DD/MM/YYYY'
        });

        $('input[name="enddate"]').val(moment(newDate).format('DD/MM/YYYY'));

    }
}

function priceMaximumChanged() {
    var maxPrice = addCommas($("#priceMaximum").val());
    $("#priceMaximumStr1").text(maxPrice);
}

function addCommas(str) {
    return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function priceMinimumChanged() {
    var minPrice = addCommas($("#priceMinimum").val());

    $("#priceMinimumStr1").text(minPrice);
}

function filterSearchMedia() {
    var link = urlSearchBar;
    var location = $("#Location").val();
    var type = $("#type").val();
    var city = $("#city").val();
    var panjang = $("#Panjang").val();
    var lebar = $("#Lebar").val();
    var minprice = $("#priceMinimum").val();
    var maxprice = $("#priceMaximum").val();


    //string location, string type, string city
    var url = link + "?location=" + location + "&type=" + type + "&city=" + city + "&page=1" + "&panjang=" + panjang + "&lebar=" + lebar + "&minprice=" + minprice + "&maxprice=" + maxprice;
    window.location.href = url;
    //alert(url);
}
