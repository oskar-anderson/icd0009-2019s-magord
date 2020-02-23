export function sayHi(user){
    console.log(`Hi, ${user}!`);
}

export function sayHo(user){
    console.log(`Ho, ${user}!`);
}

export let months = ['jan', 'feb'];

//Can also declare functions and at the end export them 

export default class Person {
    constructor(name) {
        this.name = name;
    }
}