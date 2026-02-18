let num = 121;
let real = num;
let rev = 0;
while (real > 0) {
  rev = rev * 10 + (real % 10);
  real = parseInt(real / 10);
}
if (num == rev) {
  console.log(num + " is a palindrome");
} else {
  console.log(num + " is not a palindrome");
}
