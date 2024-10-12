const https = require('https');
const { partnerCode, accessKey, redirectUrl, ipnUrl, lang } = require('./momoConfig');
const generateSignature = require('./momoSignature');

function sendMoMoRequest(orderInfo, amount) {
    const requestId = partnerCode + new Date().getTime();
    const orderId = requestId;
    const requestType = 'captureWallet';
    const extraData = '';

    const signature = generateSignature({
        accessKey,
        amount,
        extraData,
        ipnUrl,
        orderId,
        orderInfo,
        partnerCode,
        redirectUrl,
        requestId,
        requestType
    });

    const requestBody = JSON.stringify({
        partnerCode,
        accessKey,
        requestId,
        amount,
        orderId,
        orderInfo,
        redirectUrl,
        ipnUrl,
        extraData,
        requestType,
        signature,
        lang,
    });

    const options = {
        hostname: 'test-payment.momo.vn',
        port: 443,
        path: '/v2/gateway/api/create',
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': Buffer.byteLength(requestBody)
        }
    };

    const req = https.request(options, res => {
        console.log(`Status: ${res.statusCode}`);
        res.setEncoding('utf8');
        res.on('data', (body) => {
            console.log('Body: ', body);
        });
    });

    req.on('error', (e) => {
        console.error(`Request error: ${e.message}`);
    });

    req.write(requestBody);
    req.end();
}

module.exports = sendMoMoRequest;
