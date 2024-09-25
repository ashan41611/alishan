var Utils = {
    ShowSelectedImage:function(InputFileImage,Img_ID) {
        var tmppath = URL.createObjectURL(InputFileImage.files[0]);
        if (InputFileImage.files && InputFileImage.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#' + Img_ID).attr('src', e.target.result);
            }
            reader.readAsDataURL(InputFileImage.files[0]);
        }
    },
    CheckingNullParms: function (Value) {

        return Value == "" ? null : Value;
    },
    DateFormate: function (Date) {

        return dayjs(Date).format('YYYY-MM-DD');
        //   return Date;
    },
    CurrencyFormate: function (Amount) {

        return Amount;
    },
    DecimlPrecission: function (value,count) {


        return value;
    },    
    DefaultProfileImagePath:"/Area/T_R/Content/tnR_assests/uploadpic.png"
}