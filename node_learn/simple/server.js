// console.log("Hello Node.js"); //Debug.Log();

// let word = "Sum is ";

// let a = 10;
// let b = 20;

// function add(a,b) {
//     let sum = 0;
//     for(let i = 0; i < arguments.length; i++) {
//         sum += arguments[i];
//     }
//     return sum;
// }

// let my =  () => {
//     let sum = 0;
//     for(let i = 0; i < arguments.length; i++) {
//         sum += arguments[i];
//     }
//     return sum;
// }
// let my2 = (a,b) => a + b;

// console.log(my(10, 20,30,40,50));

//java, c#, c 강형(string type) 언어
//js, python, php 약형(weak type) 언어 => TypeScript

// let arr = [1,2,3,4,5,6,7,8,9,10];

// let newArr = arr.filter(x => x % 2 == 0);
// console.log(newArr);

// let obj = {
//     name:"ㅈ승환",
//     age:"18"
// };

// console.log(obj);

// let users = [
//     { id: 11, name: 'Adam', age: 23, group: 'editor' },
//     { id: 47, name: 'John', age: 28, group: 'admin' },
//     { id: 85, name: 'William', age: 34, group: 'editor' },
//     { id: 97, name: 'Oliver', age: 28, group: 'admin' }
// ];

// let a = users.map(x => ({...x,category: (x.age >= 25) ? "어른" : "덜 어른"}));

// console.log(a);

let a  = {
    name:"경기게임마이스터고등학교",
    openYear:2020,
    circle:[{code:1,name:"GMGM"}, {code:2,name:"게임기획"}, {code:3,name:"픽셀웨일"}],
    circleGrade: {
        1:[{name:"최희진",grade:"A"},{name:"서동연",grade:"B"},{name:"이태영",grade:"C"}],
        2:[{name:"유지호",grade:"A"},{name:"임동하",grade:"C"}],
        3:[{name:"서선호",grade:"A"},{name:"강기호",grade:"B"}]
    },
    teacherList: ["최선한","김은정","이하은"]
};

function add(circle, name, grade) {
    let c = a.circle.find(x => x.name == circle);
    a.circleGrade[c.code].push({name:name,grade:grade});
    
}
add("GMGM","한우엽","A");
add("픽셀웨일","김경혁","B");
add("게임기획","김태현","A");

let str = JSON.stringify(a);
console.log(str);
// console.log(a);
// console.log(a.circleGrade);
