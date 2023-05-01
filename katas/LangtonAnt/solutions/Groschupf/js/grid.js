window.onload = function printGrid(){
	var length = 11;
	var table = document.getElementById('grid');
	var counter = 1;
	
	for(i = 0; i < length;i++){
		var row = table.insertRow(i);
		for(j = 0; j < length;j++){
			var cell = row.insertCell(j);
			cell.id = counter;
			counter++;
		}
	}

	document.getElementById("55").innerHTML = '<img src="ant.png" id="ant">';
	fetch('gridSteps.txt')
  .then(response => response.text())
  .then(text => startAnt(text));
}

function startAnt(gridSteps){
	var steps = gridSteps.split("\r\n");
	steps.forEach(step => {
		setTimeout(executeStep(step), 3000);
	});
}

function executeStep(step){
	var fieldId = 1;
	cells = step.split(',');
	cells.forEach(cell => {
		if(cell.length > 1){
			var antDirection = cell.charAt(0);
			moveAnt(fieldId, antDirection);
			cell = cell.charAt(1);
		}

		if(cell == 's'){
			document.getElementById(fieldId).className = 'black';
		} else {
			document.getElementById(fieldId).className = '';
		}
		fieldId++;
	});
}

function moveAnt(fieldId, antDirection)
{
	document.getElementById("ant").parentElement.innerHTML = '';
	document.getElementById(fieldId).innerHTML = '<img src="ant.png" id="ant" class="'+antDirection+'">';
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function readTextFile()
{
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", "gridSteps.txt", false);
    rawFile.onreadystatechange = function ()
    {
        if(rawFile.readyState === 4)
        {
            if(rawFile.status === 200 || rawFile.status == 0)
            {
                var allText = rawFile.responseText;
                return allText;
            }
        }
    }
    alert(rawFile.send(null));
}