// Database configuration - kết nối trực tiếp đến database của game server
// Cập nhật theo nro.sql: database = 'nro onepuch'
module.exports = {
    database: {
        host: 'localhost',
        user: 'root',
        password: '',
        database: 'nro_onepuch',
        port: 3306,
        waitForConnections: true,
        connectionLimit: 10,
        queueLimit: 0
    },
    server: {
        port: 3000,
        sessionSecret: 'nro-admin-secret-key-2024'
    },
    jwt: {
        secret: 'nro-jwt-secret-key-2024',
        expiresIn: '24h'
    }
};
