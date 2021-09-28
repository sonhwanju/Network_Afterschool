let connectionData = {
    host:"gondr.asuscomm.com",
    user:"yy_40113",
    password:"1234",
    database:"yy_40113",
    tiemzone:"+09:00"
};

const mysql = require('mysql2');

const pool = mysql.createPool(connectionData);
const promisePool = pool.promise(); //프로미스 기반의 풀을 만듦

async function insertData(name, msg, score) {
    let sql = `INSERT INTO high_scores (name,msg,score) VALUES (?,?,?)`;
    let result = await promisePool.query(sql,[name,msg,score]);

    console.log(result);

    
}
insertData("asd","asd",111);

//insertData("겜마고","내가 만들고 싶은 게임을 만들 수 있는 곳",2);

async function getList() {
    let sql = `SELECT name,msg,score FROM high_scores`;
    let [rows] = await promisePool.query(sql);
    console.log(rows);
}

//getList();

