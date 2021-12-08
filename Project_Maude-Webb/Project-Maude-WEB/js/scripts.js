function myFunction() {
    // Declare variables 
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    table = document.getElementById("table");
    tr = table.getElementsByTagName("tr");
  
    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
      td = tr[i].getElementsByTagName("td")[0];
      if (td) {
        txtValue = td.textContent || td.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
          tr[i].style.display = "";
        } else {
          tr[i].style.display = "none";
        }
      } 
    }
  }


function isVerified() {
  // Declare variables 
  var input,  table, tr, td, i, a;
  input = document.getElementById("Verify");
  input = input.checked;
  table = document.getElementById("table");
  tr = table.getElementsByTagName("tr");

  // Loop through all table rows, and hide those who don't match the search query
  for (i = 0; i < tr.length; i++) {
    td = tr[i].getElementsByTagName("td")[4];
    if (td) {
      
      a = td.getAttribute('value');
      if (a == "true" && input == true) {
        tr[i].style.display = "none";
      } else {
        tr[i].style.display = "";
      }
    } 
  }
}
function isFlagged() {
  // Declare variables 
  var input,  table, tr, td, i, a;
  input = document.getElementById("Flagged");
  input = input.checked;
  table = document.getElementById("table");
  tr = table.getElementsByTagName("tr");

  // Loop through all table rows, and hide those who don't match the search query
  for (i = 0; i < tr.length; i++) {
    td = tr[i].getElementsByTagName("td")[5];
    if (td) {
      
      a = td.getAttribute('value');
      if (a == "false" && input == true) {
        tr[i].style.display = "none";
      } else {
        tr[i].style.display = "";
      }
    } 
  }
}

function readURL(input) {
  if (input.files && input.files[0]) {
      var reader = new FileReader();

      reader.onload = function (e) {
          $('#blah')
              .attr('src', e.target.result)
              .width(150)
              .height(200);
      };

      reader.readAsDataURL(input.files[0]);
       
  }
}



  
