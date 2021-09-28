let secret = require('./secret');

const mysql = require('mysql2');

const pool = mysql.createPool(secret);
const promisePool = pool.promise(); //프로미스 기반의 풀을 만듦

async function insertData(name, msg, score) {
    let sql = `INSERT INTO high_scores (name,msg,score) VALUES (?,?,?)`;
    let result = await promisePool.query(sql,[name,msg,score]);

    console.log(result[0]);

    return result[0];
}

module.exports = {
    pool:promisePool,
    insertData
};