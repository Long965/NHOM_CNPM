const express = require('express');
const app = express();

const orders = [
    { id: '123', status: 'Đang giao', estimatedDelivery: '2024-10-15' },
    { id: '456', status: 'Đã giao', estimatedDelivery: '2024-10-10' },
    // Thêm nhiều đơn hàng khác nếu cần
];

app.get('/api/orders/:id', (req, res) => {
    const orderId = req.params.id;
    const order = orders.find(o => o.id === orderId);
    if (order) {
        res.json(order);
    } else {
        res.status(404).send('Không tìm thấy đơn hàng.');
    }
});

app.listen(3000, () => {
    console.log('Server đang chạy trên cổng 3000');
});
