var modal = document.getElementById("myModal");
var btn = document.getElementById("openModalBtn");
var spans = document.getElementsByClassName("closeBtn")[0];
var span1 = document.getElementsByClassName("closeBtn1")[0];

var form = document.getElementById("modalForm");
var submitBtn = document.getElementById("submitBtn");
var isActiveCheckbox = document.getElementById("isActive");
var isActiveContainer = document.getElementById("isActiveContainer");
var deleteModal = document.getElementById('deleteModal');
var employeeId;
// When the user clicks the button, open the modal 
btn.onclick = function () {
    form.reset();
    isActiveContainer.style.display = 'none';
    modal.style.display = "block";        ;
}

// When the user clicks on <span> (x), close the modal

spans.onclick = function () {
    employeeId = -1;
        modal.style.display = "none";
    }

span1.onclick = function () {
    deleteModal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
function renderUI(data) {
    const empData = document.getElementById('employeedata');
    const html = data.map((item) => {
        return `<tr>
            <td>${item.FirstName}</td>
            <td>${item.LastName}</td>
            <td>${item.MobileNo}</td>
            <td>${item.Address}</td>
            <td>${item.IsActive == true ? 'Active': 'In Active'}</td>
            <td>
                <button id="Edit" name="Edit" class="edit-form" data-id="${item.EmployeeID}">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil" viewBox="0 0 16 16">
                        <path d="M12.146.146a.5.5 0 0 1 .708 0l2 2a.5.5 0 0 1 0 .708L3.207 14.5H1.5v-1.707L12.146.146zM11.207 2L3 10.207V13h2.793L14 4.793 11.207 2z"/>
                    </svg>
                </button>
                <button id="Delete" name="Delete" class="delete-form" data-id="${item.EmployeeID}">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
                        <path d="M5.5 5.5A.5.5 0 0 1 6 5h4a.5.5 0 0 1 .5.5v8A.5.5 0 0 1 10 14h-4a.5.5 0 0 1-.5-.5v-8zm3 2a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0v-6zm-2 0a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0v-6z"/>
                        <path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H12v9.5A1.5 1.5 0 0 1 10.5 15h-5A1.5 1.5 0 0 1 4 13.5V4H2.5a1 1 0 0 1 0-2h3.5a1 1 0 0 1 1-1h3a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4h8l-.118-.5h-7.764z"/>
                    </svg>
                </button>
            </td>
        </tr>`
    })
    console.log(html);
    empData.innerHTML = html.join("");
    document.querySelectorAll('.edit-form').forEach((btn) => {
        btn.addEventListener('click', async () => {
            var empId = btn.getAttribute('data-id');
            employeeId = empId;
            try {
                const response = await (await fetch(`Controller/EmployeeHandler.ashx?method=GetEmployeeDataById&empId=${empId}`)).json();
                const employeeData = JSON.parse(response);

                // Populate the form with fetched employee data
                document.getElementById('firstName').value = employeeData[0].FirstName;
                document.getElementById('lastName').value = employeeData[0].LastName;
                document.getElementById('mobileNo').value = employeeData[0].MobileNo;
                document.getElementById('address').value = employeeData[0].Address;

                isActiveCheckbox.checked = employeeData[0].IsActive;

                isActiveContainer.style.display = 'block';

                // Show the modal
                modal.style.display = "block";
            } catch (error) {
                console.log(`Error: ${error}`);
            }
        });
    });
    document.querySelectorAll('.delete-form').forEach((btn) => {
        btn.addEventListener('click', async () => {
            var empId = btn.getAttribute('data-id');

            deleteModal.style.display = "block";
            try {
                document.getElementById('confirmDeleteBtn').addEventListener('click',async ()=> {
                    deleteModal.style.display = "none";
                    const response = await(await fetch(`Controller/EmployeeHandler.ashx?method=DeleteData&empId=${empId}`)).json();
                    if (response == true) {
                        alert(`Employee Id ${empId} is deleted`);
                        document.dispatchEvent('DOMContentLoaded');
                    } else {
                        console.log(response);
                    }                });
                document.getElementById('cancelDeleteBtn').addEventListener('click', () => {
                    deleteModal.style.display = "none";
                });
                
            } catch (error) {
                console.log(`Error: ${error}`)
            }
        });
    });

}
document.addEventListener('DOMContentLoaded', async (e) => {
    try {
        const response = JSON.parse(await(await fetch('Controller/EmployeeHandler.ashx?method=GetEmployeeData')).json());
        renderUI(response);
    } catch(error) {
        console.log(`Error: ${error}`)
    }
});
submitBtn.addEventListener('click', (e) => {
    e.preventDefault();
    const formData = new FormData(form)
    let data = Object.fromEntries(formData);
    data.isActive = isActiveCheckbox.checked;
    data.employeeId = employeeId;
    if (!data.firstName) {
        alert("Please enter the First Name");
    } else if (!data.lastName) {
        alert("Please enter the Last Name");
    } else if (!data.mobileNo) {
        alert("Please enter the Mobile Number");
    } else if (!data.address) {
        alert("Please enter the Address");
    } else {
        $.ajax({
            async: false,
            url: 'Controller/EmployeeHandler.ashx?method=AddUpdateEmployees',
            type: 'POST',
            data: { EmployeeData: JSON.stringify(data) },
            dataType: 'json',
            success: function (response) {
                alert('Data saved successfully!');
                modal.style.display = "none";
                form.reset();
                window.location.reload();
            },
            error: function () {
                alert('An error occurred!');
            }
        });
    }
    console.log(data);    
    // Send the data using jQuery AJAX
    
}) 
