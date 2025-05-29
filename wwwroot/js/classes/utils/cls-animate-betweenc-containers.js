// utils/MathUtils.js (Recommended: Define in a module)
class MathUtils {
  static PI = 3.14159; // Static property

  static add(a, b) {
    return a + b;
  }

  static multiply(a, b) {
    return a * b;
  }

  static getRandomNumber(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
}

// To make it "globally" accessible in a browser environment (less recommended for large projects):
// Assign it to the `window` object
window.MathUtils = MathUtils;

// In a module-based environment (Node.js or modern browser with bundlers):
// export default MathUtils; // For ES Modules
// module.exports = MathUtils; // For CommonJS

console.log(MathUtils.PI); // Output: 3.14159
console.log(MathUtils.add(5, 3)); // Output: 8
console.log(MathUtils.multiply(4, 6)); // Output: 24
console.log(MathUtils.getRandomNumber(1, 10)); // Output: a random number between 1 and 10
