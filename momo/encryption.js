const crypto = require('crypto');
const NodeRSA = require('node-rsa');

const secretKey = 'um76xDBeRmmj5kVMhXiCeFKixZTTlmZb';
const iv = Buffer.alloc(16, 0); // IV with 16 bytes of zero

function encryptAES(data) {
    const cipher = crypto.createCipheriv('aes-256-cbc', secretKey, iv);
    const encrypted = Buffer.concat([cipher.update(data), cipher.final()]);
    return encrypted.toString('base64');
}

function decryptAES(encryptedData) {
    const decipher = crypto.createDecipheriv('aes-256-cbc', secretKey, iv);
    const decrypted = Buffer.concat([decipher.update(Buffer.from(encryptedData, 'base64')), decipher.final()]);
    return decrypted.toString();
}

function encryptRSA(jsonData, pubKey) {
    const key = new NodeRSA(pubKey, { encryptionScheme: 'pkcs1' });
    return key.encrypt(JSON.stringify(jsonData), 'base64');
}

module.exports = { encryptAES, decryptAES, encryptRSA };
