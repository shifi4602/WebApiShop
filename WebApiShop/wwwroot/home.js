
async function user() {
    const username = document.querySelector("#userName");
    const password = document.querySelector("#pass")
    const firstName = document.querySelector("#firstName")
    const lastName = document.querySelector("#lastName")

    const newUser = {
        Email: username.value,
        Password: password.value,
        FirstName: firstName.value,
        LastName: lastName.value,
    };

    console.log("in post 0")
    try {
        const response = await fetch('https://localhost:44350/api/users',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newUser)
            });
        if (response.ok) {
            const data = await response;
            sessionStorage.setItem("User", JSON.stringify(data))
            console.log('Post data:', data);
            alert("המשתמש נרשם בהצלחה")
        }
        else {
            throw new Error(`HTTP error! status"${response.status}"`)
        }
    }
    catch (e) {
        console.log(e)
    }
    localStorage.setItem("user", JSON.stringify(newUser));
    let userObject = localStorage.getItem("user");
    let currentUser = JSON.parse(userObject);
}

async function login() {
    const username = document.querySelector("#userName1");
    const password = document.querySelector("#pass1")

    const existUser = {
        Email: username.value,
        Password: password.value,
    };
    try {
        const response = await fetch('/api/users/login',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(existUser)
            });
        if (!response.ok) {
            throw new Error(`HTTP error! status"${response.status}"`)
        }
        if (response.status == 204) {
            alert("המשתמש לא קים במערכת")
        }
        const data = await response.json();
        sessionStorage.setItem("User", JSON.stringify(data))
        console.log('Post data:', data);
        window.location.href = "update.html"
    }
    catch (e) {
        console.log(e)
    }
}



