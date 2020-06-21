var BillBoardTypeds = [];
var cityds = [];
$(document).ready(function () {
    if (isFiltered === "MDB") {
        document.getElementById("profileMenu").style.display = "block";
        document.getElementById("wishMenu").style.display = "block";
        document.getElementById("cartMenu").style.display = "block";
        document.getElementById("bookMenu").style.display = "block";
        document.getElementById("signin").style.display = "none";
        document.getElementById("signup").style.display = "none";
    } else {
        document.getElementById("profileMenu").style.display = "none";
        document.getElementById("wishMenu").style.display = "none";
        document.getElementById("cartMenu").style.display = "none";
        document.getElementById("bookMenu").style.display = "none";
        document.getElementById("signin").style.display = "block";
        document.getElementById("signup").style.display = "block";
    }
    $.when(GetCity()).done(function () {
        $.when(GetBillBoardType()).done(function () {
            fillBillBoardType();
            fillCity();

            const urlParams = new URLSearchParams(window.location.search);
            const location = urlParams.get('location');

            if (location != null && location != "") {
                $("#location").val(location);
            }

            const type = urlParams.get('type');

            if (type != null && type != "") {
                $("#tour-category option[value='" + type + "']").attr("selected", "selected");
            }

            const city = urlParams.get('city');

            if (city != null && type != "") {
                $("#choose-city option[value='" + city + "']").attr("selected", "selected");
            }

            const filterfrom = urlParams.get('filterfrom');
            const filterto = urlParams.get('filterto');

            if (filterfrom != null && filterfrom != "" && filterto != null && filterto != "") {
                $("#dateFilter").val(filterfrom + " - " + filterto);
            }
        });
    });
});

window.onload = function () {
    if (document.querySelectorAll('.cust-date') !== null) {
        var cdate = document.querySelectorAll('.cust-date');
        for (var i = 0; i < cdate.length; i++) {
            cdate[i].innerHTML = formatDate(new Date(cdate[i].innerHTML.toString()));
        }
    }
    if (document.querySelectorAll('.cart-price') !== null) {
        var cprice = document.querySelectorAll('.cart-price');
        for (var x = 0; x < cprice.length; x++) {
            cprice[x].innerHTML = addCommas(cprice[x].innerHTML.toString());
        }
    }

   
}

function formatDate(date) {
    var monthNames = [
        "Jan", "Feb", "Mar",
        "Apr", "Mei", "Jun", "Jul",
        "Agu", "Sep", "Okt",
        "Nov", "Des"
    ];


    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;
}

function checkTime(i) {
    return (i < 10) ? "0" + i : i;
}

function formatHours(date) {
    var hour = checkTime(date.getHours());
    var minutes = checkTime(date.getMinutes());
    var seconds = checkTime(date.getSeconds());

    return hour + ":" + minutes + ":" + seconds;
}

function addCommas(nStr) {
    nStr += '';
    var comma = /,/g;
    nStr = nStr.replace(comma, '');
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function GetBillBoardType() {
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

function GetCity() {
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

function fillBillBoardType() {
    $("#tour-category").empty();
    var data = BillBoardTypeds;
    $("#tour-category").append('<option value="" selected disabled>Please select</option>');
    for (var i = 0; i < data.length; i++) {
        $("#tour-category").append('<option value="' + data[i].type + '">' + data[i].type + '</option>');
    }

}

function fillCity() {
    $("#choose-city").empty();
    var data = cityds;
    $("#choose-city").append('<option value="" selected disabled>Please select</option>');
    for (var i = 0; i < data.length; i++) {
        $("#choose-city").append('<option value="' + data[i].kota + '">' + data[i].kota + '</option>');
    }

}

function loginBuyer() {
    var username = $("#username").val();
    var password = $("#password").val();
    var linkLogin = urlLogin;

    var data = {
        UserName: username,
        Password: password
    };
    //alert('a');
    if (username !== "" && username.length >= 8 && password !== "" && password.length >= 4) {
        $.ajax({
            url: linkLogin,
            data: data,
            type: 'POST',
            success: function (result) {
                if (result.roleName === null) {
                    Swal.fire({

                        type: 'error',
                        title: 'Gagal login',
                        html: 'Username atau password tidak cocok'
                    });
                } else {
                    location.reload(true);
                }
            }
            //},
            //error: function (data) {
            //    alert('Something Went Wrong');
            //    startSpinner('loading..', 0);
            //}
        });
    }


}


function searchBarAction() {
    var link = urlSearchBar;
    var location = $("#location").val();
    var type = $("#tour-category").val();
    var city = $("#choose-city").val();
    var dateFilter = $("#dateFilter").val();
    var dateFilterFrom = "";
    var dateFilterTo = "";
    var dateFilterSplit;

    if (dateFilter != null) {
        dateFilterSplit = dateFilter.split(" - ");
        dateFilterFrom = dateFilterSplit[0];
        dateFilterTo = dateFilterSplit[1];
        //alert(dateFilterFrom);
        //alert(dateFilterTo);

    }

    //string location, string type, string city
    var url = link + "?location=" + location + "&type=" + type + "&city=" + city + "&page=1" + "&filterfrom=" + dateFilterFrom + "&filterto=" + dateFilterTo;
    window.location.href = url;
  //  alert(dateFilter);
}
//$(function () {
//    $("#loginModalForm").validate({
//        rules: {
//            username: {
//                required: true,
//                minlength: 8
//            },
//            password: {
//                required: true,
//                minlength: 4
//            }
//        },
//        messages: {
//            username: {
//                required: "Please enter some data",
//                minlength: "Your data must be at least 8 characters"
//            },
//            password: {
//                required: "Please enter some data",
//                minlength: "Your data must be at least 4 characters"
//            }
//        }
//    });
//});

