// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const GENDER_LIST = [
    { id: 1, name: "Pria" },
    { id: 2, name: "Wanita" }
];

const HOBBIES = ["Sepak Bola", "Badminton", "Tennis", "Renang", "Bersepeda", "Tidur"];

const USER_INFO = {
    name: "Mulia Abror Khairul",
    age: 27,
    email: "muliausman12@gmail.com"
};

document.addEventListener("DOMContentLoaded", function () {
    $("#btnGenerate").on("click", function () {
        const tbody = $("#data-table tbody");
        tbody.empty();

        for (let i = 1; i <= 1000; i++) {
            const name = randomString(8); // 8 karakter huruf kapital + angka
            const genderObj = GENDER_LIST[Math.floor(Math.random() * GENDER_LIST.length)];
            const hobby = HOBBIES[Math.floor(Math.random() * HOBBIES.length)];
            const age = Math.floor(Math.random() * (40 - 18 + 1)) + 18;

            const row = `<tr>
                <td>${i}</td>
                <td>${name}</td>
                <td data-id="${genderObj.id}">${genderObj.name}</td>
                <td>${hobby}</td>
                <td>${age}</td>
            </tr>`;
            tbody.append(row);
        }

        $("#btnGenerate").prop("disabled", true);
        $("#btnReset").prop("disabled", false);
        $("#btnSubmit").prop("disabled", false);
    });

    $("#btnReset").on("click", function () {
        $("#data-table tbody").empty();
        $("#btnGenerate").prop("disabled", false);
        $("#btnReset").prop("disabled", true);
        $("#btnSubmit").prop("disabled", true);
    });

    $("#btnSubmit").on("click", function () {
        const payload = [];

        $("#data-table tbody tr").each(function (index) {
            const cols = $(this).find("td");
            payload.push({
                id: index + 1,
                nama: $(cols[1]).text(),
                genderId: parseInt($(cols[2]).attr("data-id")),
                genderName: $(cols[2]).text(),
                HobiName: $(cols[3]).text(),
                age: parseInt($(cols[4]).text())
            });
        });

        const finalJson = {
            name: USER_INFO.name,
            age: USER_INFO.age,
            email: USER_INFO.email,
            payload: payload
        };

        console.log("JSON yang dikirim ke backend:");
        console.log(JSON.stringify(finalJson, null, 2));

        $.ajax({
            url: "/Home/Submit",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(finalJson),
            success: function (res) {
                alert(`Data berhasil disimpan oleh ${USER_INFO.name} (${USER_INFO.email})`);
            },
            error: function (xhr) {
                try {
                    const errors = JSON.parse(xhr.responseText);
                    if (Array.isArray(errors)) {
                        alert(errors.join("\n"));
                    } else {
                        alert(errors);
                    }
                } catch (e) {
                    alert("Terjadi error tidak terduga.");
                }
            }
        });
    });

    function randomString(length = 8) {
        const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
        let result = '';
        for (let i = 0; i < length; i++) {
            result += chars.charAt(Math.floor(Math.random() * chars.length));
        }
        return result;
    }
});
