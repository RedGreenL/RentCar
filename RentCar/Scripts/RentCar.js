

// Populate dropdown with cities from curent district
function DropDownCities2() {
    var district_id = $("#DropDownDistrict").val();

    if (district_id == null) {
        return;
    };

    console.log(district_id);
    $.ajax({
        url: '/Client/PopulationCities',
        type: 'POST',
        data:
        {
            id: district_id
        }
        ,
        success: function (result) {

            console.log(result);

            $("#cityDropDown").html("");
            $.each($.parseJSON(result), function (i, city) {
                $("#cityDropDown").append($('<option></option>').val(city.City_ID).html(city.Name))
            })
        },
        error: function (err) {
            alert("error " + err.toString() + err.message);

        }
    });
}

/// Refresh partial view with another models
function redirect(gearID, categoryID, fuelID) {
        $.ajax({
            url: '/Home/SelectCarLIst',
            type: 'POST',
            data:
            {
                gearId: gearID,
                categoryId: categoryID,
                fuelId: fuelID
            }
            ,
            success: function (result) {
                $("#selectCarList").html(result);
            },
            error: function (err) {
                alert("error " + err.toString() + err.message);
            }
        });
}

//function CalculatePrice() {
//    var startData = $("#startData").val();
//    var endData = $("#endData").val();
//    var curentPrice =@(ViewBag.CurentCar.Price)
//    debugger 
//    var curentPrice = parseFloat($("#price").val());
//        $.ajax({
//            url: '/Rents/TotalPriceRent',
//            type: 'POST',
//            data:
//            {
//                startDate: startData,
//                endDate: endData,
//                price: curentPrice
//            }
//            ,
//            success: function (result) {
//                console.log("MDa");
//                $("#price").html(result);
//            },
//            error: function (err) {
//                alert("error " + err.toString() + err.message);
//            }
//        });
//    }
