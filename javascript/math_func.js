let number = 4.7;
let negative = -10;

console.log("given:", number);

// Math.round
console.log("Round:", Math.round(number));

// Math.floor
console.log("Floor:", Math.floor(number));

// Math.ceil
console.log("Ceil:", Math.ceil(number));

// Math.trunc
console.log("Trunc:", Math.trunc(number));

// Math.abs
console.log("Absolute:", Math.abs(negative));

// Math.pow
console.log("Power (2^3):", Math.pow(2, 3));

// Math.sqrt
console.log("Square root of 16:", Math.sqrt(16));

// Math.cbrt
console.log("Cube root of 27:", Math.cbrt(27));

// Math.max
console.log("Max:", Math.max(1, 5, 3));

// Math.min
console.log("Min:", Math.min(1, 5, 3));

// Math.random
console.log("Random number (0-1):", Math.random());

// Random number between 1 and 10
let randomNumber = Math.floor(Math.random() * 10) + 1;
console.log("Random number (1-10):", randomNumber);

// Math.sign
console.log("Sign of -10:", Math.sign(negative));

// Math.log
console.log("Natural log of 10:", Math.log(10));

// Math.PI
console.log("Value of PI:", Math.PI);
