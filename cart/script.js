// script.js
async function trackOrder() {
    const orderDetailsDiv = document.getElementById('orderDetails');
    
    try {
        // Thay '123' bằng mã đơn hàng mặc định hoặc lấy đơn hàng đầu tiên từ backend.
        const response = await fetch(`http://localhost:3000/api/orders/123`);
        if (!response.ok) {
            throw new Error('Không tìm thấy đơn hàng.');
        }
        const order = await response.json();
        orderDetailsDiv.innerHTML = `
            <h2>Thông tin đơn hàng</h2>
            <p><strong>Mã đơn hàng:</strong> ${order.id}</p>
            <p><strong>Trạng thái:</strong> ${order.status}</p>
            <p><strong>Dự kiến giao hàng:</strong> ${order.estimatedDelivery}</p>
        `;
    } catch (error) {
        orderDetailsDiv.innerHTML = `<p>${error.message}</p>`;
    }
}
