$(document).ready(function() {
    $('.eye').click(function() {
        // Lấy input gần nhất so với icon mắt vừa được click
        let passwordField = $(this).prev('.toggle-password');

        if (passwordField.attr('type') === 'password') {
            passwordField.attr('type', 'text');
        } else {
            passwordField.attr('type', 'password');
        }

       
        // $(this).hide();
    });
});
function validatePassword() {
    let password = document.getElementById('password').value;
    let confirmPassword = document.getElementById('confirm_password').value;

    if (password !== confirmPassword) {
        // Hiển thị thông báo lỗi nếu mật khẩu không khớp
        document.getElementById('error-message').style.display = 'block';
        return false; // Ngăn không cho form gửi đi
    } else {
        // Ẩn thông báo lỗi nếu mật khẩu khớp
        document.getElementById('error-message').style.display = 'none';
        return true; // Cho phép form gửi đi
    }
}