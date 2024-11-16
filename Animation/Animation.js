// JavaScript để thêm class 'visible' khi trang tải xong
window.addEventListener('load', function() {
    document.querySelector('.container__intro').classList.add('visible');
    document.querySelector('.container__wellcome').classList.add('visible');
    document.querySelector('.container__infor').classList.add('visible');
});

document.addEventListener('DOMContentLoaded', function() {
    // Lắng nghe sự kiện cuộn trang
    window.addEventListener('scroll', function() {
        const elements = document.querySelectorAll('.container__col-item'); // Lấy tất cả các phần tử sản phẩm
        elements.forEach(element => {
            const rect = element.getBoundingClientRect(); // Lấy vị trí của phần tử trong viewport
            if (rect.top <= window.innerHeight && rect.bottom >= 0) {
                // Nếu phần tử nằm trong vùng nhìn thấy của màn hình
                element.classList.add('visible'); // Thêm class 'visible' để kích hoạt hiệu ứng
            }
        });
    });
});