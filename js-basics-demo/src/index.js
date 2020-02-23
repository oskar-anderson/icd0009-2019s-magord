'use strict';



import {hi, ho} from './hiho'

hi();
ho();




/*import Person from './person';

let x = new Person('Marko');
alert(x.name);
*/





/*
import {sayHi} from './sayHi';
import {sayHo} from './sayHi';

//Can use AS to rename variables/functions

import {sayHi as hi, sayHo as ho} from './sayHi';

import * as xxx from './sayHi';

xxx.sayHi('Marko');
xxx.sayHo('Marko');

sayHi('foo');
*/



/*
try{    
    JSON.parse('oisfjgoisjsiog');
}
catch (e){
    console.log(typeof e)
    console.log(e instanceof SyntaxError);
}
*/



/*
class Rectangle{
    constructor(x, y){
        this.x = x;
        this.y = y;
    }

    getArea(){
        return this.x * this.y;
    }
}

class Square extends Rectangle{
    constructor(x){
        // When a class extends from another - in constructor must always call super first, then can access instance
        super(x,x);
    }
}

alert(new Square(5).getArea());
*/






/*class Person {
    
    constructor(firstName, lastName) {
        this.firstName = firstName
        this.lastName = lastName
    }

    getFullName(){
        return this.firstName + " " + this.lastName;
    }

    get fullName(){
        return this.firstName + " " + this.lastName;
    }

    set fullName(value) {

        this.firstName = value; this.lastName = "-";
    }
}

let x = new Person("Marko", "Gordejev");
x.fullName = "testing";
alert(x.fullName);
*/





/*
//Modifying built in objects
Date.prototype.toEstonian = function () {
    return this.getFullYear() + " aastat!";
}

let d = new Date();
console.log(d.toEstonian())

let x = new Date();
console.log(x.toEstonian())
*/






/*console.log('Hello!');

console.log(Math.PI);

console.log(Object.getOwnPropertyDescriptor(Math, "PI"));

Object.defineProperty(Math, 'PI3', {'value': 3});

console.log(Object.getOwnPropertyDescriptor(Math, 'PI3'));
*/