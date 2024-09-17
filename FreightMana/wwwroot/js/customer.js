window.onclick = function(event) {
    if (!event.target.matches('.icon-down')) {
      var logouts = document.getElementsByClassName("logout-content");
      for (var i = 0; i < logouts.length; i++) {
        var openDropdown = logouts[i];
        if (openDropdown.classList.contains('show')) {
          openDropdown.classList.remove('show');
        }
      }
    }
}

function confirmLogout() {
    if (confirm("Bạn có chắc muốn đăng xuất không?")) {
        window.location.href = "login_customer.html";
    }
    return false;
}