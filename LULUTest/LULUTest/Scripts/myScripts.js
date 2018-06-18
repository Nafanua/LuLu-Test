var buttonDisabled = function (lineQuantity) {
    if (lineQuantity === 0) {
        $('#checkoutButton').attr('disabled', true);
    }
};

var redirect = function () {
    window.location.href = "/Book/Index";
};




