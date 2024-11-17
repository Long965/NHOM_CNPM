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

    // Xử lý sự kiện submit form đăng ký
    document.getElementById('registerForm')?.addEventListener('submit', async (e) => {
        e.preventDefault();

        const fullName = document.getElementById('fullName')?.value || '';
        const email = document.getElementById('email')?.value || '';
        const password = document.getElementById('password')?.value || '';
        const role = document.querySelector('input[name="role"]:checked')?.value || '';

        if (!validatePassword()) {
            return;
        }

        try {
            const response = await fetch('https://localhost:44344/api/User/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ fullName, email, password, role })
            });

            const data = await response.json();
            document.getElementById('registerMessage').innerText = response.ok
                ? 'Đăng ký thành công!'
                : 'Đăng ký thất bại: ' + (data.message || 'Lỗi không xác định.');

        } catch (error) {
            alert('Đăng ký thành công!');
        }
    });

document.getElementById('signin').addEventListener('submit', async (e) => {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const response = await fetch('https://localhost:44344/api/User/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password }),
    });

    const data = await response.json();
    if (response.ok) {
        if (data.token) {
            localStorage.setItem('authToken', data.token);
            alert("Login successful");
            window.location.href = 'HomeAfterToLogin.html'; // Thay 'TrangChu.html' bằng đường dẫn đến trang chủ của bạn.
        } else {
            alert("Login failed");
        }
    } else {
        alert(data.message || "Login failed");
    }
});

document.getElementById('updateProfile').addEventListener('submit', async (e) => {
    e.preventDefault();

    const phoneNumber = document.getElementById('phone').value;
    const gender = document.getElementById('gender').value;
    const dateOfBirth = document.getElementById('birthday').value;
    const address = document.getElementById('address').value;
    const token = localStorage.getItem('authToken');

    if (!token) {
        alert("Please log in before updating your profile.");
        return;
    }

    const requestData = { phoneNumber, gender, dateOfBirth, address };

    try {
        const response = await fetch('https://localhost:44344/api/UserProfile/update-profile', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
            },
            body: JSON.stringify(requestData),
        });

        const data = await response.json();
        if (response.ok) {
            alert("Profile updated successfully!");
        } else {
            alert(data.message || "Failed to update profile.");
        }
    } catch (error) {
        console.error("Error:", error);
        alert("An error occurred while updating your profile.");
    }
});

document.getElementById('changePasswordButton').addEventListener('click', async () => {
    const oldPassword = document.getElementById('password').value;
    const newPassword = document.getElementById('passwordnew').value;
    const confirmPassword = document.getElementById('confirm_password').value;

    // Kiểm tra xác nhận mật khẩu
    if (newPassword !== confirmPassword) {
        alert("Mật khẩu mới và xác nhận mật khẩu không khớp.");
        return;
    }

    // Lấy token từ localStorage
    const token = localStorage.getItem('authToken');
    if (!token) {
        alert("Vui lòng đăng nhập trước khi thay đổi mật khẩu.");
        return;
    }

    const requestData = {
        oldPassword,
        newPassword,
    };

    try {
        console.log("Đang gửi yêu cầu đổi mật khẩu...");

        // Gửi yêu cầu tới API
        const response = await fetch('https://localhost:44344/api/Account/ChangePassword', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
            },
            body: JSON.stringify(requestData),
        });

        const data = await response.json();

        if (response.ok) {
            alert("Đổi mật khẩu thành công!");
            document.getElementById('password').value = '';
            document.getElementById('passwordnew').value = '';
            document.getElementById('confirm_password').value = '';
        } else {
            console.error(data.message || "Đổi mật khẩu thất bại.");
            alert(data.message || "Đổi mật khẩu thất bại.");
        }
    } catch (error) {
        console.error("Lỗi:", error);
        alert("Đã xảy ra lỗi khi gửi yêu cầu đổi mật khẩu.");
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