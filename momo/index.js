const sendMoMoRequest = require('./momoRequest');
const { encryptAES, decryptAES } = require('./encryption');

const orderInfo = 'Thanh toán MoMo';
const amount = '50000';

sendMoMoRequest(orderInfo, amount);

// Ví dụ mã hóa và giải mã AES
const encryptedToken = encryptAES('Hello MoMo');
console.log('Encrypted Token:', encryptedToken);
const decryptedToken = decryptAES(encryptedToken);
console.log('Decrypted Token:', decryptedToken);
