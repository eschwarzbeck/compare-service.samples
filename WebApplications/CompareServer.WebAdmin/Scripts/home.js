var comparer = comparer || {}


/*
 *     Get Server Version
 */
comparer.testConnection = function () {
    $("#service-version").empty().append(utils.Spinner());
    $("#comparison-version").empty().append(utils.Spinner());
    $.ajax({
        type: "POST",
        url: "Home/TestConnection",
        data: {},
        success: comparer.onSuccessTest,
        error: comparer.onFailedTest
    });
}

comparer.onSuccessTest = function (result) {
    $("#service-version").empty();
    $("#comparison-version").empty();
    if (result.success == true) {
        $("#service-version").text(result.service);
        $("#comparison-version").text(comparison = result.comparison);
    } else {
        utils.showError(result.message);
    }

}

comparer.onFailedTest = function (req, status, error) {
    $("#service-version").empty("img");
    $("#comparison-version").empty("img");
    utils.showError(error);
}

/*
 *      Get Last Access info ( from log files 
 */
comparer.serverInfo = function () {
    $("#last-access").empty().append(utils.Spinner());
    $.ajax({
        type: "POST",
        url: "Home/ServerInfo",
        data: {},
        success: comparer.onSuccessInfo,
        error: comparer.onFailedInfo
    });
}

comparer.onSuccessInfo = function (result) {
    $("#last-access").empty();
    if (result.success == true) {
        $("#last-access").text(result.access);
    } else {
        utils.showError(result.message);
    }
}

comparer.onFailedInfo = function (req, status, error) {
    $("#last-access").empty();
    utils.showError(error);
}


/*
*      Get Last Rendering Sets Numver
*/
comparer.renderingSets = function () {
    $("#rsets-available").empty().append(utils.Spinner());
    $.ajax({
        type: "POST",
        url: "Home/RSetsNumber",
        data: {},
        success: comparer.onSuccessRSets,
        error: comparer.onFailedRSets
    });
}

comparer.onSuccessRSets = function (result) {
    $("#rsets-available").empty();
    if (result.success == true) {
        $("#rsets-available").text(result.count);
    } else {
        utils.showError(result.message);
    }
 }

comparer.onFailedRSets = function (req, status, error) {
    $("#rsets-available").empty();
    utils.showError(error);
}



/*
 *  Ping handlers
 */

comparer.onSuccessPing = function (result) {
    $("#ping-result").empty().text(result.ping);
    if (result.success == false) {
        utils.showError(result.message);
    }
}

comparer.onFaildPing = function (req, status, error) {
    $("#ping-result").empty();
    utils.showError(error);
}


$(function () {
    comparer.renderingSets();
    comparer.serverInfo();
    comparer.testConnection();

    var button = $("#ping-button")[0];
    button.onclick = function (target) {
        $("#ping-result").empty().append(utils.Spinner());
        $.ajax({
            type: "POST",
            url: "Home/Ping",
            data: {},
            success: comparer.onSuccessPing,
            error: comparer.onFaildPing
        });
    }
})