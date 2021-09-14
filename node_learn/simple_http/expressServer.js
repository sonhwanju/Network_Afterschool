const express = require('express');
const http = require('http');
const path = require('path');
const fs = require('fs/promises');

const app = express();

//app이 요청이 왔을때 응답을 해주는 함수
const server = http.createServer(app);

app.get("/", (req,res) => {
    res.json({msg:"메인페이지입니다."});
});

app.get("/hello", (req,res) => {
    res.json({msg:"헬로페이지입니다."});
});
app.get("/image", (req,res) => {
    let filePath = path.join(__dirname,"Images","youandme.png");
    res.sendFile(filePath);
});

app.get("/record", (req,res) => {
    res.json({msg:"당신의 기록 메시지로 대체되었다"});
});

app.get("/fileList", async (req, res) => {
    const files = await fs.readdir(path.join(__dirname,"images"));
    res.json({msg:"load success", list:files});
});

server.listen(54000, () => {
    console.log("서버가 54000번 포트에서 구동중입니다");
});