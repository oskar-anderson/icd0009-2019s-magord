const Player1 = {
    name: "John",
    tokenCount: "12"

}

const Player2 = {
    name: "Mary",
    tokenCount: "12"
}

const grid = () => Array.from(document.getElementsByClassName('q'));

const qNumId = (qEl) => Number.parseInt(qEl.id.replace('q', ''));
const emptyQs = () => grid().filter(_qEl => _qEl.innerText === '');
const allSame = (arr) => arr.every(_qEl => _qEl.innerText === arr[0].innerText && _qEl.innerText !== '');

function takeTurn(index, letter) {
    grid()[index].innerText = letter;
    Player1.tokenCount -= 1;
    document.getElementById('player1Token').innerHTML = Player1.tokenCount;
    document.getElementById('player2Token').innerHTML = Player2.tokenCount;
}

const AiopponentChoice = () => qNumId(emptyQs()[Math.floor(Math.random() * emptyQs().length)]);

const endGame = () => {alert('Game over!')}

const checkForVictory = () => {
    let victory = false;


    // If opponent has less than 3 pieces left victory = true

    return victory;
}

const AiOpponentTurn = () => {
    disableListeners();
    setTimeout(() => {
        takeTurn(AiopponentChoice(), 'o');
        Player2.tokenCount -=1
        if(!checkForVictory())
            enableListeners();
    }, 1000);
}

const clickFn = (event) => {
    takeTurn(qNumId(event.target), 'x');
    if(!checkForVictory())
        AiOpponentTurn();
}

function enableListeners() {
    grid().forEach(_qEl => _qEl.addEventListener('click', clickFn));
    document.getElementById('1player').addEventListener('click', startPlayerOne);
    document.getElementById('2player').addEventListener('click', startPlayerTwo);
    document.getElementById('0player').addEventListener('click', startPlayerZero);
}

function disableListeners() {
    grid().forEach(_qEl => _qEl.removeEventListener('click', clickFn));
    document.getElementById('1player').removeEventListener('click', startPlayerOne);
    document.getElementById('2player').removeEventListener('click', startPlayerTwo);
    document.getElementById('0player').removeEventListener('click', startPlayerZero);
}

function startPlayerOne() {
    alert("123")
}

function startPlayerTwo() {
    alert("123")
}

function startPlayerZero() {
    alert("123")
}


enableListeners();