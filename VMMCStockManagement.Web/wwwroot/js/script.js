
// Register Form Validation

$(document).ready(function(){
    $("#register-form").parsley();

    $(function () {
        $('#add_campaign_form').parsley().on('field:validated', function () {
                var ok = $('.parsley-error').length === 0;
                $('.bs-callout-info').toggleClass('formHidden', !ok);
                $('.bs-callout-warning').toggleClass('formHidden', ok);
            })
            .on('form:submit', function () {
                return false;
            });           
    });

});


// Table Plugin Call 
$(document).ready(function(){
    $('table').basictable();
});

// Show Hide Password
function showPassword(){
    const input = document.querySelector('#password');
    input.getAttribute('type') === 'password' ? input.setAttribute('type', 'text') : input.setAttribute('type', 'password');
}

function showPassword2(){
    const input2 = document.querySelector('#password2');
    input2.getAttribute('type') === 'password' ? input2.setAttribute('type', 'text') : input2.setAttribute('type', 'password');
}

// Bootstrap Dropdown Make Select 
document.querySelectorAll('.b_drop_selecMenu .dropdown-item').forEach( function(el) {   
    el.addEventListener('click', function() {
        document.querySelector('.b_drop_select').innerText = el.textContent;
        document.querySelector('.b_drop_selecMenu').value = el.textContent;
    });
});
document.querySelectorAll('.b_drop_selecMenu1 .dropdown-item').forEach( function(el) {    
    el.addEventListener('click', function() {
        document.querySelector('.b_drop_select1').innerText = el.textContent;
        document.querySelector('.b_drop_selecMenu').value = el.textContent;
    });
});

// Sidebar Toggle 
$(document).ready(function(){
    $("#toogle_bar").click(function(e) {
        e.preventDefault();
        $("#sidebar_sh").toggleClass("actives");
        $('#cb_main_content').toggleClass('actives');
    });
})

// Offcanvas responsive sidebar 
$(document).ready(function(){
    $("#mobile_toggle").click(function() {
        $('.mobile_sidebar').toggleClass('mobile_active')
    });
})

// All Select 
$(document).ready(function(){
    //$('.mega_select').selectpicker();
})

$('.selectpicker').attr('data-trigger', 'change').attr('data-required', 'true');

console.log(window.location.hostname)

if (window.location.href == "https://localhost:5001/Dashboard" || window.location.href == "https://localhost:5001/"
    || window.location.href == "https://itasset.righttocare.org/Dashboard" || window.location.href == "https://itasset.righttocare.org") {
    $('#dashboard').addClass('active');
}


//Asset Request
if (window.location.href.match("/AssetRequest/Request") || window.location.href.match("/assetRequest/edit") || window.location.href.match("/assetRequest/view") || window.location.href.match("/AssetRequest") ) {
    $('#request').addClass('active');
}

//Asset Damage
if (window.location.href.match("/AssetDamage/Create") || window.location.href.match("/AssetDamage/View") || window.location.href.match("/AssetDamage")) {
    $('#damage').addClass('active');
}

//Asset Dispensary
if (window.location.href.match("/assetDispensary/view") || window.location.href.match("/AssetDispensary")) {
    $('#dispensary').addClass('active');
} 

//Asset Disposal
if (window.location.href.match("/AssetDisposal/Create") || window.location.href.match("/AssetDisposal/View") || window.location.href.match("/AssetDisposal")) {
    $('#disposal').addClass('active');
}

//Asset Replacement
if (window.location.href.match("/AssetReplacement/Create") || window.location.href.match("/AssetReplacement/View") || window.location.href.match("/AssetReplacement")) {
    $('#replacement').addClass('active');
}

//Asset Repairs
if (window.location.href.match("/AssetsRepairs/Create") || window.location.href.match("/AssetsRepairs")) {
    $('#repair').addClass('active');
}

//Return Asserts
if (window.location.href.match("/TransferAsserts/Create") || window.location.href.match("/TransferAsserts/View") || window.location.href.match("/TransferAsserts")) {
    $('#transfer').addClass('active');
}
//Return Assets
if (window.location.href.match("/ReturnAssets/Create") || window.location.href.match("/ReturnAssets/View") || window.location.href.match("/ReturnAssets")) {
    $('#return').addClass('active');
}




//Adapt MOV Exit Interview Report
if (window.location.href.includes("Report")) {
    document.getElementById("reports").click();

    $("#reports").attr("aria-expanded", "true");
    $('#reports').addClass('active');

    $('#movexitinterviewreport').addClass('active');
}

// select Manage User
if (window.location.href.includes("ManageUser")) {
    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#manageuser').addClass('active');
}

// select countries
if (window.location.href.includes("Country")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#country').addClass('active');
}

// select provinces
if (window.location.href.includes("Provinces")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#provinces').addClass('active');
}

// select districts
if (window.location.href.includes("districts")) {
    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#districts').addClass('active');
}

// select sub-districts
if (window.location.href.includes("SubDistricts")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#ad-sub').addClass('active');
}

// select facilities
if (window.location.href.includes("Facilities")) {
    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#facilities').addClass('active');
}



// select education level
if (window.location.href.includes("EducationLevel")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#educationlevel').addClass('active');
}

// select facility classification
if (window.location.href.includes("FacilityClassification")) {
    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#facilityclassification').addClass('active');
}

// select facility type
if (window.location.href.includes("FacilityType")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#facilitytype').addClass('active');
}

// select designation
if (window.location.href.includes("Designation")) {
    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#designation').addClass('active');
}

// select WorkStatus
if (window.location.href.includes("WorkStatus")) {
    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#workstatus').addClass('active');
}

// select reason visiting
if (window.location.href.includes("ReasonVisiting")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#reasonvisiting').addClass('active');
}

// select vaccination refusing
if (window.location.href.includes("VaccinationRefusing")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#vaccinationrefusing').addClass('active');
}

// select depart service delivery status
if (window.location.href.includes("DepartServiceDeliveryStatus")) {
    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#departservicedeliverystatus').addClass('active');
}

// select vaccination status
if (window.location.href.includes("VaccinationStatus")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#vaccinationstatus').addClass('active');
}

// select gender
if (window.location.href.includes("Gender")) {
    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#gender').addClass('active');
}

// select vaccination period
if (window.location.href.includes("vaccinationPeriod")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#vaccinationperiodid').addClass('active');
}

// select vaccination messaging
if (window.location.href.includes("VaccinationMessaging")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#vaccinationmessaging').addClass('active');
}



// select vaccination period
if (window.location.href.includes("VaccinationPeriod")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#vaccinationperiod').addClass('active');
}

// select vaccination opportunity
if (window.location.href.includes("VaccinationOpportunity")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#vaccinationopportunity').addClass('active');
}

// select sourceinformation
if (window.location.href.includes("SourceInformation")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#sourceinformation').addClass('active');
}

// select covid vaccination turn
if (window.location.href.includes("CovidVaccinationTurn")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#covidvaccinationturn').addClass('active');
}




 //select client vaccince period
if (window.location.href.includes("ClientVaccincePeriod")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#clientvaccinceperiod').addClass('active');
}

// select covid health vaccination
if (window.location.href.includes("CovidHealthVaccination")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#covidhealthvaccination').addClass('active');

}

// select patients covid vaccination
    if (window.location.href.includes("PatientsCovidVaccination")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

        $('#patientscovidvaccination').addClass('active');
    }


//    // select covid inject vaccine
if (window.location.href.includes("CovidInjectVaccine")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#covidinjectvaccine').addClass('active');
    }

//   select patients covid vaccination period
if (window.location.href.includes("PatientsCovidVaccinationPeriod")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#patientscovidvaccinationperiodid').addClass('active');
    }

//    // select covid Inject Vaccine period
if (window.location.href.match("covidInjectVaccinePeriod")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#covidinjectvaccineperiodid').addClass('active');
    }

//    // select Turn Provide Vaccine Health Talks
    if (window.location.href.includes("TurnProvideVaccineHealthTalks")) {

    document.getElementById("admin").click();

    $("#admin").attr("aria-expanded", "true");
    $('#admin').addClass('active');

    $('#turnprovidevaccinehealthtalks').addClass('active');
}










if (window.location.href.includes("dashboard")) {
    $('#dashboard').addClass('active');
}

$(document).ready(function(){

    $(window).on("load", function () {

        $('.cam_edit').on('click', save_onclick);
        $('.cam_edit').on('click', function () {
            $('.camp_view_field input, select').css('border', 'none');
        });  
    
        $('.cam_cancel').on('click', cancel_onclick);
        $('.cam_cancel').on('click', function () {
            $('.camp_view_field input, select').css('border', 'none');
            $('.camp_view_field input, .cam_view_select_edits.bootstrap-select .btn-light').css('border', '0');
            $('.cam_view_select_edits.bootstrap-select .dropdown-menu').css('visibility', 'hidden');
        });
        $('.cam_save').on('click', cancel_onclick);
        $('.cam_save').on('click', function () {
            $('.camp_view_field input, select').css('border', 'none');
            $('.camp_view_field input, .cam_view_select_edits.bootstrap-select .btn-light').css('border', '0');
            $('.cam_view_select_edits.bootstrap-select .dropdown-menu').css('visibility', 'hidden');
        });
    
        $('.cam_edit').on('click', edit_onclick);
        $('.cam_edit').on('click', function () {
            $('.camp_view_field input, .cam_view_select_edits.bootstrap-select .btn-light').css('border', '1px solid #7B7B7B');
            $('.cam_view_select_edits.bootstrap-select .dropdown-menu').css('visibility', 'visible');
        });
        $('.cam_save, .cam_cancel').hide();
    });

    function edit_onclick() {
        setFormMode($(this).closest(".mega_campaign_view form"), 'edit');
    }

    function cancel_onclick() {
        setFormMode($(this).closest(".mega_campaign_view form"), 'view');

        //TODO: Undo input changes?
    }

    function save_onclick() {
        setFormMode($(this).closest(".mega_campaign_view form"), 'view');
        //TODO: Send data to server?
    }

    function setFormMode($form, mode) {
        switch (mode) {
            case 'view':
                $form.find('.cam_save, .cam_cancel').hide();
                $form.find('.cam_edit').show();
                $form.find("input, select").prop("disabled", true);
                break;
            case 'edit':
                $form.find('.cam_save, .cam_cancel').show();
                $form.find('.cam_edit').hide();
                $form.find("input, select").prop("disabled", false);
                break;
        }
    }

})