/**
 * Attribute CRUD JS
 */

'use strict';
// Get the Create for validation

const InputPrefix = "NewEntry";
const EditPrefix = "edit";
const entryNameStartWithSelector = ".post-type-name-";





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

  SetupFormRepeater();

  //todo SETUP THE CRUD PAGE IFEIIs
  SetupMainPage();
  $('.select2').each(function () {
    const $select2 = $(this);
    $select2.select2({
      dropdownParent: $select2.parent(),
      //placeholder: 'select..',
      //allowClear: true 
    });
  });

  PageLoaded();
  // Attribute List DataTable Initialization (For Attribute CRUD Page)


  new PerfectScrollbar(document.getElementById('vertical-scrollbar'), {
    wheelPropagation: false
  });
});



// Filter Form styles to default size after DataTable initialization
setTimeout(() => {

  $('.dt-buttons').addClass('d-flex align-items-center');
  $('#PostTypesTable_length').addClass('mt-0 mt-md-3 me-2');
}, 300);

const SetupMainPage = function () {
  const entityName = "PostType";
  const createEntityForm = document.getElementById(`create${entityName}Form`);
  const editEntityForm = document.getElementById(`edit${entityName}Form`);

  //todo HandelEditCategoryForm
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
  //todo createFormValidationFields
  const createFormValidatorsFields = {

  };

  //todo editFormValidatorsFields
  const editFormValidatorsFields = {
  };

  SetupCRUDPage(
    entityName,
    entryNameStartWithSelector, // EntryNameStartWithSelector,
    "/Apps/Posts/PostType",
    editEntityForm,
    editFormValidatorsFields,
    handleEditCategoryModal,
    createEntityForm,
    createFormValidatorsFields);

  DataTableFactory.Generate("#PostTypesTable", {
    columnDefs: dataTableColumnsDefs,
    buttons: [{
      text: '<i class="ri-add-line ri-16px me-0 me-sm-1_5"></i><span class="d-none d-sm-inline-block">Add ' + entityName + '</span>',
      className: 'add-new btn btn-primary waves-effect waves-light',
      attr: {
        'data-bs-toggle': 'modal',
        'data-bs-target': '#create' + entityName + 'Modal'
      },
      // action : SetupFormRepeater
    }]
  });

  // For Modal to close on edit button click
  var editEntityOffcanvas = $('#edit' + entityName + 'Modal');

  // Event listener for the "Edit" offcanvas opening
  editEntityOffcanvas.on('show.bs.modal', function () {
    // Close any open modals
    $('.modal').modal('hide');
  });
}

const SetupFormRepeater = function () {
  const UpdateRepeaterControls = function ($item, row) {
    var fromControl = $item.find('.form-control, .form-select, .form-check-control');
    var formLabel = $item.find('.form-label');
    fromControl.each(function (i) {
      let entity = $(fromControl[i]).data("entity"), field = $(fromControl[i]).data("field");
      var id = `${entity}[${row}]_${field}`;
      var inputName = `${entity}[${row}][${field}]`;
      $(fromControl[i]).attr("data-idx", row),
        $(fromControl[i]).attr('id', id);
      $(fromControl[i]).attr('name', inputName);
      $(formLabel[i]).attr('for', id);
      //col++;
    });
    $item.find('button[data-bs-toggle="collapse"]').attr("data-bs-target", `#collapse-attribute-menu-${row}`).attr("aria-controls", `collapse-attribute-menu-${row}`);
    $item.find('[id^="collapse-attribute-menu-"]').attr("id", `collapse-attribute-menu-${row}`);

    $item.find('.select2').each(function () {
      const $select2 = $(this);
      $select2.select2({
        dropdownParent: $select2.parent()
      });
    });
  }
  //todo Setup Forms Reprater
  var formRepeater = $(".form-repeater");

  var row = 1;
  // var col = 1;
  formRepeater.on('submit', function (e) {
    e.preventDefault();
  });
  formRepeater.repeater({
    show: function () {
      UpdateRepeaterControls($(this), row);
      row++;
      $(this).slideDown();
    },
    hide: function (e) {
      confirm('Are you sure you want to delete this element?') && $(this).slideUp(e);
    },
    ready: function (setIndexes) {
      console.log('drop', this.ready);
    },
  });
  //todo END Setup Forms Reprater

  UpdateRepeaterControls(formRepeater.find("[data-repeater-item]"), 0);

  // const setRepeaterIndexes = function ($items(), getGroupName(), fig.repeaters) {};
}
