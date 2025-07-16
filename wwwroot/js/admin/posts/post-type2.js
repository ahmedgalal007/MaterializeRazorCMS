/**
 * PostType CRUD JS
 */

'use strict';


// import Page from '../../classes/cls-page.js';
import Page from '/js/classes/cls-page.js';
function editHandler(btn, page) { }
let entityName = "Customer"; // "Order";

const dataTableOptions = null;
//const dataTableOptions = {
//  ajax: { url: `/api/PostType?entityName=${entityName}`, type:'GET' },
//  controlColumn: {
//    visible: true
//  },
//  columns: [
//    { data: 'id', className: 'control', render: data => '' },
//    { data: 'id', title: 'Select', render: data => '<input type="checkbox" value="' + data + '" />' },
//    {
//      data: 'id',
//      options: {
//        id: 'id',
//        name: "Id",
//        typeName: 'TextField',
//        size: 'lg',
//        colSize: 8
//      }
//    },
//    {
//      data: 'name',
//      options: {
//        id: 'name',
//        name: "name",
//        typeName: 'TextField',
//        colSize: 6
//      }
//    },
//    {
//      data: 'parent', render: function (data) {
//        return 'ID=' + data.id + ', Name=' + data.name;
//      }
//    },
//    {
//      data: 'email',
//      options: {
//        id: 'emailObj',
//        name: "email",
//        typeName: 'TextField',
//        group: {
//          prepend: { text: '@' }
//        },
//        colSize: 6
//      }
//    },
//    { data: 'id', title: 'actions', searchable: false, orderable: false, render: () => '' },
//    { data: 'id', title: 'extra', searchable: false, orderable: false, render: () => '' }
//  ],
//  columnDefs: [
//    {
//      // For Id
//      targets: 2,
//      responsivePriority: 4

//    },
//    {
//      // For Name
//      targets: 3,
//      responsivePriority: 2
//    },
//    {
//      // For Email
//      targets: 4,
//      responsivePriority: 3

//    },
//    {
//      // For Actions
//      targets: [5,6],
//      responsivePriority: 1,
//      visible: true
//    }
//  ],
//  buttons: [
//    /*{
//    text: '<i class="ri-add-line ri-16px me-0 me-sm-1_5"></i><span class="d-none d-sm-inline-block">Add ' + entityName + '</span>',
//    className: 'add-new btn btn-primary waves-effect waves-light',
//    attr: {
//      'data-bs-toggle': 'modal',
//      'data-bs-target': '#create' + entityName + 'Modal'
//    },
//    // action : SetupFormRepeater
//  }*/
//  ],
//  "fnInitComplete": function (oSettings, json) {
//    $('.select2').each(function () {
//      const $select2 = $(this);
//      $select2.select2({
//        dropdownParent: $select2.parent(),
//        //placeholder: 'select..',
//        //allowClear: true
//      });
//    });
//  }
//};

$(document).ready(function () {
  console.log("Page Loaded");
  const page = new Page(this, '#app-page', config, entityName, "/Apps/Posts/PostType", editHandler, FormValidation, "#PostTypesTable", dataTableOptions);
  const { borderColor, bodyBg, headingColor } = page.getConfigColors(isDarkStyle);
  PageLoaded();
  page.setRepeater(".form-repeater")
    .on('page-repeater-show', (e, item) => {
      console.log("Repeater", e.data.repeater);
      debugger
      e.data.repeater.UpdateRepeaterControls(item);
      e.data.repeater.row++;
      console.log("RowItem", item);
    });
  let scroller = document.getElementById('vertical-scrollbar');
  if (scroller) {
    new PerfectScrollbar(scroller, {
      wheelPropagation: false
    });
  }

  //console.log(borderColor, bodyBg, headingColor);

});






