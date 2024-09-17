window.onload = function() {
    var forms = document.querySelectorAll('form'); // Lấy tất cả các form
  
    forms.forEach(function(form) {
      form.addEventListener('submit', function(e) {
        e.preventDefault(); // Ngăn chặn hành vi mặc định của form
        var inputField = form.querySelector('input[type="text"]');
        var input = inputField.value; // Lấy giá trị từ input
        var key = inputField.name; // Lấy tên của trường nhập liệu để sử dụng làm khóa cho localStorage
        localStorage.setItem(key, input); // Lưu giá trị input vào localStorage
      });
    });
  };


  