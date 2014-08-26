

var home = home || {};


home.testConnection = function () {
    $("#service-version").empty().append(utils.Spinner());
    $("#comparison-version").empty().append(utils.Spinner());
    $.ajax({
        type: "POST",
        url: "Home/TestConnection",
        data: {},
        success: home.onSuccessTest,
        error: home.onFailedTest
    });
}

home.onSuccessTest = function (result) {
    $("#service-version").empty();
    $("#comparison-version").empty();
    if (result.success == true) {
        $("#service-version").text(result.service);
        $("#comparison-version").text(comparison = result.comparison);
    } else {
        utils.showError(result.message);
    }

}

home.onFailedTest = function (req, status, error) {
    $("#service-version").empty("img");
    $("#comparison-version").empty("img");
    utils.showError(error);
}


$(function () {
    home.testConnection();
});