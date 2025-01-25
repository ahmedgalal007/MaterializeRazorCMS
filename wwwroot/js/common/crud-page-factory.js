/**
 * Attribute CRUD JS
 */

'use strict';

const { ready } = require("jquery");

const Field = class {
  constructor(name, type, targets, className, searchable, orderable, responsivePriority, visible, render) {
    this.name = name;
    this.className = className;
    this.type = type;
    this.targets = targets;
    this.searchable = searchable?? false;
    this.orderable = orderable??false;
    this.responsivePriority = responsivePriority??1;
    this.visible = visible ?? true;
    this.render = render;
  }
}

let f1 = new Field("Name","String",2);
const PageFactory = (function ($, options) {
  const InputPrefix = options.InputPrefix ?? "NewEntry";
  const entryNameStartWithSelector = options.entryNameStartWithSelector ?? ".post-type-name-";
  const GenerateColumns = function (RenderRowControl, columns) {
    const _r = [
      {
        // For Responsive Popup Button (plus icon)
        className: 'control',
        searchable: false,
        orderable: false,
        responsivePriority: 2,
        targets: 0,
        visible: true,
        render: RenderRowControl
      },
      {
        // For Id
        targets: 1,
        responsivePriority: 9
      },
      ...columns,
      {
        // For Actions
        targets: options.Columns.length + 2,
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
    return _r;
  }

  const _options = {
    EntityName: options.EntityName,
    PageUrl: options.PageUrl,
    RenderRowControl: options.RenderRowControl ?? function (data, type, full, meta) {
      return '';
    },

    MasterPage: {
      Columns: GenerateColumns(this.RenderRowControl, options.MasterPage.Columns),
      CreateFormValidatorsFields: options.MasterPage?.CreateFormValidatorsFields ?? [],
      EditFormValidatorsFields: options.MasterPage?.EditFormValidatorsFields ?? [],
    },
    DetailsPage: {
      Columns: GenerateColumns(this.RenderRowControl, options.DetailsPage.Columns),
      CreateFormValidatorsFields: options.DetailsPage?.CreateFormValidatorsFields ?? [],
      EditFormValidatorsFields: options.DetailsPage?.EditFormValidatorsFields ?? [],
    }

  };

  return {
    ready: function () {

    },
    loaded: function () { };
    init: function () {
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

      $(document).ready(function () {
        ready();
        SetupMainPage(_options.EntityName, _options.PageUrl, _options.MasterPage.CreateFormValidatorsFields, _options.MasterPage.EditFormValidatorsFields);
        PageLoaded();
        this.loaded();
      });

      // Filter Form styles to default size after DataTable initialization
      setTimeout(() => {
        $('.dt-buttons').addClass('d-flex align-items-center');
        $('#PostTypesTable_length').addClass('mt-0 mt-md-3 me-2');
      }, 300);
    },


  };
}); // (jQuery, op);
const SetupMainPage = function (entityName, pageUrl, EditEntityHandler, createFormValidatorsFields, editFormValidatorsFields) {
  const createEntityForm = document.getElementById(`create${entityName}Form`);
  const editEntityForm = document.getElementById(`edit${entityName}Form`);

  SetupCRUDPage(
    entityName,
    entryNameStartWithSelector, // EntryNameStartWithSelector,
    pageUrl,
    editEntityForm,
    editFormValidatorsFields,
    EditEntityHandler,
    createEntityForm,
    createFormValidatorsFields);

  DataTableFactory.Generate("#" + entityName +"Table", {
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
