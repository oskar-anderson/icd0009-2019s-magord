console.log("Hello from Corona JS!");

interface IState {
    name: string;
    title: string;
    [propName: string]: string;
}

let initialState: IState = {
    name: "Values on",
    title: "load..."
};


let state = new Proxy(initialState, {
    set(target: IState, propertyName: string, value: string) {
        target[propertyName] = value;
        renderUI();
        return true;
    }
});

const renderUI = () => {
    const bindings = Array.from(
        document.querySelectorAll('[data-binding]')
    ).map((elem) => (elem as HTMLElement).dataset.binding!);
    bindings.forEach((binding) => {
        document.querySelector(`[data-binding='${binding}']`)!.innerHTML 
            = state[binding];
        (document.querySelector(`[data-model='${binding}']`)! as HTMLInputElement).value 
            = state[binding];
    });
}

let button = document.querySelector('.js-reset-data') as HTMLAnchorElement;
// attaching directly to on* is not the best practice - only one can be attached
button.onclick = function (){
    // updating the state updates the UI automatically
    state.name = "Updated values";
    state.title = "in state!";
}



const listeners = Array.from(document.querySelectorAll('[data-model]'));
listeners.forEach((listener) => {
    if (listener instanceof HTMLInputElement){
        const name = listener.dataset.model!;
        // this is the recommended approach
        listener.addEventListener('input',(event) => {
            state[name] = listener.value;
        })

    }
});

renderUI();
