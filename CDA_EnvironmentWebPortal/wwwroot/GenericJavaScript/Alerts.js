
//var options = {
//    autoClose: true,
//    progressBar: true,


//};



//$('#successtoast').click(function () {

//    toast.success("Successfull");

//});

//$('#infotoast').click(function () {

//    toast.info("Information");

//});

//$('#warningtoast').click(function () {

//    toast.warning("Warning");

//});

//$('#errortoast').click(function () {

//    toast.error("Error");

//});




//success toast



var options = {
    autoClose: true,
    progressBar: true,
    enableSounds: true,
    sounds: {

        info: "https://res.cloudinary.com/dxfq3iotg/video/upload/v1557233294/info.mp3",
        // path to sound for successfull message:
        success: "https://res.cloudinary.com/dxfq3iotg/video/upload/v1557233524/success.mp3",
        // path to sound for warn message:
        warning: "https://res.cloudinary.com/dxfq3iotg/video/upload/v1557233563/warning.mp3",
        // path to sound for error message:
        error: "https://res.cloudinary.com/dxfq3iotg/video/upload/v1557233574/error.mp3",
    },
};
var toast = new Toasty(options);
toast.configure(options);

//these function use as e.g
//  Alert.Success(parameters)
var Alert = {
    Success: function (msg) {
        toast.success(msg);
    },
    Warning: function (msg) {
        /*alert(msg);*/
        toast.warning(msg);
    },
    Error: function (msg) {
        //alert(msg);
        toast.error(msg);
    },
    Confirmation: function (title, msg) {
        /* alert(msg);*/
        toast.info(msg);
    }
};