$(document).ready(setUp);

function setUp() {
    $("#login_submit").click(savelocalsession);
}

function savelocalsession(event) {
    localStorage.Username = $("#lblUser").val();
    console.log(localStorage.Username);
    localStorage.Password = $("#lblPassword").val();
    console.log(localStorage.Password);
    localStorage.Company = $("#lblCompany").val();
    console.log(localStorage.Company);
}
