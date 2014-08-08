if (SUKUL == undefined) var SUKUL;
if (!SUKUL) SUKUL = {};

$(document).ready(function () {
    var weather = new SUKUL.Weather();
    weather.ShowSummaryWeather();
});


SUKUL.Weather = function () {
    var self = this;
    self.ShowSummaryWeather = function () {
        $.ajax({
            url: "/_api/web/lists/GetByTitle('Yahoo%20Weather')/items/?$select=Title,JSONData&$filter=Success eq 1",
            type: "GET",
            headers: { "Accept": "application/json;odata=verbose" },
            success: function (data) {
                var listData = data.d.results;
                var ul = $("<ul style='list-style-type: none; padding-left: 0px;' class='cbs-List'>");
                for (i = 0; i < listData.length; i++) {
                   
                    var listItemData = $.parseJSON(listData[i].JSONData);
                    var htmlStr = "<li style='display: inline;'><div style='width: 320px; float: left; display: table; margin-bottom: 10px; margin-top: 5px;'>";
                    htmlStr += "<a href='#'>";
                    htmlStr += "<div style='float: left; width: 140px; padding-right: 10px;'>";
                    htmlStr += listItemData.query.results.channel.item.description;
                    htmlStr += "</div>";
                    htmlStr += "<div style='float: left; width: 170px'>";
                    htmlStr += "<div class='mtcProfileHeader mtcProfileHeaderP'>" + listItemData.query.results.channel.title + "</div>";
                    htmlStr += "</div></a></div></li>";
                    ul.append($(htmlStr))
                     
                }
                $("#divContentContainer").append(ul);
            },
            error: function (data) {
                alert(data.statusText);
            }
        });
    }

function setImageWidth(imgString, width) {
    var img = $(imgString);
    img.css('width', width);
    return img[0].outerHTML;
}
}