var dropdown = document.getElementById("shippingMethod");
  var priceDisplay = document.getElementById("priceDisplay");

  dropdown.addEventListener("change", function() {
    var selectedValue = dropdown.value;
    var shippingCost = calculateShippingCost(selectedValue);
    priceDisplay.textContent = shippingCost + " đ"; // Cập nhật nội dung của phần tử
  });

  function calculateShippingCost(method) {
    // Giả sử có các giá vận chuyển khác nhau cho từng phương thức
    if (method === "standard") {
      return 12000;
    } else if (method === "express") {
      return 29000;
    } else if (method === "overnight") {
      return 59000;
    } else {
      return "Không xác định";
    }
  }