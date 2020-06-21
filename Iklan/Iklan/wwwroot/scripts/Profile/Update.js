$(document).ready(function () {

});
function updatePassword() {
	var linkUpdate = urlUpdate;
	var oldpass = $("#oldpass").val();
	var newpass = $("#newpass").val();
	var conpass = $("#conpass").val();
	var validate = true;
	var valnew = true;

	var data = {
		OldPassword: oldpass,
		NewPassword: newpass
	};

	if (oldpass === null || oldpass === undefined || oldpass === "") {
		validate = false;
	}
	if (newpass === null || newpass === undefined || newpass === "") {
		validate = false;
	}
	if (conpass === null || conpass === undefined || conpass === "") {
		validate = false;
	}
	else if (newpass !== conpass)
	{
		validate = false;
		valnew = false;
	}

	if (validate === true && valnew === true) {
		$.ajax({
			url: linkUpdate,
			data: data,
			type: 'POST',
			success: function (result) {
				if (result.roleName === null) {
					alert('Fail to change');
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
	else if (valnew !== true)
	{
		alert('Password tidak sesuai.');
	}
	else
	{
		alert('Mohon Lengkapi data anda.');
	}
}

function updateProfile() {
	var linkUpdate = urlUpdate;
	var firstame = $("#firstname").val();
	var lastname = $("#lastname").val();
	var username = $("#userlogin").val();
	var namaperusahaan = $("#comname").val();
	var emailperusahaan = $("#comemail").val();
	var notelp = $("#comphone").val();
	var kategori = $("#category").val();
	var npwp = $("#npwp").val();
	var website = $("#website").val();
	var alamat = $("#comaddress").val();
	var catatan = $("#notes").val();
	var validate = true;

	var data = {
		FirstName: firstame,
		LastName: lastname,
		UserName: username,
		EmailPIC: username,
		NamaPerusahaan: namaperusahaan,
		EmailPerusahaan: emailperusahaan,
		NoTelp: notelp,
		Kategori: kategori,
		NPWP: npwp,
		Website: website,
		Alamat: alamat,
		Catatan: catatan
	};

	if (firstame === null || firstame === undefined || firstame === "") {
		validate = false;
	}
	if (lastname === null || lastname === undefined || lastname === "") {
		validate = false;
	}
	if (username === null || username === undefined || username === "") {
		validate = false;
	}
	if (emailperusahaan === null || emailperusahaan === undefined || emailperusahaan === "") {
		validate = false;
	}
	if (namaperusahaan === null || namaperusahaan === undefined || namaperusahaan === "") {
		validate = false;
	}

	if (validate === true) {
		$.ajax({
			url: linkUpdate,
			data: data,
			type: 'POST',
			success: function (result) {
				if (result.roleName === null) {
					alert('Fail to login');
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
	else
	{
		alert('Mohon Lengkapi data anda.');
	}
}
