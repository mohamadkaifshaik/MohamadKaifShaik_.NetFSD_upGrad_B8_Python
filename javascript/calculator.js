let num1 = 10;
let num2 = 20;
let operator = "-";

let res;

if (operator == "+") {
  res = num1 + num2;
} else if (operator == "-") {
  res = num1 - num2;
} else if (operator == "*") {
  res = num1 * num2;
} else if (operator == "/") {
  res = num1 / num2;
} else {
  res = "Invalid operator";
}
console.log("Result: " + res);
