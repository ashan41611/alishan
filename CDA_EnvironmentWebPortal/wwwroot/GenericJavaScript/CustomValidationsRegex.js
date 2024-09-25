//these ENUMS use as e.g
//  CustomRegex.CNIC
var CustomRegex = {

    CNIC: "hello",

    ONLY_APLHABETS:"asdasdad",

    ONLY_NUMBERS:"asdasads"

};

function onlyAlphabetkey(event) {
    if (!((event.which >= 65 && event.which <= 90)
        || (event.which >= 97 && event.which <= 122) || (event.which == 32))) {
        return false;
    }
    else {
        return true;
    }
}

function onlyNumberKey(event) {
    if (!(event.which >= 48 && event.which <= 57)) {
        return false;
    }
    else {
        return true;
    }
}
