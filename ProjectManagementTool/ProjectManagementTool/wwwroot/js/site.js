
// Write your JavaScript code.

window.setTimeout(function () {
    $(".alert").fadeTo(500, function () {
        $(this).remove();
    });
}, 2000);


// Convert seconds into day, hour, minutes and second
function secondsToDHMS(seconds) {
    seconds = Number(seconds);
    var day = Math.floor(seconds / (3600 * 24));
    var hour = Math.floor((seconds % (3600 * 24)) / 3600);
    var minutes = Math.floor((seconds % 3600) / 60);
    var sec = Math.floor(seconds % 60);

    if (sec == 0) {
        return sec + " seconds";
    }
    else if (minutes == 0) {
        return sec + " seconds";
    }
    else if (hour == 0) {
        return minutes + " minutes " + sec + " seconds";
    }
    else if (day == 0) {
        return hour + " hours " + minutes + " minutes " + sec + " seconds";
    }
    else {
        return day + " days " + hour + " hours " + minutes + " minutes " + sec + " seconds";
    }
}


// formating date
function formatedDate(val) {
    var date = new Date(val);
    const month = String(date.getUTCMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const year = date.getFullYear();
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day} ${hours}:${minutes}`;
}

// Formate the details
function showDetailsModal(startTime, endTime, totalTime) {
    startTime = "Start time: " + startTime + "\n";
    endTime = "End time: " + endTime + "\n";
    seconds = secondsToDHMS(totalTime);
    totalTime = "Total working time: " + seconds;

    var mainTimeDescription = startTime + endTime + totalTime;
    return mainTimeDescription;
}

// Formate the details
function showDetailsModalWithDescription(description, startTime, endTime, totalTime) {
    description = "Description: " + description + "\n";
    var result = showDetailsModal(startTime, endTime, totalTime);
    var mainTimeDescription = description + result;
    return mainTimeDescription;
}


// History table
function createTimeHistoryTable(data) {
    let timeHistoryTable = `
                <table class="table table-bordered table-striped table-hover">
                    <thead>
                        <tr>
                            <th>Start Time</th>
                            <th>End Time</th>
                            <th>Total Time</th>
                        </tr>
                    </thead>
                    <tbody>`;

    data.forEach(item => {
        const startTime = formatedDate(item.startTime);
        const endTime = formatedDate(item.endTime);
        const totalTime = secondsToDHMS(item.totalTime);

        timeHistoryTable += `
                    <tr>
                        <td>${startTime}</td>
                        <td>${endTime}</td>
                        <td>${totalTime}</td>
                    </tr>`;
    });
    timeHistoryTable += `
                            </tbody>
                        </table>`;

    return timeHistoryTable;
}

// Flip the play and pause button
function flipPlayPauseButton(id, status) {
    if (status === "Started") {
        document.getElementById("playButton-" + id).classList.add("d-none");
        document.getElementById("pauseButton-" + id).classList.remove("d-none");
    } else {
        document.getElementById("playButton-" + id).classList.remove("d-none");
        document.getElementById("pauseButton-" + id).classList.add("d-none");
    }
}

// calculate waiting time to active the pause button
function waitingTimeToActivePauseButton(startTime) {
    var date1 = new Date(startTime);
    var date2 = new Date();
    var waitingTime = Math.floor(Math.abs(date2.getTime() - date1.getTime()) / 1000);

    if (waitingTime > 10) {
        waitingTime = 0;
    }
    else {
        waitingTime = 10 - waitingTime;
    }

    return waitingTime;
}
// Update stop watch

