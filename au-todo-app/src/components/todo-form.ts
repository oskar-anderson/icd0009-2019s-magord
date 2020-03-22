import { bindable, autoinject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { EventChannels } from 'types/EventChannels';
import { AppState } from 'state/app-state';

@autoinject
export class TodoForm {
    @bindable public placeholder = "xxxx";
    @bindable public appTitle = "xxxxxxxxx";
    @bindable public submitButtonTitle = "xxxxx";

    private _input = "";

    constructor(private appState: AppState, private eventAggregator: EventAggregator) {

    }

    formSubmitted(event: Event) {

        if (this._input.length > 0) {
            //this.eventAggregator.publish(EventChannels.NewTodoCreation, this._input);
            this.appState.addTodo(this._input);
        }
        this._input = "";
        event.preventDefault();
    }
}
