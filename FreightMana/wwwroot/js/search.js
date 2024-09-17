
function scrollToSearch() {
    event.preventDefault(); // Ngăn chặn hành vi mặc định
    document.getElementById("searchForm").scrollIntoView({ behavior: "smooth" });
}