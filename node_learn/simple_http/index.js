//설치하지 않아도 쓸 수잇는 기본 내장 모듈 존재
const http = require('http');
const server = http.createServer(function(req,res) {
    // console.log(req.url,req.method);
    res.writeHead(200, {"Content-Type":"application/json"});
    res.end(JSON.stringify({msg:"Hello web server"}));
});

//일반적인 서버랑 다르게 웹은, 요청에 의한 응답만을 함
server.listen(52000,function() {
    console.log("Server is running on 52000");
});