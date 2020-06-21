function laporkanSite(target) {
	document.getElementById("loading").style.display = "block";
	var siteID = document.getElementById('siteReport').textContent;
	var desc = document.getElementById('laporan').value;
	$.post(target, { siteID: siteID, desc: desc },
		function (data) {
			document.getElementById("loading").style.display = "none";
			alert("Report Submitted");
			$("#reportModal").modal('hide');
			location.reload(true);
		}).fail(function (e) {
			document.getElementById("loading").style.display = "none";
			alert("Fail to Added Data");
		});
}
function bayarSekarang(target, bookID, bookAMT) {
	document.getElementById("loading").style.display = "block";
	let priceAMT = $('#amtHarga').val();
	let discAMT = $('#amtDisc').val();
	let totalAMT = $('#amtTotal').val();
	bookAMT = $('#amtBayar').val();
	$.post(target, { bookID: bookID, hargaAmt: priceAMT, discAMT: discAMT, totalAmt: totalAMT, payAMT: bookAMT },
		function (data) {
			document.getElementById("loading").style.display = "none";
			window.location.href = data.redirect_url;
		}).fail(function (e) {
			document.getElementById("loading").style.display = "none";
			alert("Fail to Added Data");
		});
}
function changeSite(target, bookDetailID) {
	let tipe = "";
	let prov = "";
	let city = "";
	let minPrice = 0;
	let maxPrice = 0;
	var url = target + "?tipeBillboard=" + tipe + "&provinsi=" + prov + "&kota=" + city + "&prevSite=" + bookDetailID + "&minHarga=" + minPrice + "&maxHarga=" + maxPrice;
	window.location.href = url;
}
function deleteSite(target, bookDetailID) {
	document.getElementById("loading").style.display = "block";
	$.post(target, { BookDetailID: bookDetailID },
		function (data) {
			document.getElementById("loading").style.display = "none";
			alert("Site Deleted");
			location.reload(true);
		}).fail(function (e) {
			document.getElementById("loading").style.display = "none";
			alert("Fail to Delete Data");
		});
}
$(document).ready(function () {
	$('.optradio').change(function (e) {
		var selectedValue = $(this).val();
		$('#amtBayar').val(selectedValue);
		$('#pamount').text(addCommas(selectedValue));
	});
});

