$(function () {
    $("#dvFacets").hide();
    $("#dvFiltro").hide();
    $("#cleanFilters").hide();


    function clearMarker() {
        if (!!searchApp && !!searchApp.markers)
        {
            for (var i = 0 ; i < searchApp.markers.length ; i++)
            {
                searchApp.markers[i].setMap(null);
            }
        }
        
    }

    function fillFacets(data)
    {
        $("#dvFacets").show();

        var stringFacets = ["city", "state", "neighborhood", "placeType"];
        var facetsProps = Object.keys(data.Facets);

        for (var i = 0; i < facetsProps.length ; i++) {
            var prop = facetsProps[i];
            var dv = $("<div/>").text(prop);

            for (var x = 0 ; x < data.Facets[prop].length; x++) {
                if (data.Facets[prop][x].Value != null) {
                    var a = $("<a/>").text(data.Facets[prop][x].Value);
                    a.attr("href", "#" + data.Facets[prop][x].Value);

                    var href = (stringFacets.indexOf(prop) >= 0) ? prop + " eq '" + data.Facets[prop][x].Value + "'" : prop + " eq " + data.Facets[prop][x].Value + "";
                    a.attr("data-filter", href);

                    a.prepend("<div>");
                    a.click(function () {
                        vl = $(this).attr("data-filter");

                        var Q = $("#Q").val();
                        var Filter = $("#Filter").val();
                        search(Q, Filter + vl);
                    });

                    dv.append(a);
                }
            }

            $("#dvFacets").append(dv);
        }
    }

    function setMarker(data)
    {
        for (var i = 0 ; i < data.Results.length; i++)
        {
            var doc = data.Results[i].Document;

            var position = new google.maps.LatLng(doc.geolocation.Latitude, doc.geolocation.Longitude);
            var marker = new google.maps.Marker({
                position: position
            });

            searchApp.markers.push(marker);

            marker.setMap(searchApp.map);
        }
    }

    function renderResult(data) {
        for (var i = 0 ; i < data.Results.length; i++)
        {
            var doc = data.Results[i].Document;
            
            var dv = $("<div>");
            dv.html(doc.placeType);

            var dormitorios =$("<span>");
            dormitorios.text(doc.numberOfBedrooms + " Dormitórios");
            dormitorios.prepend("<div>");

            var suites = $("<span>");
            suites.text(doc.numberOfBedrooms + " Suítes");
            suites.prepend("<div>");

            var m2 = $("<span>");
            m2.text(doc.floorArea + " m2");
            m2.prepend("<div>");

            var vl = $("<span>");
            vl.text("R$ " + doc.locationValue);
            vl.prepend("<div>");

            var end = $("<span>");
            end.text(doc.streetName + " " + doc.neighborhood + " " + doc.city);
            end.prepend("<div>");

            dv.append(dormitorios);
            dv.append(suites);
            dv.append(m2);
            dv.append(vl);
            dv.append(end);

            $("#results").append(dv);
            $("#results").append("<br>");
        }
    }

    function search(Q, Filter) {
        var form = $("form");

        $.post(form.attr("action"), { Q: Q, Filter: Filter }, function (data) {
            clearMarker();

            $("#results").html(" ");
            $("#dvFacets").html(" ");
            $("#dvResultsCount").html("Resultados encontrados:" + data.Count);

            console.log(data);

            fillFacets(data);
            setMarker(data);
            renderResult(data);
        });
    }

    $("#btnFiltrar").click(function () {
        $("#dvFiltro").show();
    });

    $("#btnPesquisar").click(function () {
        var Q = $("#Q").val();
        var Filter = $("#Filter").val();

        if (Q == "")
            return;

        search(Q, Filter);

        $("#cleanFilters").show();

    });

    $("#cleanFilters").click(function () {

        $("#Q").val("*");

        $("#btnPesquisar").click();

        $("#Q").val("");

    });

});