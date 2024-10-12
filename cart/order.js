// orders.js
const express = require('express');
const router = express.Router();

// Giả lập cơ sở dữ liệu đơn hàng
const orders = [
    { id: '123', status: 'Đang xử lý', estimatedDelivery: '2024-10-15' },
    { id: '456', status: 'Đã giao', estimatedDelivery: '2024-10-10' },
    { id: '789', status: 'Đang giao', estimatedDelivery: '2024-10-12' },
];

// Lấy thông tin đơn hàng theo ID
router.get('/:id', (req, res) => {
    const orderId = req.params.id;
    const order = orders.find(o => o.id === orderId);
    
    if (order) {
        res.json(order);
    } else {
        res.status(404).json({ message: 'Không tìm thấy đơn hàng.' });
    }
});

module.exports = router;
