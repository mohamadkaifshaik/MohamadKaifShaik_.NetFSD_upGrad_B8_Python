let text = "  Hello World  ";

console.log("given:", text);

// length
console.log("Length:", text.length);

// trim
let trimmed = text.trim();
console.log("trim:", trimmed);

// toUpperCase
console.log("Uppercase:", trimmed.toUpperCase());

// toLowerCase
console.log("Lowercase:", trimmed.toLowerCase());

// includes
console.log("Includes 'World':", trimmed.includes("World"));

// startsWith
console.log("Starts with 'Hello':", trimmed.startsWith("Hello"));

// endsWith
console.log("Ends with 'World':", trimmed.endsWith("World"));

// indexOf
console.log("Index of 'World':", trimmed.indexOf("World"));

// slice
console.log("Slice (0,5):", trimmed.slice(0, 5));

// substring
console.log("Substring (6,11):", trimmed.substring(6, 11));

// replace
console.log("Replace World with JS:", trimmed.replace("World", "JS"));

// split
let words = trimmed.split(" ");
console.log("Split into array:", words);

// charAt
console.log("Character at 1:", trimmed.charAt(1));

// concat
let newText = trimmed.concat(" !!!");
console.log("concat:", newText);

// repeat
console.log("Repeat 2 times:", trimmed.repeat(2));
