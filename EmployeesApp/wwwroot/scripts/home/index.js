const formSee = document.querySelector("#form-see");
const companySelect = document.querySelector('[name="company"]')
const employeeSelect = document.querySelector('[name="employee"]')

const companyName = document.querySelector("#company-name");
const inn = document.querySelector("#inn");
const legalAdress = document.querySelector("#legal-adress");
const factAdress = document.querySelector("#fact-adress");

const employeeName = document.querySelector("#employee-name");
const surname = document.querySelector("#surname");
const patronymic = document.querySelector("#patronymic");
const birthdate = document.querySelector("#birthdate");
const passportSeries = document.querySelector("#passport-series");
const passportNumber = document.querySelector("#passport-number");

formSee.addEventListener("submit", ev => {
	ev.preventDefault();
})

companySelect.addEventListener("change", ev => {
	loadEmploeesByCompany(ev.target.value);

	fetch(`Company/GetById?companyId=${ev.target.value}`)
		.then(response => response.json())
		.then(result => {
			companyName.value = result.data.name;
			inn.value = result.data.inn;
			legalAdress.value = result.data.legal;
			factAdress.value = result.data.fact;

			employeeName.value = "";
			surname.value = "";
			patronymic.value = "";
			birthdate.value = "";
			passportSeries.value = "";
			passportNumber.value = "";
		})
})

employeeSelect.addEventListener("change", ev => {
	fetch(`Employee/GetById?employeeId=${ev.target.value}`)
		.then(response => response.json())
		.then(result => {
			employeeName.value = result.data.firstName;
			surname.value = result.data.surname;
			patronymic.value = result.data.patronymic;
			birthdate.value = result.data.birthdate;
			passportSeries.value = result.data.series;
			passportNumber.value = result.data.number;
		})
})

function downloadFile(fileId) {
	const fileNames = { 1: "Companies", 2: "Employees" };

	fetch(`home/DownloadFile?fileId=${fileId}`)
		.then(res => res.blob())
		.then(blob => {
			const url = window.URL.createObjectURL(blob);
			const link = document.createElement('a');
			link.href = url;
			link.setAttribute('download', `${fileNames[fileId]}.csv`);
			link.click();
		})
}

function loadEmploeesByCompany(companyId) {
	fetch(`Employee/ByCompany?companyId=${companyId}`)
		.then(response => response.json())
		.then(result => {
			employeeSelect.replaceChildren(document.querySelector('option[value="-1"]'))
			employeeSelect.value = -1;
			for (const emp of result.data) {
				const opt = document.createElement("option");
				opt.value = emp.employeeId;
				opt.textContent = `${emp.surname} ${emp.firstName} ${emp.patronymic}`;
				employeeSelect.appendChild(opt);
			}
		});
}