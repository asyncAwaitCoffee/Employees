const formEmployee = document.querySelector("#form-employee");
const employeeImport = document.querySelector("#employee-import");
const employeeImportLabel = document.querySelector('label[for="employee-import"');

employeeImport.addEventListener("change", ev => {
	employeeImportLabel.textContent = ev.target.files[0].name;
})

formEmployee.addEventListener("submit", (ev) => {
	ev.preventDefault();

	const formData = new FormData(formEmployee);
	fetch("/employee/create", {
		method: "POST",
		body: formData
	})
		.then(response => {
			employeeImport.value = "";
			employeeImportLabel.textContent = "Импортировать из файла"
			return response.json()
		})
		.then(data => {
			if (!data.ok) {
				showModal(data.validation);
			} else {
				showModal(["Выполнено успешно!"]);
			}
		});
})