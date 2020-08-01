function uptable(res) {
    if (res != null && res.code == 0) {
        window.location("newlogin.html");
    }
    else if (res != null && res.code == 1) {
        alert(res.Message);
    }
    else {
        alert("已完成")
        window.location.reload();
    }
}