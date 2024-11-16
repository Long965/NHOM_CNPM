let cart = [];
let total = 0;

function addToCart(productName, productPrice) {
    // Đảm bảo giá sản phẩm là số
    productPrice = parseFloat(productPrice);
    if (isNaN(productPrice)) {
        alert('Giá sản phẩm không hợp lệ!');
        return;
    }
    
    // Kiểm tra xem sản phẩm đã có trong giỏ chưa
    const existingProductIndex = cart.findIndex(item => item.name === productName);
    if (existingProductIndex !== -1) {
        // Nếu sản phẩm đã có trong giỏ, tăng số lượng thay vì thêm mới
        cart[existingProductIndex].quantity++;
    } else {
        cart.push({ name: productName, price: productPrice, quantity: 1 });
    }
    total += productPrice;
    displayCart();
}

function displayCart() {
    const cartDiv = document.getElementById('cart');
    cartDiv.innerHTML = ''; // Xóa giỏ hàng hiện tại
    if (cart.length === 0) {
        cartDiv.innerHTML = '<p>Giỏ hàng của bạn trống.</p>';
    } else {
        cart.forEach((item, index) => {
            cartDiv.innerHTML += `
                <p>${item.name} - ${item.quantity} x ${item.price}đ 
                    <button onclick="removeFromCart(${index})">Xóa</button>
                </p>`;
        });
    }
    document.getElementById('total').innerText = `Tổng: ${total}đ`;
}

function removeFromCart(index) {
    total -= cart[index].price * cart[index].quantity; // Trừ giá trị sản phẩm khỏi tổng
    cart.splice(index, 1); // Xóa sản phẩm khỏi giỏ
    displayCart();
}

function checkout() {
    if (cart.length === 0) {
        alert('Giỏ hàng trống! Vui lòng thêm sản phẩm.');
        return;
    }
    alert('Chuyển đến trang thanh toán.');
    // Chuyển đến trang thanh toán
    // window.location.href = 'payment.html'; // Bỏ dấu comment để chuyển hướng đến trang thanh toán
}
