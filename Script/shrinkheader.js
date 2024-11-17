window.addEventListener('scroll', function() {
    const header = document.querySelector('.header');
    if (window.scrollY > 50) { // Ngưỡng cuộn để thu nhỏ header
        header.classList.add('shrink');
    } else {
        header.classList.remove('shrink');
    }
});