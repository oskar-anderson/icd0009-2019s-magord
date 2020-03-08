import {Calculator} from './calculator.js';

let calcBrain = new Calculator();

let calculator = document.querySelector('#calculator');
let display = calculator.querySelector('.js-display');
let numberButtons = calculator.querySelectorAll('.js-number')

// Set the initial display value
display.innerHTML = calcBrain.display;

function numberPressed(event) {
    let key = this.dataset.value;

    display.innerHTML = calcBrain.handleKey(key);
}

for (const numberButton of numberButtons) {
    numberButton.onclick = numberPressed
}