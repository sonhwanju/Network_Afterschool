const express = require('express');
const http = require('http');
const path = require('path');
const fs = require('fs/promises');
const {pool,insertData} = require('./DB');

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
    let {affectedRows} = insertData(name,msg,score);
    console.log(affectedRows);
    if(affectedRows == 1) {
        res.json({msg:"기록완료"});
    }
    else {
        res.json({msg:"기록실패"});
    }
});

server.listen(54000, () => {
    console.log("서버가 54000번 포트에서 구동중입니다");
});