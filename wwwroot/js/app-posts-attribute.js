/**
 * Attribute CRUD JS
 */

'use strict';
//#region Swal

// Functions to handle the Delete Attribute Sweet Alerts (Delete Confirmation)
function showDeleteConfirmation(AttributeId) {
  event.preventDefault(); // prevent form submit
  const AttributeName = document.querySelector(`.Attribute-name-full-${AttributeId}`).innerText;
  Swal.fire({
    title: 'Delete Attribute',
    // Show the Attribute the Attribute name to be deleted
    html: `<p>Are you sure you want to delete Attribute ?<br><br><span class="fw-medium text-danger">${AttributeName}</span></p>`,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Delete',
    cancelButtonText: 'Cancel',
    customClass: {
      confirmButton: 'btn btn-primary waves-effect waves-light me-3',
      cancelButton: 'btn btn-label-secondary waves-effect waves-light'
    }
  }).then(result => {
    if (result.isConfirmed) {
      const form = document.getElementById(AttributeId + '-deleteForm');
      if (form) {
        submitFormAndSetSuccessFlag(form, 'successFlag');
      } else {
        console.error('Form element not found');
      }
    } else {
      Swal.fire({
        title: 'Cancelled',
        // Show the Attribute that the Attribute has not been deleted.
        html: `<p><span class="fw-medium text-primary">${AttributeName}</span> is not deleted!</p>`,
        icon: 'error',
        confirmButtonText: 'Ok',
        customClass: {
          confirmButton: 'btn btn-success waves-effect waves-light'
        }
      });
    }
  });
}

// Function to submit the form and set the success flag (Set success flags for delete, create and update)
function submitFormAndSetSuccessFlag(form, flagName) {
  form.submit();
  sessionStorage.setItem(flagName, 'true');
}
//#endregion

(function () {
  // Function to set element attributes (asp-for)
  function setElementAttributes(element, attribute, value) {
    element.setAttribute(attribute, value);
  }

  // Function to set form attributes (route and action)
  function setFormAttributes(form, attributeId, handler) {
    const routeAttribute = 'asp-route-id';
    setElementAttributes(form, routeAttribute, attributeId);
    form.action = `/Apps/Posts/Attribute?handler=${handler}&id=${attributeId}`;
  }

  // Sweet Alert Success Function (Attribute Deleted/Created/Updated)
  function showSuccessAlert(message) {
    var name = message[0].toUpperCase() + message.slice(1);
    Swal.fire({
      title: name,
      text: `Attribute ${message} Successfully!`,
      icon: 'success',
      confirmButtonText: 'Ok',
      confirmButton: false,
      customClass: {
        confirmButton: 'btn btn-success waves-effect waves-light'
      }
    });
  }

  // Function to check for success flag and display success message
  function checkAndShowSuccessAlert(flagName, successMessage) {
    const flag = sessionStorage.getItem(flagName);
    if (flag === 'true') {
      showSuccessAlert(successMessage);
      sessionStorage.removeItem(flagName);
    }
  }


  // Function to handle the "Edit Attribute" Offcanvas Modal
  const handleEditAttributeModal = editButton => {
    // Get the Attribute details from the table
    const AttributeId = editButton.id.split('--')[0];
    const TableRow = document.getElementById(`${AttributeId}--editAttribute`).parentElement.parentElement;
    const AttributeIdx = TableRow.attributes.idx.value;
    //! const AttributeName = document.querySelector(`.Attribute-name-full-${AttributeId}`).innerText;
    const AttributeName = TableRow.children[2].innerText;
    const AttributeDescription = TableRow.children[3].innerText;
    const AttributeSelectedBaseType = TableRow.children[4].innerText;
    const AttributeSelectedReturnType = TableRow.children[5].innerText;
    const AttributeFormat = TableRow.children[6].innerText;

    // Set the form attributes (route and action)
    const editForm = document.getElementById('editAttributeForm');
    setFormAttributes(editForm, AttributeId, 'EditOrUpdate');

    // Set the input asp-for attributes (for model binding)
    setElementAttributes(document.getElementById('EditAttribute_AttributeName'), 'asp-for', `Attributes[${AttributeIdx}].AttributeName`);
    setElementAttributes(document.getElementById('EditAttribute_Description'), 'asp-for', `Attributes[${AttributeIdx}].Description`);
    // setElementAttributes(document.getElementById('EditAttribute_IsVerified'), 'asp-for', `Attributes[${AttributeId}].IsVerified`);
    //setElementAttributes(
    //  document.getElementById('EditAttribute_ContactNumber'),
    //  'asp-for',
    //  `Attributes[${AttributeIdx}].ContactNumber`
    //);
    setElementAttributes(document.getElementById('EditAttribute_SelectedBaseType'), 'asp-for', `Attributes[${AttributeIdx}].SelectedBaseType`);
    setElementAttributes(document.getElementById('EditAttribute_SelectedReturnType'), 'asp-for', `Attributes[${AttributeIdx}].SelectedReturnType`);
    setElementAttributes(document.getElementById('EditAttribute_Format'), 'asp-for', `Attributes[${AttributeIdx}].Format`);

    // Set the input values (for value binding)
    document.getElementById('EditAttribute_AttributeName').value = AttributeName;
    document.getElementById('EditAttribute_Description').value = AttributeDescription;
    // document.getElementById('EditAttribute_IsVerified').checked = JSON.parse(isVerified.toLowerCase());
    // document.getElementById('EditAttribute_ContactNumber').value = AttributeContactNumber;
    document.getElementById('EditAttribute_SelectedBaseType').value = AttributeSelectedBaseType;
    document.getElementById('EditAttribute_SelectedReturnType').value = AttributeSelectedReturnType;
    document.getElementById('EditAttribute_Format').value = AttributeFormat;
  };

  // Attach event listeners for "Edit Attribute" buttons (pencil icon)
  const editAttributeButtons = document.querySelectorAll("[id$='-editAttribute']");
  editAttributeButtons.forEach(editButton => {
    editButton.addEventListener('click', () => handleEditAttributeModal(editButton));
  });


  // Check and Call the functions to check and display success messages on page reload (for delete, create and update)
  checkAndShowSuccessAlert('successFlag', 'Deleted');
  checkAndShowSuccessAlert('newAttributeFlag', 'Created');
  checkAndShowSuccessAlert('editAttributeFlag', 'Updated');

  //? Create Attribute
  //#region Create Attribute
  // Get the Create for validation
  const createNewAttributeForm = document.getElementById('createAttributeForm');

  // Initialize FormValidation for create Attribute form
  const fv = FormValidation.formValidation(createNewAttributeForm, {
    fields: {
      'NewAttribute.Name': {
        validators: {
          notEmpty: {
            message: 'Please enter a Attribute name'
          },
          stringLength: {
            min: 6,
            max: 20,
            message: 'The Attribute name must be more than 6 and less than 20 characters long'
          }
        }
      },
      'NewAttribute.Description': {
        validators: {
          notEmpty: {
            message: 'Please enter an email address'
          },
          emailAddress: {
            message: 'Please enter a valid email address'
          },
          stringLength: {
            max: 50,
            message: 'The email address must be less than 50 characters long'
          }
        }
      },
      'NewAttribute.SelectedBaseType': {
        validators: {
          notEmpty: {
            message: 'Please select a base type'
          }
        }
      },
      'NewAttribute.SelectedReturnType': {
        validators: {
          notEmpty: {
            message: 'Please select a return type'
          }
        }
      }
    },
    plugins: {
      trigger: new FormValidation.plugins.Trigger(),
      bootstrap5: new FormValidation.plugins.Bootstrap5({
        eleValidClass: 'is-valid',
        rowSelector: function (field, ele) {
          return '.mb-5';
        }
      }),
      submitButton: new FormValidation.plugins.SubmitButton({
        // Specify the selector for your submit button
        button: '[type="submit"]'
      }),
      // Submit the form when all fields are valid
      // defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
      autoFocus: new FormValidation.plugins.AutoFocus()
    }
  })
    .on('core.form.valid', function () {
      // if fields are valid then
      submitFormAndSetSuccessFlag(createNewAttributeForm, 'newAttributeFlag');
    })
    .on('core.form.invalid', function () {
      // if fields are invalid
      return;
    });
  //#endregion

  // For phone number input mask with cleave.js (US phone number)
  const phoneMaskList = document.querySelectorAll('.phone-mask');
  if (phoneMaskList) {
    phoneMaskList.forEach(function (phoneMask) {
      new Cleave(phoneMask, {
        phone: true,
        phoneRegionCode: 'US'
      });
    });
  }
  //? Edit Attribute
  //#region Edit Attribute
  // Get the Edit form validation
  const editAttributeForm = document.getElementById('editAttributeForm');

  // Initialize FormValidation for edit Attribute form
  const fv2 = FormValidation.formValidation(editAttributeForm, {
    fields: {
      'Attribute.AttributeName': {
        validators: {
          notEmpty: {
            message: 'Please enter a Attribute name'
          },
          stringLength: {
            min: 6,
            max: 20,
            message: 'The Attribute name must be more than 6 and less than 20 characters long'
          }
        }
      },
      'Attribute.Email': {
        validators: {
          notEmpty: {
            message: 'Please enter an email address'
          },
          emailAddress: {
            message: 'Please enter a valid email address'
          },
          stringLength: {
            max: 50,
            message: 'The email address must be less than 50 characters long'
          }
        }
      },
      'Attribute.ContactNumber': {
        validators: {
          notEmpty: {
            message: 'Please enter a contact number'
          },
          phone: {
            country: 'US',
            message: 'Please enter a valid phone number'
          },
          stringLength: {
            min: 12,
            message: 'The contact number must be 10 characters long'
          }
        }
      },
      'Attribute.SelectedRole': {
        validators: {
          notEmpty: {
            message: 'Please select a role'
          }
        }
      },
      'Attribute.SelectedPlan': {
        validators: {
          notEmpty: {
            message: 'Please select a plan'
          }
        }
      }
    },
    plugins: {
      trigger: new FormValidation.plugins.Trigger(),
      bootstrap5: new FormValidation.plugins.Bootstrap5({
        eleValidClass: 'is-valid',
        rowSelector: function (field, ele) {
          return '.mb-5';
        }
      }),
      submitButton: new FormValidation.plugins.SubmitButton({
        // Specify the selector for your submit button
        button: '[type="submit"]'
      }),
      // Submit the form when all fields are valid
      // defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
      autoFocus: new FormValidation.plugins.AutoFocus()
    }
  })
    .on('core.form.valid', function () {
      // if fields are valid then
      submitFormAndSetSuccessFlag(editAttributeForm, 'editAttributeFlag');
    })
    .on('core.form.invalid', function () {
      // if fields are invalid
      return;
    });

  //#endregion
})();

//? DataTable Attributes
// Attribute DataTable initialization
$(document).ready(function () {
  let borderColor, bodyBg, headingColor;

  if (isDarkStyle) {
    borderColor = config.colors_dark.borderColor;
    bodyBg = config.colors_dark.bodyBg;
    headingColor = config.colors_dark.headingColor;
  } else {
    borderColor = config.colors.borderColor;
    bodyBg = config.colors.bodyBg;
    headingColor = config.colors.headingColor;
  }

  // Attribute List DataTable Initialization (For Attribute CRUD Page)
  $('#attributeTable').DataTable({
    order: [[1, 'desc']],
    displayLength: 7,
    dom:
      // Datatable DOM positioning
      '<"mx-4 d-flex flex-wrap flex-column flex-sm-row gap-2 py-4 py-sm-0"' +
      '<"d-flex align-items-center me-auto"l>' +
      '<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex flex-sm-row align-items-center justify-content-md-end gap-5 ms-n2 ms-md-2 flex-wrap flex-sm-nowrap"fB>' +
      '>t' +
      '<"row mx-4"' +
      '<"col-sm-12 col-md-6"i>' +
      '<"col-sm-12 col-md-6 pb-3 ps-0"p>' +
      '>',
    lengthMenu: [7, 10, 15, 20],
    language: {
      searchPlaceholder: 'Search..',
      search: '',
      searchPlaceholder: 'Search Attribute',
      paginate: {
        next: '<i class="ri-arrow-right-s-line"></i>',
        previous: '<i class="ri-arrow-left-s-line"></i>'
      }
    },
    // Buttons with Dropdown
    buttons: [
      {
        extend: 'collection',
        className: 'btn btn-outline-secondary dropdown-toggle me-4 waves-effect waves-light',
        text: '<i class="ri-download-line ri-16px me-1"></i> <span class="d-none d-sm-inline-block">Export</span>',
        buttons: [
          {
            extend: 'print',
            title: 'Attributes Data',
            text: '<i class="ri-printer-line me-1" ></i>Print',
            className: 'dropdown-item',
            customize: function (win) {
              //customize print view for dark
              $(win.document.body)
                .css('color', config.colors.headingColor)
                .css('border-color', config.colors.borderColor)
                .css('background-color', config.colors.body);

              $(win.document.body)
                .find('table')
                // .addClass('compact')
                .css('color', 'inherit')
                .css('border-color', 'inherit')
                .css('background-color', 'inherit');

              // Center the title "Attributes Data"
              $(win.document.body).find('h1').css('text-align', 'center');
            },
            exportOptions: {
              columns: [1, 2, 3, 4, 5, 6, 7],
              format: {
                body: function (data, row, column, node) {
                  if (column === 1) {
                    var $content = $(data);
                    // Extract the value of data-Attribute-name attribute (Attribute Name)
                    var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                    return AttributeName;
                  } else if (column === 3) {
                    // Extract the value of data-is-verified attribute (Is Verified)
                    var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                    return isVerified === 'True' ? 'Verified' : 'Not Verified';
                  }
                  return data;
                }
              }
            }
          },
          {
            extend: 'csv',
            title: 'Attributes',
            text: '<i class="ri-file-text-line me-1" ></i>Csv',
            className: 'dropdown-item',
            exportOptions: {
              columns: [1, 2, 3, 4, 5, 6, 7],
              format: {
                body: function (data, row, column, node) {
                  if (column === 1) {
                    var $content = $(data);
                    // Extract the value of data-Attribute-name attribute (Attribute Name)
                    var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                    return AttributeName;
                  } else if (column === 3) {
                    // Extract the value of data-is-verified attribute (Is Verified)
                    var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                    return isVerified === 'True' ? 'Verified' : 'Not Verified';
                  }
                  return data;
                }
              }
            }
          },
          {
            extend: 'excel',
            title: 'Attributes',
            text: '<i class="ri-file-excel-line me-1"></i>Excel',
            className: 'dropdown-item',
            exportOptions: {
              columns: [1, 2, 3, 4, 5, 6, 7],
              format: {
                body: function (data, row, column, node) {
                  if (column === 1) {
                    var $content = $(data);
                    // Extract the value of data-Attribute-name attribute (Attribute Name)
                    var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                    return AttributeName;
                  } else if (column === 3) {
                    // Extract the value of data-is-verified attribute (Is Verified)
                    var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                    return isVerified === 'True' ? 'Verified' : 'Not Verified';
                  }
                  return data;
                }
              }
            }
          },
          {
            extend: 'pdf',
            title: 'Attributes',
            text: '<i class="ri-file-pdf-line me-1"></i>Pdf',
            className: 'dropdown-item',
            exportOptions: {
              columns: [1, 2, 3, 4, 5, 6, 7],
              format: {
                body: function (data, row, column, node) {
                  if (column === 1) {
                    var $content = $(data);
                    // Extract the value of data-Attribute-name attribute (Attribute Name)
                    var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                    return AttributeName;
                  } else if (column === 3) {
                    // Extract the value of data-is-verified attribute (Is Verified)
                    var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                    return isVerified === 'True' ? 'Verified' : 'Not Verified';
                  }
                  return data;
                }
              }
            }
          },
          {
            extend: 'copy',
            title: 'Attributes',
            text: '<i class="ri-file-copy-line me-1"></i>Copy',
            className: 'dropdown-item',
            exportOptions: {
              columns: [1, 2, 3, 4, 5, 6, 7],
              format: {
                body: function (data, row, column, node) {
                  if (column === 1) {
                    var $content = $(data);
                    // Extract the value of data-Attribute-name attribute (Attribute Name)
                    var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                    return AttributeName;
                  } else if (column === 3) {
                    // Extract the value of data-is-verified attribute (Is Verified)
                    var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                    return isVerified === 'True' ? 'Verified' : 'Not Verified';
                  }
                  return data;
                }
              }
            }
          }
        ]
      },
      {
        // For Create Attribute Button (Add New Attribute)
        text: '<i class="ri-add-line ri-16px me-0 me-sm-1_5"></i><span class="d-none d-sm-inline-block">Add Attribute</span>',
        className: 'add-new btn btn-primary waves-effect waves-light',
        attr: {
          'data-bs-toggle': 'offcanvas',
          'data-bs-target': '#createAttributeOffcanvas'
        }
      }
    ],
    responsive: true,
    // For responsive popup
    rowReorder: {
      selector: 'td:nth-child(2)'
    },
    // For responsive popup button and responsive priority for Attribute name
    columnDefs: [
      {
        // For Responsive Popup Button (plus icon)
        className: 'control',
        searchable: false,
        orderable: false,
        responsivePriority: 2,
        targets: 0,
        render: function (data, type, full, meta) {
          return '';
        }
      },
      {
        // For Id
        targets: 1,
        responsivePriority: 4
      },
      {
        // For Attribute Name
        targets: 2,
        responsivePriority: 3
      },
      {
        // For Email
        targets: 3,
        responsivePriority: 9
      },
      {
        // For Is Verified
        targets: 4,
        responsivePriority: 5
      },
      {
        // For Contact Number
        targets: 5,
        responsivePriority: 7
      },
      {
        // For Role
        targets: 6,
        responsivePriority: 6
      },
      {
        // For Plan
        targets: 7,
        responsivePriority: 8
      },
      {
        // For Actions
        targets: -1,
        searchable: false,
        orderable: false,
        responsivePriority: 1
      }
    ],
    responsive: {
      details: {
        display: $.fn.dataTable.Responsive.display.modal({
          header: function (row) {
            var data = row.data();
            var $content = $(data[2]);
            // Extract the value of data-Attribute-name attribute (Attribute Name)
            var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
            return 'Details of ' + AttributeName;
          }
        }),
        type: 'column',
        renderer: function (api, rowIdx, columns) {
          var data = $.map(columns, function (col, i) {
            // Exclude the last column (Action)
            if (i < columns.length - 1) {
              return col.title !== ''
                ? '<tr data-dt-row="' +
                col.rowIndex +
                '" data-dt-column="' +
                col.columnIndex +
                '">' +
                '<td>' +
                col.title +
                ':' +
                '</td> ' +
                '<td>' +
                col.data +
                '</td>' +
                '</tr>'
                : '';
            }
            return '';
          }).join('');

          return data ? $('<table class="table mt-3"/><tbody />').append(data) : false;
        }
      }
    }
  });
});

// For Modal to close on edit button click
var editAttributeOffcanvas = $('#editAttributeOffcanvas');

// Event listener for the "Edit" offcanvas opening
editAttributeOffcanvas.on('show.bs.offcanvas', function () {
  // Close any open modals
  $('.modal').modal('hide');
});

// Filter Form styles to default size after DataTable initialization
setTimeout(() => {
  $('.dt-buttons').addClass('d-flex align-items-center');
  $('#AttributeTable_length').addClass('mt-0 mt-md-3 me-2');
}, 300);
