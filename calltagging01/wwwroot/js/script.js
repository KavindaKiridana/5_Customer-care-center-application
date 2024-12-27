function navFilter(x) {
    const categories = {
        1: [
            { id: 'checkbox1', label: 'Browsing slow' },
            { id: 'checkbox2', label: 'Speed issue' },
            { id: 'checkbox3', label: 'ONT/Router fault' },
            { id: 'checkbox4', label: 'Internet issue' },
            { id: 'checkbox5', label: 'Usage complain' },
            { id: 'checkbox6', label: 'Extra GB related' },
            { id: 'checkbox7', label: 'Add on related' },
            { id: 'checkbox8', label: 'Package issue' },
            { id: 'checkbox9', label: 'Package upgrade' },
            { id: 'checkbox10', label: 'Package downgrade' },
            { id: 'checkbox11', label: 'Password change' },
            { id: 'checkbox12', label: 'IP change' },
            { id: 'checkbox13', label: '4G Coverage issue' },
            { id: 'checkbox14', label: 'Reload issue' },
            { id: 'checkbox15', label: 'Service terminated' },
            { id: 'checkbox16', label: 'Router config' },
            { id: 'checkbox17', label: 'Repeated fault' }
        ],
        2: [
            { id: 'checkbox18', label: 'Channel related' },
            { id: 'checkbox19', label: 'Channel modification' },
            { id: 'checkbox20', label: 'TSTv Issue' },
            { id: 'checkbox21', label: 'STB' },
            { id: 'checkbox22', label: 'Remote fault' },
            { id: 'checkbox23', label: 'ONT Router fault' },
            { id: 'checkbox24', label: 'No Service' },
            { id: 'checkbox25', label: 'Reconnection issue' },
            { id: 'checkbox26', label: 'Service termination' },
            { id: 'checkbox27', label: 'Repeated complain' }
        ],
        3: [
            { id: 'checkbox28', label: 'No service' },
            { id: 'checkbox29', label: 'CPE fault' },
            { id: 'checkbox30', label: 'Repeated fault' }
        ],
        4: [
            { id: 'checkbox31', label: 'Balance Inquiry' },
            { id: 'checkbox32', label: 'Bill Complaint' },
            { id: 'checkbox33', label: 'Reconnection Request' },
            { id: 'checkbox34', label: 'Repeated Faulty' }
        ],
        5: [
            { id: 'checkbox35', label: 'MySLT App Related' },
            { id: 'checkbox36', label: 'PEO Mobile Related' },
            { id: 'checkbox37', label: 'SLT GO Related' },
            { id: 'checkbox38', label: 'Email Related' }
        ]
    };

    const voiceBillingCategories = [3, 4, 5]; // Categories to be displayed in a single column

    const checkboxContainer1 = document.getElementById('checkboxContainer1');
    const checkboxContainer2 = document.getElementById('checkboxContainer2');
    
    // Clear previous checkboxes
    checkboxContainer1.innerHTML = '';
    checkboxContainer2.innerHTML = '';

    if (categories[x]) {
        if (voiceBillingCategories.includes(x)) {
            // Display in a single column
            categories[x].forEach(item => {
                const div = document.createElement('div');
                div.className = 'form-check p-2';
                div.innerHTML = `
                    <input class="form-check-input" type="checkbox" id="${item.id}" value="${item.id}">
                    <label class="form-check-label" for="${item.id}">${item.label}</label>
                `;
                checkboxContainer1.appendChild(div);
            });
        } else {
            // Divide items into two columns
            const half = Math.ceil(categories[x].length / 2);
            const itemsColumn1 = categories[x].slice(0, half);
            const itemsColumn2 = categories[x].slice(half);

            // Create first column
            itemsColumn1.forEach(item => {
                const div1 = document.createElement('div');
                div1.className = 'form-check p-2';
                div1.innerHTML = `
                    <input class="form-check-input" type="checkbox" id="${item.id}" value="${item.id}">
                    <label class="form-check-label" for="${item.id}">${item.label}</label>
                `;
                checkboxContainer1.appendChild(div1);
            });

            // Create second column
            itemsColumn2.forEach(item => {
                const div2 = document.createElement('div');
                div2.className = 'form-check p-2';
                div2.innerHTML = `
                    <input class="form-check-input" type="checkbox" id="${item.id}" value="${item.id}">
                    <label class="form-check-label" for="${item.id}">${item.label}</label>
                `;
                checkboxContainer2.appendChild(div2);
            });
        }
    }
}

// Call navFilter(1) on page load to set the default page
window.onload = function() {
    navFilter(1);
    document.querySelector('#category1').classList.add('navSelected');
};

// Event listener for navigation links
document.addEventListener('DOMContentLoaded', function() {
    const navLinks = document.querySelectorAll('.nav-link');

    navLinks.forEach(link => {
        link.addEventListener('click', function() {
            navLinks.forEach(item => item.classList.remove('navSelected'));
            this.classList.add('navSelected');
            const category = this.getAttribute('data-category');
            navFilter(parseInt(category));
        });
    });
});
function editNumber() {
    const phoneNumberElement = document.getElementById('phoneNumber');
    const currentNumber = phoneNumberElement.innerText;
    phoneNumberElement.innerHTML = `<input type="text" id="phoneInput" value="${currentNumber}" onblur="saveNumber()">`;
    document.getElementById('phoneInput').focus();
}

function saveNumber() {
    const inputElement = document.getElementById('phoneInput');
    const newNumber = inputElement.value;
    const phoneNumberElement = document.getElementById('phoneNumber');
    phoneNumberElement.innerHTML = newNumber;
    phoneNumberElement.setAttribute('onclick', 'editNumber()');
}