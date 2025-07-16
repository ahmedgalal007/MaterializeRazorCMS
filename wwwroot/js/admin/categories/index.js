/**
 * Attribute CRUD JS
 */

'use strict';
// Get the Create for validation
const entityName = "Category";
const pageModelGetURL = "/Apps/Categories/List";
const InputPrefix = "NewEntry";
const EditPrefix = "edit";
const entryNameStartWithSelector = ".category-name-";

//todo createFormValidationFields
const createFormValidatorsFields = {
  'NewEntry.CategoryURI': {
    validators: {
      notEmpty: {
        message: 'Please enter a Uri for the Category'
      },
      stringLength: {
        min: 4,
        max: 40,
        message: 'The Category Uri must be more than 6 and less than 20 characters long'
      }
    }
  }
};

//todo editFormValidatorsFields
const editFormValidatorsFields = {
  'NewEntry.CategoryURI': {
    validators: {
      notEmpty: {
        message: 'Please enter a Uri for the Category'
      },
      stringLength: {
        min: 4,
        max: 40,
        message: 'The Category Uri must be more than 6 and less than 20 characters long'
      }
    }
  }
};


//todo HandelEditForm
const handleEditCategoryModal = function (editButton, setFormAttributes, setElementAttributes) {
  //// Get the Attribute details from the table
  const entityId = editButton.id.split('--')[0];
  const TableRow = document.getElementById(`${entityId}--edit${entityName}`).parentElement.parentElement;
  const RowIdx = TableRow.attributes.idx.value;
  //  const Slug = document.querySelector(`${entryNameStartWithSelector}${KeywordId}`).innerText;
  const CategoryURI = TableRow.children[2].innerText;
  const Slug = TableRow.children[3].innerText;
  const ParentId = $(TableRow.children[4]).find('.parentId')[0]?.innerText;
  const Color = $(TableRow.children[5]).find('.color-value')[0]?.innerText;

  // Set the form attributes (route and action)
  const editForm = document.getElementById(`edit${entityName}Form`);
  setFormAttributes(editForm, entityId, 'EditOrUpdate');

  // Set the input values (for value binding)
  var Control_CategoryURI = document.getElementById(`edit${InputPrefix}_CategoryURI`);
  Control_CategoryURI.value = CategoryURI;
  document.getElementById(`edit${InputPrefix}_Slug`).value = Slug;

  let $ParentIdSelect = $(`#edit${InputPrefix}_ParentId`);
  // if ($ParentIdSelect.length < 1) $ParentIdSelect = $(`[data-select2-id="edit${InputPrefix}_ParentId"`)
  $ParentIdSelect.find('option').each(function () {
    $(this).val() == entityId.toUpperCase() ?
      $(this).prop("disabled", true).hide() :
      // $(this).hide() :
      $(this).prop("disabled", false).show();  // .remove();
    // $(this).show();  // .remove();
  });
  $ParentIdSelect.trigger('change');
  $ParentIdSelect.val(ParentId).trigger('change');

  document.getElementById(`edit${InputPrefix}_Color`).value = Color;

  const Locals = JSON.parse(TableRow.children[6].innerText);
  // $(`#create${entityName}Form`).empty();
  LocalizationHelper.init(
    Locals,
    $(`#edit${entityName}Form`).find('ul.nav-tabs'),
    $(`#edit${entityName}Form`).find('.localizations-tabs-contents'),
    "edit");

  EditorsScriptManager.GenerateColorPickers["NewEntry.Color"](pickr, "edit");

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
    // For CategoryURI
    targets: 2,
    responsivePriority: 3
  },
  {
    // For Slug
    targets: 3,
    responsivePriority: 3
  },
  {
    // For ParentId
    targets: 4,
    responsivePriority: 5
  },
  {
    // For Color
    targets: 5,
    responsivePriority: 5
  },
  {
    // For Locals
    targets: 6,
    responsivePriority: 100,
    // visible: false
  },
  {
    // For Actions
    targets: 7,
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
    'data-bs-toggle': 'offcanvas',
    'data-bs-target': '#create' + entityName + 'Offcanvas'
  },
  action: function (e, dt, node, config) {
    const createFormContext = $(`#localization-create-context`);
    const locItem = new Object();
    const inputs = createFormContext.find('.language-tab-content:first input').each(function () {
      let col = $(this).data('column')
      locItem[col] = (col == "LanguageID" ? $(this).data('languageCode') : '')
    });
    const Locals = [];
    Locals.push(locItem);
    // $(`#create${entityName}Form`).empty();
    LocalizationHelper.init(
      Locals,
      $(`#create${entityName}Form`).find('ul.nav-tabs'),
      $(`#create${entityName}Form`).find('.localizations-tabs-contents'),
      "create");

    EditorsScriptManager.GenerateColorPickers["NewEntry.Color"](pickr, "create");

  }
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
  DataTableFactory.Generate("#CategoriesTable", {
    columnDefs: dataTableColumnsDefs,
    buttons: [dataTableCreateButton]
  });

  $('.select2').each(function () {
    const $select2 = $(this);
    $select2.select2({
      dropdownParent: $select2.parent(),
      //placeholder: 'select..',
      //allowClear: true 
    });
  });


});

// For Modal to close on edit button click
var editEntityOffcanvas = $('#edit' + entityName + 'Offcanvas');

// Event listener for the "Edit" offcanvas opening
editEntityOffcanvas.on('show.bs.offcanvas', function () {
  // Close any open modals
  $('.modal').modal('hide');
});

// Filter Form styles to default size after DataTable initialization
setTimeout(() => {

  $('.dt-buttons').addClass('d-flex align-items-center');
  $('#AttributeTable_length').addClass('mt-0 mt-md-3 me-2');
}, 300);
