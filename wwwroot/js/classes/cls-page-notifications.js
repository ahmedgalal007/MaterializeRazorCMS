/**
 * Class page notifications
 */

'use strict';
class clsPageNotifications {
  constructor(entityName, entryNameSelector) {
    this.entityName = entityName;
    this.EntryNameStartWithSelector = entryNameSelector;
  }

  //? Generate DELETE CONFIRMATION Dialog
  showDeleteConfirmation(EntryId = "") {
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
        const form = document.getElementById(EntryId + '--deleteForm');
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

  // Sweet Alert Success Function (Attribute Deleted/Created/Updated)
  showSuccessAlert(message) {
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
  checkAndShowSuccessAlert(flagName, successMessage) {
    const flag = sessionStorage.getItem(flagName);
    if (flag === 'true') {
      this.showSuccessAlert(successMessage);
      sessionStorage.removeItem(flagName);
    }
  }
}

export default clsPageNotifications;
