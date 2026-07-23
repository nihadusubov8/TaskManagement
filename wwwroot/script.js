async function login() {
    const user = {
        username: document.getElementById('username').value,
        password: document.getElementById('password').value
    };

    const response = await fetch('/api/Auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    });

    if (response.ok) {
        const data = await response.json();
        
        if (data.token) {
            localStorage.setItem('jwtToken', data.token);
            window.location.href = 'admin.html';
        } else {
            window.location.href = 'admin.html';
        }
    } else {
        alert("İstifadəçi adı və ya şifrə yanlışdır.");
    }
}

async function initPanel() {

    const token = localStorage.getItem('jwtToken');

    const response = await fetch('/api/Assignment', {
        headers: {
            'Authorization': 'Bearer ' + token
        }
    });

    if (response.ok) {
        const tasks = await response.json();
        const listDiv = document.getElementById('task-list');

        if (tasks && tasks.length > 0) {
            listDiv.innerHTML = tasks.map(t => {
                const isActive = t.status === 1;
                const statusColor = isActive ? "green" : "red";
                const statusText = isActive ? "AÇIQ" : "BAĞLI";

                return `
                <div style="border:1px solid #ccc; padding:10px; margin-bottom:10px; cursor:pointer;">
                    <div onclick="toggleDescription(${t.id})">
                        <b>ID: ${t.id} | ${t.title}</b>
                    </div>
                    
                    <div id="desc-${t.id}" style="display:none; padding:10px; background:#f9f9f9; margin-top:5px; border-top:1px solid #eee;">
                        Açıqlama: ${t.description}
                    </div>
                    
                    <div style="margin-top:10px;">
                        <button style="background-color:${statusColor}; color:white; border:none; padding:5px 15px; pointer-events:none; font-weight:bold;">
                            ${statusText}
                        </button>
                    </div>
                </div>`;
            }).join('');
        } else {
            listDiv.innerHTML = "Hələlik tapşırıq yoxdur.";
        }
    } else if (response.status === 401) {
        alert("Sessiya bitib, yenidən daxil olun.");
        window.location.href = 'index.html';
    } else {
        console.log("Tapşırıqları yükləmək mümkün olmadı.");
    }
}

function toggleDescription(id) {
    const el = document.getElementById('desc-' + id);
    el.style.display = (el.style.display === 'none' || el.style.display === '') ? 'block' : 'none';
}

async function deleteTask(id) {
    const token = localStorage.getItem('jwtToken');
    await fetch(`/api/Assignment/${id}`, {
        method: 'DELETE',
        headers: { 'Authorization': 'Bearer ' + token }
    });
    location.reload();
}

async function register() {
    const userData = {
        username: document.getElementById('username').value,
        password: document.getElementById('password').value,
        email: document.getElementById('email').value
    };

    const response = await fetch('/api/Auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(userData)
    });

    if (response.ok) {
        alert("Qeydiyyat uğurludur!");
        window.location.href = 'index.html';
    } else {
        alert("Xəta baş verdi.");
    }
}