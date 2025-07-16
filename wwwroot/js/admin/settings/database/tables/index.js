/**
 * PostType CRUD JS
 */

'use strict';


// import Page from '../../classes/cls-page.js';
import Page from '/js/classes/cls-page.js';
function editHandler(btn, page) { }
let entityName = "Table"; // "Order";  DynamicTableConfig

const dataTableOptions = {
  ajax: {
    type: 'GET',
    url: '/api/admin/database/tables'
  },
  actions: [
    {
      columns: [
        //{ index: 1, data: 'id', className: 'control', render: data => '' },
        //{ index: 2, data: 'id', title: 'Select', render: data => '<input type="checkbox" value="' + data + '" />' },
        {
          index: 3, data: 'tableName', options: {
            id: 'TableName',
            name: "TableName",
            typeName: 'TextField',
            size: 'lg',
            colSize: 8
          }
        },
        {
          index: 4, data: 'entityName', options: {
            id: 'EntityName',
            name: "EntityName",
            typeName: 'TextField',
            size: 'lg',
            colSize: 8
          }
        }
      ],
      name: 'create'
    },
    {
      columns: [
        //{ index: 1, data: 'id', className: 'control', render: data => '' },
        //{ index: 2, data: 'id', title: 'Select', render: data => '<input type="checkbox" value="' + data + '" />' },
        {
          index: 3, data: 'id', options: {
            id: 'id',
            name: "Id",
            typeName: 'TextField',
            size: 'lg',
            colSize: 8
          }
        },
        {
          index: 4, data: 'tableName', options: {
            id: 'TableName',
            name: "TableName",
            typeName: 'TextField',
            size: 'lg',
            colSize: 8
          }
        },
        {
          index: 5, data: 'entityName', options: {
            id: 'EntityName',
            name: "EntityName",
            typeName: 'TextField',
            size: 'lg',
            colSize: 8
          }
        }
      ],
      name: 'update'
    }
  ],
  columnDefs: []
};
$(document).ready(function () {
  console.log("Page Loaded");
  const page = new Page(this, '#app-page', config, entityName, "/Apps/Posts/PostType", editHandler, FormValidation, "#DynamicTableConfigTable", dataTableOptions);
  const { borderColor, bodyBg, headingColor } = page.getConfigColors(isDarkStyle);
  PageLoaded();

});






