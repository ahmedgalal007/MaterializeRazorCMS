/**
 * CRUD Page Setup JS
 */
'use strict';
//#region Swal

// Functions to handle the Delete Attribute Sweet Alerts (Delete Confirmation)
let showDeleteConfirmation;


//#endregion
const SetupCRUDPage = (
  function (
    entityName,
    EntryNameStartWithSelector,
    baseURL,
    editEntityForm,
    editFormValidatorsFields,
    handleEditEntityModal,
    createEntityForm,
    createFormValidatorsFields  // , handleCreateEntityModal
  ) {

    //? Generate DELETE CONFIRMATION Dialog
    showDeleteConfirmation = function (EntryId = "") {
      event.preventDefault(); // prevent form submit
      const EntryName = document.querySelector(`${EntryNameStartWithSelector}${EntryId}`).innerText;
      Swal.fire({
        title: `Delete ${entityName}`,
        // Show the Attribute the Attribute name to be deleted
        html: `<p>Are you sure you want to delete ${entityName} ?<br><br><span class="fw-medium text-danger">${EntryName}</span></p>`,
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
          const form = document.getElementById(EntryId + '-deleteForm');
          if (form) {
            submitFormAndSetSuccessFlag(form, 'successFlag');
          } else {
            console.error('Form element not found');
          }
        } else {
          Swal.fire({
            title: 'Cancelled',
            // Show the Attribute that the Attribute has not been deleted.
            html: `<p><span class="fw-medium text-primary">${EntryName}</span> is not deleted!</p>`,
            icon: 'error',
            confirmButtonText: 'Ok',
            customClass: {
              confirmButton: 'btn btn-success waves-effect waves-light'
            }
          });
        }
      });
    };


    // Function to set element attributes (asp-for)
    function setElementAttributes(element, attribute, value) {
      element.setAttribute(attribute, value);
    }

    // Function to set form attributes (route and action)
    function setFormAttributes(form, entityId, createHandlerName) {
      const routeAttribute = 'asp-route-id';
      setElementAttributes(form, routeAttribute, entityId);
      form.action = `${baseURL}?handler=${createHandlerName}&id=${entityId}`;
    }
    // Function to submit the form and set the success flag (Set success flags for delete, create and update)
    function submitFormAndSetSuccessFlag(form, flagName) {
      form.submit();
      sessionStorage.setItem(flagName, 'true');
    }
    // Sweet Alert Success Function (Attribute Deleted/Created/Updated)
    function showSuccessAlert(message) {
      var name = message[0].toUpperCase() + message.slice(1);
      var entity = entityName[0].toUpperCase() + entityName.slice(1);
      Swal.fire({
        title: name,
        text: `${entity} ${message} Successfully!`,
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

    // Attach event listeners for "Edit Attribute" buttons (pencil icon)
    const editEntityButtons = document.querySelectorAll("[id$='-edit" + entityName + "']");
    editEntityButtons.forEach(editButton => {
      editButton.addEventListener('click', () => handleEditEntityModal(editButton, setFormAttributes));
    });


    // Check and Call the functions to check and display success messages on page reload (for delete, create and update)
    checkAndShowSuccessAlert('successFlag', 'Deleted');
    checkAndShowSuccessAlert('newAttributeFlag', 'Created');
    checkAndShowSuccessAlert('editAttributeFlag', 'Updated');

    //? Create Attribute
    //#region Create Attribute

    // Initialize FormValidation for create Attribute form
    const fv = FormValidation.formValidation(createEntityForm, {
      fields: createFormValidatorsFields,
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
        submitFormAndSetSuccessFlag(createNewEntityForm, `new${entityName}Flag`);
      })
      .on('core.form.invalid', function () {
        // if fields are invalid
        return;
      });
    //#endregion

    //? Edit Attribute
    //#region Edit Attribute
    // Get the Edit form validation

    // Initialize FormValidation for edit Attribute form
    const fv2 = FormValidation.formValidation(editEntityForm, {
      fields: editFormValidatorsFields,
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
        submitFormAndSetSuccessFlag(editEntityForm, `edit${entityName}Flag`);
      })
      .on('core.form.invalid', function () {
        // if fields are invalid
        return;
      });

    //#endregion
  });
//(
//  entityName,
//  EntryNameStartWithSelector,
//  baseURL,
//  editFormValidatorsFields,
//  handleEditEntityModal,
//  createFormValidatorsFields  // , handleCreateEntityModal
//);
