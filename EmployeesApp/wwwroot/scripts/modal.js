const modal = document.querySelector("#modal");
const messagesList = document.querySelector("#messages-list");

function hideModal() {
	modal.classList.add("display-none");
	while (messagesList.firstChild) {
		messagesList.removeChild(messagesList.firstChild);
	}
}
function showModal(messages) {
	modal.classList.remove("display-none");

	for (const message of messages) {
		const li = document.createElement("li");
		li.textContent = message;
		messagesList.appendChild(li);
	}
}