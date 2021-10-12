const express = require('express');
const http = require('http');
const path = require('path');
const fs = require('fs/promises');
const {pool,insertData} = require('./DB');
const jwt = require('jsonwebtoken');

const key = require('./secretKey');;

const app = express();


app.use(express.json()); //이 녀석은 요청을 json으로 변환해주는 역할을 함.

//app이 요청이 왔을때 응답을 해주는 함수
const server = http.createServer(app);

app.get("/", (req,res) => {
    res.json({msg:"메인페이지입니다."});
});

app.get("/hello", (req,res) => {
    res.json({msg:"헬로페이지입니다."});
});
app.get("/image", (req,res) => {
    let filename = req.query.file;
    let filePath = path.join(__dirname,"images",filename);
    res.sendFile(filePath);
});

app.get("/record", (req,res) => {
    res.json({msg:"당신의 기록 메시지로 대체되었다"});
});

app.get("/fileList", async (req, res) => {
    const files = await fs.readdir(path.join(__dirname,"images"));
    res.json({msg:"load success", list:files});
});
app.get("/thumb", async (req,res) =>{
    let filename = req.query.file;
    let filePath = path.join(__dirname, "thumbnails", filename);

    res.sendFile(filePath);
});

app.post("/postdata",async (req,res) => {
    let {name, msg, score} = req.body;
    let result = await insertData(name,msg,score);

    if(result) {
        res.json({msg:"기록완료"});
    }
    else {
        res.json({msg:"기록중 오류 발생"});
    }
});
app.get("/list", async (req,res) => {
    let auth = req.header["Authorization"];
    if(auth === undefined) {
        res.json({result:false, payload:"로그인하세요"});
        return;
    }
    auth = auth.split(" ");
    let token = auth[1];

    if(token === undefined) {
        res.json({result:false, payload:"잘못된 토큰입니다"});
        return;
    }

    const decode = jwt.verify(token,key); //해당 토큰이 해당키로 암호화 되었는지 체크
    if(decode) {
        let sql = `SELECT * FROM high_scores ORDER BY score DESC LIMIT 0, 10`;
        let [list] = await pool.query(sql);
    
        res.json({result:true, payload:{list, count:list.length}});
    }
    else {
        res.json({result:false, payload:"잘못된 토큰입니다"});
    }
    
});
app.post("/register", async (req,res) => {
    let {name,id,password} = req.body;

    let sql = `SELECT * FROM \`user\` WHERE id = ?`; //작성해서 입력한 id의 회원이 존재하는지 먼저 검사
    let [result] = await pool.query(sql,[id]);

    if(result.length > 0) {
        res.json({result:false,payload:"중복된 회원이 존재합니다"});
        return;
    }
    //존재하면 res.json({result:false, msg:"중복된 회원이 존재합니다"}); return;
    //그렇지 ㅇ낳다면 아래의 로직 실행

    sql = `INSERT INTO user (id, name, password) VALUES (?,?,PASSWORD(?))`;

    await pool.query(sql,[id,name,password]);

    res.json({result:true, payload:"성공적으로 회원가입"});
});
app.post("/login", async (req,res) => {
    let {id,password} = req.body;
    let sql = `SELECT * FROM \`user\` WHERE id = ? AND password = PASSWORD(?)`; //작성해서 입력한 id의 회원이 존재하는지 먼저 검사
    const [row] = await pool.query(sql,[id,password]);

    if(row.length > 0) {
        let {code,id,name} = row[0];
        //회원이 존재한다면
        const token = jwt.sign({code,id,name},key, {
            expiresIn:'30 days'
        });
        //console.log(token);
        res.json({result:true,paylaod:token});
    }
    else {
        //회원이 존재하지 않는다면
        res.json({result:false, payload:"존재하지 않는 회원입니다"});
    }
});
    
server.listen(54000, () => {
    console.log("서버가 54000번 포트에서 구동중입니다");
});