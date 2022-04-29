let dropArea = document.getElementById('drop-area');
let uploadProgress = [];
let progressBar = document.getElementById('progress-bar');
var files = [];

document.getElementById("upload-button").addEventListener("click", function (event) {
    event.preventDefault()
});

//Prevent default functionality
;['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
    dropArea.addEventListener(eventName, preventDefaults, false);
})

function preventDefaults(e) {
    e.preventDefault();
    e.stopPropagation();
}

//Highligh when file is dragged over.
;['dragenter', 'dragover'].forEach(eventName => {
    dropArea.addEventListener(eventName, highlight, false);
})

;['dragleave', 'drop'].forEach(eventName => {
    dropArea.addEventListener(eventName, unhighlight, false);
})

function highlight(e) {
    dropArea.classList.add('highlight');
}

function unhighlight(e) {
    dropArea.classList.remove('highlight');
}

//Handle dropped files
dropArea.addEventListener('drop', handleDrop, false)

function handleDrop(e) {
    let dt = e.dataTransfer;
    let files = dt.files;
    handleFiles(files);
}

function handleFiles(files) {
    files = [...files];
    initializeProgress(files.length)
    files.forEach(previewFile);
}

function previewFile(file) {
    files.push(file);
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onloadend = function () {
        let img = document.createElement('img');
        img.src = reader.result;
        document.getElementById('gallery').appendChild(img);
    };
}

function upload() {
    for (let i = 0; i < files.length; i++) {
        uploadFile(files[i], i);
    }
}

function uploadFile(file, i) {
    var url = 'UploadFiles';
    var xhr = new XMLHttpRequest();
    var formData = new FormData();
    xhr.open('POST', url, true);

    xhr.upload.addEventListener("progress", function (e) {
        updateProgress(i, (e.loaded * 100.0 / e.total) || 100);
    });

    xhr.addEventListener('readystatechange', function (e) {
        if (xhr.readyState == 4 && xhr.status == 200) {
            //Success
        }
        else if (xhr.readyState == 4 && xhr.status != 200) {
            //Failure
        }
    });

    formData.append('file', file);
    formData.append('name', document.getElementById('name').value);
    xhr.send(formData);
}

//Display Progress
function initializeProgress(numFiles) {
    progressBar.value = 0;
    uploadProgress = [];

    for (let i = numFiles; i > 0; i--) {
        uploadProgress.push(0);
    }
}

function updateProgress(fileNumber, percent) {
    progressBar.style.display = "block";
    uploadProgress[fileNumber] = percent;
    let total = uploadProgress.reduce((tot, curr) => tot + curr, 0) / uploadProgress.length;
    progressBar.value = total;
}