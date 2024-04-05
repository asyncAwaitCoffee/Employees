const formCompany = document.querySelector("#form-company");
const companyImport = document.querySelector("#company-import");
const companyImportLabel = document.querySelector('label[for="company-import"');

companyImport.addEventListener("change", ev => {
	companyImportLabel.textContent = ev.target.files[0].name;
})

formCompany.addEventListener("submit", async (ev) => {
	ev.preventDefault();
	const formData = new FormData(formCompany);
	await fetch("/company/create", {
		method: "POST",
		body: formData
	})
		.then(response => {
			companyImport.value = "";
			companyImportLabel.textContent = "Import from file"
			return response.json()
		})
		.then(data => {
			if (!data.ok) {
				showModal(data.validation);
			} else {
				showModal(["Success!"]);
			}
		});
})