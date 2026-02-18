let numbers = [1, 2, 3];
let extra = [5, 6];
let unsorted = [3, 1, 4, 2];

console.log("given:", numbers);

// push
numbers.push(4);
console.log("push:", numbers);

// pop
numbers.pop();
console.log("pop:", numbers);

// shift
numbers.shift();
console.log("shift:", numbers);

// unshift
numbers.unshift(0);
console.log("unshift:", numbers);

// concat
let combined = numbers.concat(extra);
console.log("concat:", combined);

// join
let joined = combined.join("-");
console.log("join:", joined);

// map
let doubled = combined.map((num) => num * 2);
console.log("map:", doubled);

// filter
let even = combined.filter((num) => num % 2 === 0);
console.log("filter:", even);

// reduce
let sum = combined.reduce((total, num) => total + num, 0);
console.log("reduce (sum):", sum);

// forEach
console.log("forEach:");
combined.forEach((num) => console.log(num));

// sort
unsorted.sort();
console.log("sort:", unsorted);

// reverse
unsorted.reverse();
console.log("reverse:", unsorted);
