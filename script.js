// Tắt/Bật hiển thị mật khẩu
document.querySelectorAll('#togglePassword').forEach(toggle => {
    toggle.addEventListener('change', () => {
        const passwordInputs = document.querySelectorAll('input[type="password"], input[type="text"]');
        passwordInputs.forEach(input => {
            input.type = input.type === 'password' ? 'text' : 'password';
        });
    });
});

// Xử lý đăng nhập
document.getElementById('loginForm')?.addEventListener('submit', async (e) => {
    e.preventDefault();

    const response = await fetch('https://localhost:44315/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email: document.getElementById('email').value,
            password: document.getElementById('password').value
        })
    });

    const data = await response.json();
    if (response.ok) {
        document.getElementById('loginMessage').innerText = 'Đăng nhập thành công!';
        
        // Chuyển hướng tới trang chủ
        window.location.href = 'HomeAfterToLogin.html'; // Thay 'TrangChu.html' bằng đường dẫn đến trang chủ của bạn.
    } else {
        document.getElementById('loginMessage').innerText = 'Đăng nhập thất bại: ' + data.message;
    }
});

// Xử lý đăng ký
document.getElementById('registerForm')?.addEventListener('submit', async (e) => {
    e.preventDefault();

    const response = await fetch('https://localhost:44315/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            fullName: document.getElementById('fullName').value,
            email: document.getElementById('email').value,
            password: document.getElementById('password').value,
            role: document.getElementById('role').value
        })
    });

    const data = await response.json();
    document.getElementById('registerMessage').innerText = response.ok ? 'Đăng ký thành công!' : 'Đăng ký thất bại: ' + data.message;
});

