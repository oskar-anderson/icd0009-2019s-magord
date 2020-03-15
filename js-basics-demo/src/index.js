const Player1 = {
    tokenCount: "12",
    myTurn: false

}

const Player2 = {
    tokenCount: "12",
    myTurn: false
}

const grid = () => Array.from(document.getElementsByClassName('q'));

const qNumId = (qEl) => Number.parseInt(qEl.id.replace('q', ''));

function emptyQs() {
    return grid().filter(_qEl => _qEl.innerText === '');
}

const allSame = (arr) => arr.every(_qEl => _qEl.innerText === arr[0].innerText && _qEl.innerText !== '');


//const AiopponentChoice = () => qNumId(emptyQs()[Math.floor(Math.random() * emptyQs().length)]);

const endGame = () => {alert('Game over!')}

const checkForVictory = () => {
    let victory = false;


    // If opponent has less than 3 pieces left victory = true

    return victory;
}

/*const AiOpponentTurn = () => {
    disableListeners();
    setTimeout(() => {
        takeTurn(AiopponentChoice(), 'o');
        Player2.tokenCount -=1
        grid().forEach(_qEl => _qEl.addEventListener('click', clickFn));
        if(!checkForVictory())
            enableListeners();
    }, 1000);
}
*/


function toggleTurn() {
    if(Player1.myTurn == false) {
        Player1.myTurn = true;
    }
    else {
        Player1.myTurn = false;
    }
}

function clickFn(event) {
    if (!(emptyQs().includes(event.target))) {
        document.getElementsByClassName('q').removeEventListener('click', clickFn());
    }
    if (Player1.myTurn == true) {
        player1TakeTurn(qNumId(event.target), 'x');
    } else{
        player2TakeTurn(qNumId(event.target), 'o');
    }
    //if(!checkForVictory())
        //AiOpponentTurn();
}

function player1TakeTurn(index, letter) {
    grid()[index].innerText = letter;
    Player1.tokenCount -= 1;
    toggleTurn();
    displayWhosTurn();
    displayGamePhase();
    displayTokenCounts();
}

function player2TakeTurn(index, letter) {
    grid()[index].innerText = letter;
    Player2.tokenCount -= 1;
    toggleTurn();
    displayWhosTurn();
    displayGamePhase();
    displayTokenCounts();
}

function enableListeners() {
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
    grid().forEach(_qEl => _qEl.addEventListener('click', clickFn));
}

function startPlayerTwo() {
    grid().forEach(_qEl => _qEl.innerText = '');
    Player1.tokenCount = 12;
    Player2.tokenCount = 12;
    displayTokenCounts();
    chooseWhoStarts();
    grid().forEach(_qEl => _qEl.addEventListener('click', clickFn));
}

function startPlayerZero() {
    grid().forEach(_qEl => _qEl.addEventListener('click', clickFn));
}

function chooseWhoStarts() {
    const rd1 = document.getElementById("rd1");
    const rd2 = document.getElementById("rd2");

    if(rd1.checked == true) {
        Player1.myTurn = true;
    } else if(rd2.checked == true) {
        Player2.myTurn = true;
    }
    displayWhosTurn();
    displayGamePhase();
}

function displayWhosTurn() {
    if (Player1.myTurn == true) {
        document.getElementById('whosTurn').innerHTML = "Player 1 turn!";
    } else {
        document.getElementById('whosTurn').innerHTML = "Player 2 turn!";
    }
}

function displayTokenCounts() {
    document.getElementById('player1Token').innerHTML = Player1.tokenCount;
    document.getElementById('player2Token').innerHTML = Player2.tokenCount;
}

displayTokenCounts();
enableListeners();

//  PHASE2 

function displayGamePhase() {
    if (Player1.tokenCount != "0" && Player2.tokenCount != "0") {
        document.getElementById("gamePhase").innerHTML = "Phase 1";
    }
    else if(Player1.tokenCount == "0" && Player2.tokenCount == "0") {
        alert("Phase 2 begins!");
        document.getElementById("gamePhase").innerHTML = "Phase 2";
        disableListeners();
        makeElementsDraggable();
    }
}

function makeElementsDraggable() {
    grid().forEach((_qEl) => {
        _qEl.setAttribute('draggable', true),
        _qEl.addEventListener('dragstart', onDragStart),
        _qEl.addEventListener('dragover', onDragOver)
        _qEl.addEventListener('drop', onDrop),
        _qEl.addEventListener('dragend', onDragEnd);
        }
    )
}

let sameQuad = false;

function onDragEnd(event) {
    if (sameQuad == false) {
        event.target.innerText = '';
    }
}

function onDragStart(event) {
    event.dataTransfer.setData('text', event.target.innerText);
  }

function onDragOver(event) {
    event.preventDefault();
}

function onDrop(event) {
    event.preventDefault();
    const data = event.dataTransfer.getData('text');
    if(event.target.innerText != '') {
        sameQuad = true;
        alert("Can't drop here!")
    }
    else {
        sameQuad = false;
        event.target.innerText = data;
        toggleTurn();
    displayWhosTurn();
    }
    event.dataTransfer.clearData();
}