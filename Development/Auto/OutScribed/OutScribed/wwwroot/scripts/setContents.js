
function OnSetContents(value) {

	try {
		var editor = document.getElementsByClassName('ql-editor');

		editor[0].innerHTML = value;
	}
	catch (Exception) {
		console.log(Exception);

	}


}
