import { ITodo } from './../domain/ITodo';
export class AppState {
    private _todos: readonly ITodo[] = [];

    getTodos = (): readonly ITodo[] => [...this._todos];

    addTodo = (description: string): readonly ITodo[] =>
        this._todos = [...this._todos, { description: description, done: false }]

    removeTodo = (elementNo: number): readonly ITodo[] =>
        this._todos = [...this._todos.slice(0, elementNo), ...this._todos.slice(elementNo + 1)];

    countTodos = (): number => this._todos.length;

}
