

function showDeleteConfirmation(event, message = "هل أنت متأكد أنك تريد الحذف؟") {

    event.preventDefault();

    var form = event.target;

    var overlay = document.createElement("div");

    overlay.style.position = "fixed";
    overlay.style.top = "0";
    overlay.style.left = "0";
    overlay.style.width = "100vw";
    overlay.style.height = "100vh";
    overlay.style.backgroundColor = "rgba(0,0,0,0.5)";
    overlay.style.display = "flex";
    overlay.style.alignItems = "center";
    overlay.style.justifyContent = "center";
    overlay.style.zIndex = "999999";

    var box = document.createElement("div");

    box.style.width = "400px";
    box.style.maxWidth = "90%";
    box.style.backgroundColor = "#ffffff";
    box.style.borderRadius = "12px";
    box.style.padding = "30px";
    box.style.textAlign = "center";
    box.style.boxShadow = "0 10px 40px rgba(0,0,0,0.3)";

    box.innerHTML = `
        <h3 style="margin-bottom:20px;">
            تأكيد الحذف
        </h3>

        <p style="font-size:16px; margin-bottom:25px;">
            ${message}
        </p>

        <button id="confirmDelete"
                style="
                    padding:10px 25px;
                    margin:5px;
                    border:none;
                    border-radius:8px;
                    background:#198754;
                    color:white;
                    font-weight:bold;
                    cursor:pointer;
                ">
            موافق
        </button>

        <button id="cancelDelete"
                style="
                    padding:10px 25px;
                    margin:5px;
                    border:none;
                    border-radius:8px;
                    background:#6c757d;
                    color:white;
                    font-weight:bold;
                    cursor:pointer;
                ">
            تراجع
        </button>
    `;

    overlay.appendChild(box);
    document.body.appendChild(overlay);

    document.getElementById("cancelDelete").onclick = function () {
        overlay.remove();
    };

    document.getElementById("confirmDelete").onclick = function () {
        overlay.remove();
        form.submit();
    };

    return false;
}


                    
function showMessageBox(title, message) {

   

        var overlay = document.createElement("div");

        overlay.style.position = "fixed";
        overlay.style.top = "0";
        overlay.style.left = "0";
        overlay.style.width = "100vw";
        overlay.style.height = "100vh";
        overlay.style.backgroundColor = "rgba(0,0,0,0.5)";
        overlay.style.display = "flex";
        overlay.style.alignItems = "center";
        overlay.style.justifyContent = "center";
        overlay.style.zIndex = "999999";

        var box = document.createElement("div");

        box.style.width = "400px";
        box.style.maxWidth = "90%";
        box.style.backgroundColor = "#ffffff";
        box.style.borderRadius = "12px";
        box.style.padding = "30px";
        box.style.textAlign = "center";
        box.style.boxShadow = "0 10px 40px rgba(0,0,0,0.3)";

        box.innerHTML = `
            <h3 style="margin-bottom:20px;">
                ${title}
            </h3>

            <p style="font-size:16px; line-height:1.8; margin-bottom:25px;">
                ${message}
            </p>

            <button
                id="messageBoxOk"
                style="
                    min-width:110px;
                    padding:10px 25px;
                    border:none;
                    border-radius:8px;
                    background:#198754;
                    color:white;
                    font-weight:bold;
                    cursor:pointer;
                ">
                موافق
            </button>
        `;

        overlay.appendChild(box);
        document.body.appendChild(overlay);
        document.getElementById("confirmDelete").onclick = function () {
    form.submit();
};

        // document.getElementById("messageBoxOk").addEventListener("click", function () {
        //     overlay.remove();
        // });

}