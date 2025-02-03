/**
 * PostType CRUD JS
 */

'use strict';


import Page from '/js/classes/cls-page.js';
function editHandler(btn, page) { }
let entityName = "PostType";
const dataTableOptions = {
  controlColumn: {
    visible: true
  },
  columnDefs: [
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
  ],
  controlColumn: {
    visible: false
  },
  buttons: [{
    text: '<i class="ri-add-line ri-16px me-0 me-sm-1_5"></i><span class="d-none d-sm-inline-block">Add ' + entityName + '</span>',
    className: 'add-new btn btn-primary waves-effect waves-light',
    attr: {
      'data-bs-toggle': 'modal',
      'data-bs-target': '#create' + entityName + 'Modal'
    },
    // action : SetupFormRepeater
  }],
  "fnInitComplete": function (oSettings, json) {
    $('.select2').each(function () {
      const $select2 = $(this);
      $select2.select2({
        dropdownParent: $select2.parent(),
        //placeholder: 'select..',
        //allowClear: true 
      });
    });
  }
};
$(document).ready(function () {
  console.log("Page Loaded");
  const page = new Page(this,'#app-page', config, entityName, "/Apps/Posts/PostType", editHandler, FormValidation, "#PostTypesTable", dataTableOptions);
  const { borderColor, bodyBg, headingColor } = page.getConfigColors(isDarkStyle);
  PageLoaded();
  page.setPageRepeater(".form-repeater").on('page-repeater-show', (e, item) => {
    console.log("Repeater", e.data.repeater);
    debugger
    e.data.repeater.UpdateRepeaterControls(item);
    e.data.repeater.row++;
    console.log("RowItem", item);
  });
  //console.log(borderColor, bodyBg, headingColor);

});






