// Function to check if the input is a numeric digit (isNumeric)
function isNumeric(evt) {
  var charCode = evt.which ? evt.which : evt.keyCode;
  if (charCode >= 48 && charCode <= 57) {
    return true;
  }
  return false;
}

// Function to Clear the input value or take any other action you prefer
function handleKeyUp(event) {
  var input = event.target;
  var value = input.value;
  if (/^[a-zA-Z]+$/.test(value)) {
    input.value = "";
  }
}

// Function to check if the input is an alphabet (isAlphabet)
function isAlphabet(evt) {
  var charCode = evt.which ? evt.which : evt.keyCode;
  if (
    (charCode >= 65 && charCode <= 90) ||
    (charCode >= 97 && charCode <= 122) ||
    charCode == 32
  ) {
    return true;
  }
  return false;
}



function isNumeric(event) {
  var keyCode = event.which ? event.which : event.keyCode;
  if (keyCode < 48 || keyCode > 57) {
    return false;
  }
  return true;
}

function formatCNIC(input) {
  var value = input.value.replace(/-/g, ""); // Remove existing hyphens
  var formattedValue = "";

  if (value.length > 5) {
    formattedValue += value.substr(0, 5) + "-";
    if (value.length > 12) {
      formattedValue += value.substr(5, 7) + "-";
      formattedValue += value.substr(12, 1);
    } else {
      formattedValue += value.substr(5);
    }
  } else {
    formattedValue = value;
  }

  input.value = formattedValue;
}
