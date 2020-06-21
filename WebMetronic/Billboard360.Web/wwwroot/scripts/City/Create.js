var BaseUrl = window.location.origin;

function FillProvince() {
  $.post(BaseUrl + "/Dashboard/GetProvince", null, function (data) {
      if (data) {
          var provID = $("#selectedProv").val();
          $("#lstprovince").append("<option value=''>Pilih Provinsi</option>");
          $(data).each(function () {
              if (this.provinsi == provID || this.provinceID == provID) {
                  $("#lstprovince").append('<option value=' + this.kode + '|' + this.provinsi + ' selected>' + this.provinsi + '</option >');         
              } else {
                  $("#lstprovince").append($('<option/>', { value: this.kode + '|' + this.provinsi }).html(this.provinsi));
              }
          });
      }
  }, 'json')
}

$(document).ready(function () {
  FillProvince();
});