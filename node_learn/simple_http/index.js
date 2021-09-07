//설치하지 않아도 쓸 수잇는 기본 내장 모듈 존재
const http = require('http');

//파일경로를 조립해줄 수 있는 모듈, 파일에 접근할수 있는 모듇
const fs = require('fs');
const path = require('path');

const server = http.createServer(function(req,res) {
    // console.log(req.url,req.method);

    switch(req.url) {
        case "/":
            res.writeHead(200, {"Content-Type":"application/json"});
            res.end(JSON.stringify({msg:"Hello web server"}));
            break;
        case "/image":
            //__dirname 은 현재 폴더를 나타내는 상수
            let filePath = path.join(__dirname, "Images","youandme.png");
            //파일 시스템에서 해당 파일의 정보를 가져온다
            let fileStat = fs.statSync(filePath);
            res.writeHead(200, {"Content-Type":"image/png", "Content-Length": fileStat.size});

            let readStream = fs.createReadStream(filePath);
            readStream.pipe(res);
            break;
    }  
    
});

//일반적인 서버랑 다르게 웹은, 요청에 의한 응답만을 함
server.listen(52000,function() {
    console.log("Server is running on 52000");
});