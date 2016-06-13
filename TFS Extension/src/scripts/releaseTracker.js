
$(document).ready(function () {
    clearReleaseDisplay();
    $("#workItemId").on("keyup",function () {
        clearReleaseDisplay();
        displayReleases(searchForReleases());
    });
	$("#searchButton").click(function () {
        clearReleaseDisplay();
        displayReleases(searchForReleases());
    });
});

function searchForReleases() {
    var workitemId = $("#workItemId").val();
    $("#error").html("Searching...");
    $.ajax({
        url: "http://releasetracker/api/release/GetAssociatedReleases?workitemid=" + workitemId,
        dataType: "json",
        type: "get", 
        cache: false,
        success: function (data) {
            $("#error").html("");
            displayReleases(data);
        },
        error: function () {
            $("#error").html("API call failed - see browser console.");
        }
    });
}

function displayReleases(releases) {
    if (releases != null) {
        console.log(releases);
        var allReleases = $.parseJSON(releases);
        if (allReleases.length === 0) {
            $("#error").html("No releases found");
        } else {
            $.each(allReleases, function (key, value) {
                if (value != null) {
                    createReleaseDiv(value);
                }
            });
        }
    }
}

function createReleaseDiv(release) {
	release.Created = new Date(release.Created).toLocaleString(); 

	var environments = createEnvironments(release);	

    $("<div class='release'>" +
     "<h3>" + release.DefinitionName + ": " + release.Name + "</h3>" +
     "<ul class='details'>" +
     "<li><strong>Created date:</strong> " + release.Created + "</li>" +
     "<li><strong>Created by:</strong> " + release.CreatedBy + "</li>" +     
     environments +
	 "<li><a class='view-link' href='" + release.Url + "' target='_blank'><strong>View Release</a></strong></li>" +
     "</div>").appendTo("#releases");

}

function createEnvironments(release){
	var environments = "<div class='environments'><h3>Environments</h3><ul class='details'>";

	$.each(release.Environments, function(key, enviro){
		environments += "<li><strong>" + enviro.name + ":</strong> "+ enviro.status + "</li>";	
	})
	
	environments += "</ul></div>";
	
	return environments;
}

function clearReleaseDisplay() {
    $("#releases").html("");
}


