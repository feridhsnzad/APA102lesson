// 1.Verilmis arrayde tekrarlanan reqemleri silmek ve tekrar reqemlerin sayini gostermek.
// function analyzeArray(arr) {
//     let keys = [];       
//     let values = [];     
//     let unique = [];     
//     for (let i = 0; i < arr.length; i++) {
//         let num = arr[i];
//         let index = -1;
//         for (let j = 0; j < keys.length; j++) {
//             if (keys[j] === num) {
//                 index = j;
//                 break;
//             }
//         }
//         if (index === -1) {
//             keys[keys.length] = num;
//             values[values.length] = 1;
//         } else {  
//             values[index] = values[index] + 1;
//         }
//     }
//     for (let i = 0; i < keys.length; i++) {
//         if (values[i] === 1) {
//             unique[unique.length] = keys[i];
//         }
//     }
//     return {
//         uniqueNumbers: unique,
//         keys: keys,
//         counts: values
//     };
// }
// let arr = [43,13,22,43,56,22,43];
// let result = analyzeArray(arr);
// console.log("Təkrarsız rəqəmlər:", result.uniqueNumbers);
// console.log("Bütün rəqəmlər:", result.keys);
// console.log("Hər rəqəmin sayı:", result.counts);
// -----------------------------------------------------------------------------
// Verilmis sozun polindrom olub olmadığını yoxlayan alqoritm yazmaq.

// function isPalindrome(word) {
//     for (let i = 0; i < word.length / 2; i++) {
//         let left = word[i];
//         let right = word[word.length - 1 - i];

//         if (left !== right) {
//             return false; 
//         }
//     }
//     return true; 
// }


// console.log(isPalindrome("ana"));   // true
// console.log(isPalindrome("level")); // true
// console.log(isPalindrome("salam")); // false
// -----------------------------------------------------------------------------
// Girilen ededin verilmis arreyde nece elementden kicik oldugunu yazan alqoritim.
// function countSmaller(arr, number) {
//     let count = 0;  

//     for (let i = 0; i < arr.length; i++) {
//         if (number < arr[i]) {
//             count++;
//         }
//     }

//     return count;
// }


// let arr = [5, 10, 3, 8, 15, 1];
// let input = 7;

// console.log(countSmaller(arr, input));  
// -----------------------------------------------------------------------------
// .Daxil edilen ededin Aboundant ve ya Deficient oldugunu yoxlayan algorithm.
// (Abundant ədəd öz müsbət bolenlerinin(ozunden basqa) cəmi özündən böyük olan
//  müsbət tam ədədlərə deyilir. Eks halda Deficient eded olur. 12-Aboundant, 13- Deficient)

// function checkNumber(num) {
//     let sum = 0;

   
//     for (let i = 1; i < num; i++) {
//         if (num % i === 0) {
//             sum += i;
//         }
//     }

   
//     if (sum > num) {
//         return "Abundant";
//     } else {
//         return "Deficient";
//     }
// }


// console.log(checkNumber(12)); 
// console.log(checkNumber(13)); 
// console.log(checkNumber(6));  
// console.log(checkNumber(18)); 

// -----------------------------------------------------------------------------
// Array-in bütün elementlərini kvadrata yüksəldib yeni array qaytaran funksiya yazın.

// function squareArray(arr) {
//     let result = [];

//     for (let i = 0; i < arr.length; i++) {
//         result[i] = arr[i] * arr[i];
//     }

//     return result;
// }


// console.log(squareArray([1, 2, 3, 4])); 

// -----------------------------------------------------------------------------