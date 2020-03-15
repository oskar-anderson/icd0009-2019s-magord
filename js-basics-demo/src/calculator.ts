export class Calculator {

    private _display: string = "0";

    constructor() {
    }

    handleKey(key: string) {
        let num = Number(key);
        if (!isNaN(num)) {
            this.numberPressed(num);
        }
        return this.display;    
    }

    numberPressed(num: number) {
        if( this.display == "0") {
            this._display = num.toString();
        } else {
            this._display += num.toString();
        }

    }

    get display(){
        return this._display;
    }
}
