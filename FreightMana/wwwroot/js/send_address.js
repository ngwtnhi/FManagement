// SEND
var secitis = document.getElementById("secity");
var sedistricts = document.getElementById("sedistrict");
var sewards = document.getElementById("seward");
var reParameter = {
  url: "js/data.json", //Đường dẫn đến file chứa dữ liệu hoặc api do backend cung cấp
  method: "GET", //do backend cung cấp
  responseType: "application/json", //kiểu Dữ liệu trả về do backend cung cấp
};
//gọi ajax = axios => nó trả về cho chúng ta là một promise
var promise = axios(reParameter);
//Xử lý khi request thành công
promise.then(function (result) {
  senderCity(result.data);
});

function senderCity(data) {
  for (const z of data) {
    secitis.options[secitis.options.length] = new Option(z.Name, z.Id);
  }

  // xứ lý khi thay đổi tỉnh thành thì sẽ hiển thị ra quận huyện thuộc tỉnh thành đó
  secitis.onchange = function () {
    sedistricts.length = 1;
    sewards.length = 1;
    if(this.value != ""){
      const result = data.filter(n => n.Id === this.value);

      for (const k of result[0].Districts) {
        sedistricts.options[sedistricts.options.length] = new Option(k.Name, k.Id);
      }
    }
  };

   // xứ lý khi thay đổi quận huyện thì sẽ hiển thị ra phường xã thuộc quận huyện đó
  sedistricts.onchange = function () {
    sewards.length = 1;
    const sedataCity = data.filter((n) => n.Id === secitis.value);
    if (this.value != "") {
      const sedataWards = sedataCity[0].Districts.filter(n => n.Id === this.value)[0].Wards;

      for (const w of sedataWards) {
        sewards.options[sewards.options.length] = new Option(w.Name, w.Id);
      }
    }
  };
}