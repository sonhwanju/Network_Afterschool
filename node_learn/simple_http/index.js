//설치하지 않아도 쓸 수잇는 기본 내장 모듈 존재
const http = require('http');
const server = http.createServer(function(req,res) {
    console.log(req.url,req.method);
    res.end(JSON.stringify({msg:"Hello web server"}));
})

server.listen(52000,function() {
    console.log("Server is running on 52000");
})