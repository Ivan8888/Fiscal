function ShowCustomAlert(text){
    alert(text);
}

function ShowLocalStorageVisit() {
    var key = "num_visit";
    var num_of_visit_local = 1;

    var num_of_visit_string_local = localStorage.getItem(key);
    if (num_of_visit_string_local) {
        num_of_visit_local = parseInt(num_of_visit_string_local) + 1;
    }

    localStorage.setItem(key, num_of_visit_local);
    $("#p_local").text(num_of_visit_local);
    alert('Num of visit localStorage is: ' + num_of_visit_local);
}

function ShowSessionStorageVisit() {
    var key = "num_visit";
    var num_of_visit_session = 1;

    var num_of_visit_string_session = sessionStorage.getItem(key);
    if (num_of_visit_string_session) {
        num_of_visit_session = parseInt(num_of_visit_string_session) + 1;
    }

    sessionStorage.setItem(key, num_of_visit_session);
    $("#p_session").text(num_of_visit_session);
    alert('Num of visit sessionStorage is: ' + num_of_visit_session);
}

function CallWebApi() {
        $.ajax({
            url: "http://localhost:64034/api/product/get/2",
            type: "GET"
            //data: JSON.stringify({key: 'key3', value: 'value3' }),
            //contentType: "application/json;charset=utf-8"
        }).done(function (responseText) {
            $(".api_result").text(responseText);
        }).fail(function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText);
        });
    }
