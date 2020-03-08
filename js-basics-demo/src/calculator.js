export class Calculator {

    constructor() {
        this._display = "0";
    }

    handleKey(key) {
        let num = Number(key);
        if (!isNaN(num)) {
            this._numberPressed(num);
        }
        return this._display;    
    }

    _numberPressed(num) {
        if( this._display === "0") {
            this._display = num.toString();
        } else {
            this._display += num.toString();
        }

    }

    get display(){
        return this._display;
    }

}
