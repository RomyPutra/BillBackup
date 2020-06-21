window.onload = function () {
	var btns = document.getElementsByClassName("smenu");
	for (var i = 0; i < btns.length; i++) {
		btns[i].addEventListener("click", function () {
			var headers = document.getElementById("divitem");
			if (headers !== null || headers !== undefined) {
				var divs = headers.getElementsByClassName("items");
				for (var j = 0; j < divs.length; j++) {
					if (this.innerHTML.toUpperCase() === "SEMUA") {
						divs[j].style.display = "block";
					}
					else if (divs[j].className.toUpperCase().includes(this.innerHTML.toUpperCase())) {
						divs[j].style.display = "block";
					}
					else if (this.innerHTML.toUpperCase() === "ON PROGRESS" && divs[j].className.toUpperCase().includes("ONPROGRESS")) {
						divs[j].style.display = "block";
					}
					else {
						divs[j].style.display = "none";
					}
				}
			}
		}, false);
	}
	if (document.querySelectorAll('.cust-date') !== null) {
		var cdate = document.querySelectorAll('.cust-date');
		for (var x = 0; x < cdate.length; x++) {
			cdate[x].innerHTML = formatDate(new Date(cdate[x].innerHTML.toString()));
		}
	}
	if (document.querySelectorAll('.cust-time') !== null) {
		var ctime = document.querySelectorAll('.cust-time');
		for (var y = 0; y < cdate.length; y++) {
			ctime[y].innerHTML = formatHours(new Date(ctime[y].innerHTML.toString()));
		}
	}
	if (document.querySelectorAll('.cart-price') !== null) {
		var cprice = document.querySelectorAll('.cart-price');
		for (var z = 0; z < cprice.length; z++) {
			cprice[z].innerHTML = addCommas(cprice[z].innerHTML.toString());
		}
	}
}
