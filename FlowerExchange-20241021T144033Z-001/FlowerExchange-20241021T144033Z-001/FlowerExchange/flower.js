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
document.getElementById('signin').addEventListener('submit', async (e) => {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const response = await fetch('https://localhost:44315/api/Auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password }),
    });

    const data = await response.json();
    if (response.ok) {
        if (data.token) {
            localStorage.setItem('authToken', data.token);
            alert("Login successful");
        } else {
            alert("Login failed");
        }
    } else {
        alert(data.message || "Login failed");
    }
});

 // JavaScript để điều khiển việc hiển thị các phần
 function showSection(sectionId, element) {
    // Ẩn tất cả các phần
    var sections = document.getElementsByClassName('section');
    for (var i = 0; i < sections.length; i++) {
        sections[i].style.display = 'none';
    }

    // Hiển thị phần đã chọn
    document.getElementById(sectionId).style.display = 'block';

    // Xóa lớp active khỏi tất cả các mục
    var navItems = document.querySelectorAll('.sidebar nav ul li');
    navItems.forEach(item => {
        item.classList.remove('active');
    });

    // Thêm lớp active cho mục đang được chọn
    element.classList.add('active');
}

document.getElementById('fileInput').addEventListener('change', function(event) {
    const file = event.target.files[0]; // Lấy tệp đã chọn
    const reader = new FileReader(); // Tạo FileReader để đọc tệp

    reader.onload = function(e) {
        document.getElementById('profileImage').src = e.target.result; // Cập nhật ảnh đại diện
    };

    if (file) {
        reader.readAsDataURL(file); // Đọc tệp dưới dạng URL dữ liệu
    }
});