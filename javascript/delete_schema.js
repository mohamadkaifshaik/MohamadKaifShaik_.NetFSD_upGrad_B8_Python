// ES6 MAP key/value pair
const map = new Map();
const map1 = new Map();
//console.log(map);
// insert data
map.set("table", { name: "deepti", job: "trainer" });
//map.delete('table1');
//console.log(map);
// console.log(map.get('table'));
// console.log(map.delete(deepti));
console.log(map);
map1.set("FName", "Rahul");
map1.set("LName", "singh");
for (let [key, value] of map1) {
  console.log(key + "--" + value);
}

// delete map.get("table").name;

const obj = map.get("table");
delete obj.name;
console.log(map);

//function
function print(name, function1) {
  console.log("hi how are you");
  function1(name);
}
//callback function
function call(name) {
  console.log("hello" + "" + name);
}
setTimeout(print, 2000, "deepti", call);
//print('deepti');

//async and await
async function A() {
  console.log("I m async fnction");
  return Promise.resolve(1);
}
//A();
A().then(function (result) {
  console.log(result);
});
