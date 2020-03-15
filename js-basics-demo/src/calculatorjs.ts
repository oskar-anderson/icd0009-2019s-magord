import {Calculator} from './calculator';

let calcBrain = new Calculator();

let calculator = document.querySelector('#calculator')!;
let display = calculator.querySelector('.js-display')!;
let numberButtons = calculator.querySelectorAll('.js-number')

// Set the initial display value
display.innerHTML = calcBrain.display;

function numberPressed(this: GlobalEventHandlers, event: MouseEvent) {
    if (this instanceof HTMLAnchorElement) {
        let key = this.dataset.value!;
        display.innerHTML = calcBrain.handleKey(key);
    } else {
        console.error("Bad element type for this! - ", JSON.stringify(this));
    }
}

for (const numberButton of numberButtons) {
    (numberButton as HTMLAnchorElement).onclick = numberPressed
}