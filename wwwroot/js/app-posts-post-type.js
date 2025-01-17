/**
 * Attribute CRUD JS
 */

'use strict';
// Get the Create for validation
const entityName = "PostType";
const pageModelGetURL = "/Apps/Posts/PostType";
const InputPrefix = "NewEntry";
const EditPrefix = "edit";
const entryNameStartWithSelector = ".post-type-name-";

//todo createFormValidationFields
const createFormValidatorsFields = {

};

//todo editFormValidatorsFields
const editFormValidatorsFields = {
};


//todo HandelEditForm
const handleEditCategoryModal = function (editButton, setFormAttributes, setElementAttributes) {
  //// Get the Attribute details from the table
  const entityId = editButton.id.split('--')[0];
  const TableRow = document.getElementById(`${entityId}--edit${entityName}`).parentElement.parentElement;
  const RowIdx = TableRow.attributes.idx.value;
  //  const Slug = document.querySelector(`${entryNameStartWithSelector}${KeywordId}`).innerText;
  const Name = TableRow.children[2].innerText;

  const editForm = document.getElementById(`edit${entityName}Form`);
  setFormAttributes(editForm, entityId, 'EditOrUpdate');

  $ParentIdSelect.trigger('change');
  $ParentIdSelect.val(ParentId).trigger('change');

  document.getElementById(`edit${InputPrefix}_Name`).value = Name;


}

//todo DataTable Columns Definetions
const dataTableColumnsDefs = [
  {
    // For Responsive Popup Button (plus icon)
    className: 'control',
    searchable: false,
    orderable: false,
    responsivePriority: 2,
    targets: 0,
    visible: true,
    render: function (data, type, full, meta) {
      return '';
    }
  },
  {
    // For Id
    targets: 1,
    responsivePriority: 9
  },
  {
    // For Name
    targets: 2,
    responsivePriority: 3
  },
  {
    // For Actions
    targets: 3,
    searchable: false,
    orderable: false,
    responsivePriority: 1,
    visible: true
  },

  {
    // For DropDown
    targets: -2,
    searchable: false,
    orderable: false,
    responsivePriority: 1
  }
];

//todo DataTable Create Button
const dataTableCreateButton = {
  // For Create Attribute Button (Add New Attribute)
  text: '<i class="ri-add-line ri-16px me-0 me-sm-1_5"></i><span class="d-none d-sm-inline-block">Add ' + entityName + '</span>',
  className: 'add-new btn btn-primary waves-effect waves-light',
  attr: {
    'data-bs-toggle': 'modal',
    'data-bs-target': '#create' + entityName + 'Modal'
  },
  //action: function (e, dt, node, config) {


  //}
};

//#region Swal

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
  //todo Setup Forms Reprater
  var formRepeater = $(".form-repeater");

  var row = 2;
  var col = 1;
  formRepeater.on('submit', function (e) {
    e.preventDefault();
  });
  formRepeater.repeater({
    show: function () {
      var fromControl = $(this).find('.form-control, .form-select');
      var formLabel = $(this).find('.form-label');

      fromControl.each(function (i) {
        var id = 'form-repeater-' + row + '-' + col;
        $(fromControl[i]).attr('id', id);
        $(formLabel[i]).attr('for', id);
        col++;
      });

      row++;

      $(this).slideDown();
    },
    hide: function (e) {
      confirm('Are you sure you want to delete this element?') && $(this).slideUp(e);
    }
  });
  //todo END Setup Forms Reprater

  //todo SETUP THE CRUD PAGE IFEIIs
  const createEntityForm = document.getElementById(`create${entityName}Form`);
  const editEntityForm = document.getElementById(`edit${entityName}Form`);

  SetupCRUDPage(
    entityName,
    entryNameStartWithSelector, // EntryNameStartWithSelector,
    pageModelGetURL,
    editEntityForm,
    editFormValidatorsFields,
    handleEditCategoryModal,
    createEntityForm,
    createFormValidatorsFields);

  PageLoaded();
  // Attribute List DataTable Initialization (For Attribute CRUD Page)
  DataTableFactory.Generate("#PostTypesTable", {
    columnDefs: dataTableColumnsDefs,
    buttons: [dataTableCreateButton]
  });

  new PerfectScrollbar(document.getElementById('post-type-attributes'), {
    wheelPropagation: false
  });
});

// For Modal to close on edit button click
var editEntityOffcanvas = $('#edit' + entityName + 'Modal');

// Event listener for the "Edit" offcanvas opening
editEntityOffcanvas.on('show.bs.modal', function () {
  // Close any open modals
  $('.modal').modal('hide');
});

// Filter Form styles to default size after DataTable initialization
setTimeout(() => {

  $('.dt-buttons').addClass('d-flex align-items-center');
  $('#PostTypesTable_length').addClass('mt-0 mt-md-3 me-2');
}, 300);
