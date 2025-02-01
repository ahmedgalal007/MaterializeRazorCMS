/**
 * PostType CRUD JS
 */

'use strict';


import Page from '/js/classes/cls-page.js';
function editHandler(btn, page) { }
let entityName = "PostType";
const dataTableOptions = {
  columnDefs: [
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
  ],
  buttons: [{
    text: '<i class="ri-add-line ri-16px me-0 me-sm-1_5"></i><span class="d-none d-sm-inline-block">Add ' + entityName + '</span>',
    className: 'add-new btn btn-primary waves-effect waves-light',
    attr: {
      'data-bs-toggle': 'modal',
      'data-bs-target': '#create' + entityName + 'Modal'
    },
    // action : SetupFormRepeater
  }]
};
$(document).ready(function () {
  console.log("Page Loaded");
  const page = new Page(this, config, entityName, "/Apps/Posts/PostType", editHandler, FormValidation, "#PostTypesTable", dataTableOptions);
  const { borderColor, bodyBg, headingColor } = page.getConfigColors(isDarkStyle);
  console.log(borderColor, bodyBg, headingColor);
});


PageLoaded();




