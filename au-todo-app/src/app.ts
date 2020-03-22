import { EventChannels } from './types/EventChannels';
import { autoinject } from 'aurelia-framework';
import { EventAggregator, Subscription } from 'aurelia-event-aggregator';
import { ITodo } from './domain/ITodo';
import { AppState } from 'state/app-state';

@autoinject
export class App {
    private _subscriptions: Subscription[] = [];

    //private _todos: ITodo[] = [];

    private _placeholder = "What do you want to do?";
    private _appTitle = "Aurelia Todos";
    private _submitButtonTitle = "Add";
    private _input = "";

    constructor(private appState: AppState, private eventAggregator: EventAggregator) {
        /*this._subscriptions.push(
            this.eventAggregator.subscribe(
                EventChannels.NewTodoCreation,
                (description: string) => this.eventNewTodoCreation(description))
        );
        */
    }

    detached() {
        this._subscriptions.forEach(subscription => subscription.dispose());
        this._subscriptions = [];
    }

    /*
    eventNewTodoCreation(description: string) {
        this._todos.push({ description: description, done: false });
    }
    */

    removeTodo(index: number) {
        this.appState.removeTodo(index);
        //this._todos.splice(index, 1);
    }

    getDateStr(): string {
        return new Date().getTime().toString();
    }
}
