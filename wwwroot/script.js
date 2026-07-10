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
        localStorage.setItem('jwtToken', data.token);
        // Səni birbaşa bura yönləndirir:
        window.location.href = 'admin.html'; 
    } else {
        alert("İstifadəçi adı və ya şifrə yanlışdır.");
    }
}

async function initPanel() {
    const token = localStorage.getItem('jwtToken');
    if (!token) { 
        window.location.href = 'index.html'; 
        return; 
    }

    const response = await fetch('/api/Todo', {
        headers: { 'Authorization': 'Bearer ' + token }
    });
    
    if (response.ok) {
        const tasks = await response.json();
        const listDiv = document.getElementById('task-list');
        
        // Əgər tapşırıq yoxdursa, bunu yazacaq
        if (tasks.length === 0) {
            listDiv.innerHTML = "<p>Hazırda tapşırıq yoxdur.</p>";
        } else {
            // Əgər tapşırıq varsa, siyahını quracaq
            listDiv.innerHTML = tasks.map(t => `
                <div style="border:1px solid #ccc; padding:10px; margin-bottom:10px;">
                    <b>${t.title}</b> - ${t.description}
                </div>
            `).join('');
        }
    } else {
        document.getElementById('task-list').innerHTML = "Tapşırıqları çəkmək mümkün olmadı.";
    }
}

async function deleteTask(id) {
    const token = localStorage.getItem('jwtToken');
    await fetch(`/api/Todo/${id}`, {
        method: 'DELETE',
        headers: { 'Authorization': 'Bearer ' + token }
    });
    location.reload();
}