let tColorA = document.getElementById('tColorA'),
    tColorB = document.getElementById('tColorB'),
    tColorC = document.getElementById('tColorC'),
    iconA = document.querySelector('.fa-credit-card'),
    iconB = document.querySelector('.fa-building-columns'),
    iconC = document.querySelector('.fa-wallet'),
    cDetails = document.querySelector('.card-details');

function doFun() {
    tColorA.style.color = "greenyellow";
    tColorB.style.color = "#444";
    tColorC.style.color = "#444";
    iconA.style.color = "greenyellow";
    iconB.style.color = "#aaa";
    iconC.style.color = "#aaa";
    cDetails.style.display = "block";
}

function doFunA() {
    tColorA.style.color = "#444";
    tColorB.style.color = "greenyellow";
    tColorC.style.color = "#444";
    iconA.style.color = "#aaa";
    iconB.style.color = "greenyellow";
    iconC.style.color = "#aaa";
    cDetails.style.display = "none";
}

function doFunB() {
    tColorA.style.color = "#444";
    tColorB.style.color = "#444";
    tColorC.style.color = "greenyellow";
    iconA.style.color = "#aaa";
    iconB.style.color = "#aaa";
    iconC.style.color = "greenyellow";
    cDetails.style.display = "none";
}

let cNumber = document.getElementById('number');
cNumber.addEventListener('keyup', function(e) {
    let num = cNumber.value.replace(/\s/g, '');
    let newValue = '';
    for (let i = 0; i < num.length; i++) {
        if (i % 4 === 0 && i > 0) newValue = newValue.concat(' ');
        newValue = newValue.concat(num[i]);
    }
    cNumber.value = newValue;
    let ccNum = document.getElementById('c-number');
    ccNum.style.border = num.length < 16 ? "1px solid red" : "1px solid greenyellow";
});

let eDate = document.getElementById('e-date');
eDate.addEventListener('keyup', function(e) {
    if (e.target.value.length === 2 && e.key !== 'Backspace') {
        e.target.value += '/';
    }
    eDate.style.border = e.target.value.length < 5 ? "1px solid red" : "1px solid greenyellow";
});

let cvv = document.getElementById('cvv');
cvv.addEventListener('keyup', function() {
    let cvvBox = document.getElementById('cvv-box');
    cvvBox.style.border = cvv.value.length < 3 ? "1px solid red" : "1px solid greenyellow";
});
