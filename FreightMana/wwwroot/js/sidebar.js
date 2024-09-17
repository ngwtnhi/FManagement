// Order
var menu = document.getElementsByClassName("menu")[0];
var menuVisible = localStorage.getItem('menuVisible') === 'true';

// Khôi phục trạng thái hiển thị của menu
menu.style.display = menuVisible ? "block" : "none";

function order_Menu() {
    menuVisible = !menuVisible;
    menu.style.display = menuVisible ? "block" : "none";
    // Lưu trạng thái hiển thị của menu
    localStorage.setItem('menuVisible', menuVisible);
}

// Staff
var staff = document.getElementsByClassName("staff")[0];
var staffVisible = localStorage.getItem('staffVisible') === 'true';

staff.style.display = staffVisible ? "block" : "none";

function staff_Menu() {
    staffVisible = !staffVisible;
    staff.style.display = staffVisible ? "block" : "none";
    localStorage.setItem('staffVisible', staffVisible);
}




function myFunction() {
    document.getElementById("myLogout").classList.toggle("show");
}

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
        window.location.href = '/LoginAdmin'; 
    }
    return false;
}
function confirmSave() {

    alert('Lưu thành công');
        //window.location.href = '/ListOrder/SaveStatus';
   
    return false;
}

