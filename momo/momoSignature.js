const crypto = require('crypto');
const { accessKey, secretKey } = require('./momoConfig');

function generateSignature(data) {
    const rawSignature = `accessKey=${data.accessKey}&amount=${data.amount}&extraData=${data.extraData}&ipnUrl=${data.ipnUrl}&orderId=${data.orderId}&orderInfo=${data.orderInfo}&partnerCode=${data.partnerCode}&redirectUrl=${data.redirectUrl}&requestId=${data.requestId}&requestType=${data.requestType}`;
    return crypto.createHmac('sha256', secretKey).update(rawSignature).digest('hex');
}

module.exports = generateSignature;
