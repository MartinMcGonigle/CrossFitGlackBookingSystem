function markUserPresent(classRegistrationId, classId, userId) {
    fetch(`/Class/MarkUserPresent?classRegistrationId=${classRegistrationId}&classId=${classId}&userId=${userId}`, {
        method: 'POST',
    })
        .then(response => {
            if (response.ok) {
                const userRow = document.getElementById(`userRow_${classRegistrationId}`);
                userRow.style.backgroundColor = '#d5e6a1';

                // Toggle button visibility
                const presentButton = document.getElementById(`presentButton_${classRegistrationId}`);
                const absentButton = document.getElementById(`absentButton_${classRegistrationId}`);
                presentButton.style.display = 'none';
                absentButton.style.display = 'inline';
            }
        })
        .catch(error => {
            console.error('An error occurred:', error);
        });
}

function markUserAbsent(classRegistrationId, classId, userId) {
    fetch(`/Class/MarkUserAbsent?classRegistrationId=${classRegistrationId}&classId=${classId}&userId=${userId}`, {
        method: 'POST',
    })
        .then(response => {
            if (response.ok) {  
                const userRow = document.getElementById(`userRow_${classRegistrationId}`);
                userRow.style.backgroundColor = '#ffc6b3';

                // Toggle button visibility
                const presentButton = document.getElementById(`presentButton_${classRegistrationId}`);
                const absentButton = document.getElementById(`absentButton_${classRegistrationId}`);
                presentButton.style.display = 'inline';
                absentButton.style.display = 'none';
            }
        })
        .catch(error => {
            console.error('An error occurred:', error);
        });
}