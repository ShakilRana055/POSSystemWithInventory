
function ShowUserInformation(information) {
    let selector = {
        userImage: $("#userImage"),
        imageUrl: "~/images/User/avatar.jpg",
        userName: $("#userName"),
    };
    selector.userName.text(information.name);
    if (information.photoUrl != "") {
        selector.userImage.attr("src", information.photoUrl);
    }
    else {
        selector.userImage.attr("src", selector.imageUrl);
    }
}

function UserInformation() {
    let ajaxOperation = new AjaxOperation();
    let information = ajaxOperation.GetAjax("User/AdminInformation");
    console.log(information);
    let email = "superadmin@gmail.com";
    if (information.email === email) {
        $(".superAdminAccess").show();
    }
    else {
        $(".superAdminAccess").hide();
    }
    ShowUserInformation(information);
}

UserInformation();