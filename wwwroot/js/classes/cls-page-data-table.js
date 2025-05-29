/**
 * Class page dataTable
 */

'use strict';

import clsPageFormsEdit from './cls-page-forms-edit.js'
import clsPageFormsCreate from './cls-page-forms-create.js'
export class clsPageDataTable {

  constructor(selector, page, isModal) {
    this.Selector = selector;
    this.Page = page;
    this.IsModal = !!isModal ? true : page.IsModal || false;
    let fields = this.getFormFieldsOptions();
    this.EditForm = new clsPageFormsEdit(this.Page, this.generateFormId('edit'), fields, this.IsModal);
    this.CreateForm = new clsPageFormsCreate(this.Page, this.generateFormId('create'), fields, this.IsModal);
    this.Page.DataTableOptions.buttons = [...this.defaultDataTableOptions.buttons, ...this.Page.DataTableOptions.buttons, ...this._getCreateButton()];
    //this.Page.DataTableOptions.buttons = this.Page.DataTableOptions.buttons
    //  .slice().unshift(this._getCreateButton());
    this.Page.ready(this.init.bind(this));
  }

  init() {
    const $ = window.jQuery;
    if (this.Page.DataTableOptions.controlColumn?.visible) {
      this.Page.DataTableOptions.columnDefs = this.Page.DataTableOptions.columnDefs
        .slice()
        .unshift(this.getControlColumn(this.Page.DataTableOptions.controlColumn?.render));
    }
    $(this.Selector).DataTable({ ...this.defaultDataTableOptions, ...this.Page.DataTableOptions })
  }

  getFormFieldsOptions() {
    let options = [];
    this.Page.DataTableOptions.columnDefs.map(field => {
      if (field.options != null && field.options != undefined && Object.keys(field.options).length > 0) {
        options.push(field.options);
      }
    });
    return options;
  }
  getControlColumn(renderFunction) {
    return {
      // For Responsive Popup Button (plus icon)
      className: 'control',
      searchable: false,
      orderable: false,
      responsivePriority: 2,
      targets: 0,
      visible: true,
      render: renderFunction || function (data, type, full, meta) {
        return '';
      }
    };
  }
  generateFormId = (prefix) => prefix + this.Page.EntityName + (this.IsModal ? 'Modal' : 'OffCanvas')
  _getCreateButton = function() {
    return [{
      text: '<i class="ri-add-line ri-16px me-0 me-sm-1_5"></i><span class="d-none d-sm-inline-block">Add ' + this.Page.EntityName + '</span>',
      className: 'add-new btn btn-primary waves-effect waves-light',
      attr: {
        'data-bs-toggle': this.IsModal ? 'modal' :'offcanvas',
        'data-bs-target': '#' +  this.generateFormId('create')
      }
      //, action : SetupFormRepeater
    }];
  }

  defaultDataTableOptions = {
    order: [[1, 'desc']],
    displayLength: 7,
    dom:
      // Datatable DOM positioning
      '<"mx-4 d-flex flex-wrap flex-column flex-sm-row gap-2 py-4 py-sm-0"' +
      '<"d-flex align-items-center me-auto"l>' +
      '<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex flex-sm-row align-items-center justify-content-md-end gap-5 ms-n2 ms-md-2 flex-wrap flex-sm-nowrap"fB>' +
      '>t' +
      '<"row mx-4"' +
      '<"col-sm-12 col-md-6"i>' +
      '<"col-sm-12 col-md-6 pb-3 ps-0"p>' +
      '>',
    lengthMenu: [7, 10, 15, 20],
    language: {
      searchPlaceholder: 'Search..',
      search: '',
      searchPlaceholder: 'Search Attribute',
      paginate: {
        next: '<i class="ri-arrow-right-s-line"></i>',
        previous: '<i class="ri-arrow-left-s-line"></i>'
      }
    },
    // Buttons with Dropdown
    buttons: [
      {
        extend: 'collection',
        className: 'btn btn-outline-secondary dropdown-toggle me-4 waves-effect waves-light',
        text: '<i class="ri-download-line ri-16px me-1"></i> <span class="d-none d-sm-inline-block">Export</span>',
        buttons: this.exportButtons()
      }
    ],
    responsive: true,
    // For responsive popup
    rowReorder: {
      selector: 'td:nth-child(2)'
    },
    // For responsive popup button and responsive priority for Attribute name
    columnDefs: [],
    responsive: {
      details: {
        display: this.responsiveDisplay,
        type: 'column',
        renderer: this.responsiveRender
      }
    }

  }


  responsiveRender(api, rowIdx, columns) {
    var data = $.map(columns, function (col, i) {
      // Exclude the last column (Action)
      if (i < columns.length - 2) {
        return col.title !== ''
          ? '<tr data-dt-row="' +
          col.rowIndex +
          '" data-dt-column="' +
          col.columnIndex +
          '">' +
          '<td>' +
          col.title +
          ':' +
          '</td> ' +
          '<td>' +
          col.data +
          '</td>' +
          '</tr>'
          : '';
      }
      return '';
    }).join('');

    return data ? $('<table class="table mt-3"/><tbody />').append(data) : false;
  }

  responsiveDisplay = $.fn.dataTable.Responsive.display.modal({
    header: function (row) {
      var data = row.data();
      var $content = $(data[3]);
      // Extract the value of data-Attribute-name attribute (Attribute Name)
      var AttributeName = $content.find('[class^="keyword-name-"]').text();
      return 'Details of ' + AttributeName;
    }
  })

  exportButtons() {
    return [
      {
        extend: 'print',
        title: 'Attributes Data',
        text: '<i class="ri-printer-line me-1" ></i>Print',
        className: 'dropdown-item',
        customize: function (win) {
          //customize print view for dark
          $(win.document.body)
            .css('color', config.colors.headingColor)
            .css('border-color', config.colors.borderColor)
            .css('background-color', config.colors.body);

          $(win.document.body)
            .find('table')
            // .addClass('compact')
            .css('color', 'inherit')
            .css('border-color', 'inherit')
            .css('background-color', 'inherit');

          // Center the title "Attributes Data"
          $(win.document.body).find('h1').css('text-align', 'center');
        },
        exportOptions: {
          columns: [1, 2, 3, 4, 5, 6, 7],
          format: {
            body: function (data, row, column, node) {
              if (column === 1) {
                var $content = $(data);
                // Extract the value of data-Attribute-name attribute (Attribute Name)
                var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                return AttributeName;
              } else if (column === 3) {
                // Extract the value of data-is-verified attribute (Is Verified)
                var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                return isVerified === 'True' ? 'Verified' : 'Not Verified';
              }
              return data;
            }
          }
        }
      },
      {
        extend: 'csv',
        title: 'Attributes',
        text: '<i class="ri-file-text-line me-1" ></i>Csv',
        className: 'dropdown-item',
        exportOptions: {
          columns: [1, 2, 3, 4, 5, 6, 7],
          format: {
            body: function (data, row, column, node) {
              if (column === 1) {
                var $content = $(data);
                // Extract the value of data-Attribute-name attribute (Attribute Name)
                var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                return AttributeName;
              } else if (column === 3) {
                // Extract the value of data-is-verified attribute (Is Verified)
                var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                return isVerified === 'True' ? 'Verified' : 'Not Verified';
              }
              return data;
            }
          }
        }
      },
      {
        extend: 'excel',
        title: 'Attributes',
        text: '<i class="ri-file-excel-line me-1"></i>Excel',
        className: 'dropdown-item',
        exportOptions: {
          columns: [1, 2, 3, 4, 5, 6, 7],
          format: {
            body: function (data, row, column, node) {
              if (column === 1) {
                var $content = $(data);
                // Extract the value of data-Attribute-name attribute (Attribute Name)
                var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                return AttributeName;
              } else if (column === 3) {
                // Extract the value of data-is-verified attribute (Is Verified)
                var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                return isVerified === 'True' ? 'Verified' : 'Not Verified';
              }
              return data;
            }
          }
        }
      },
      {
        extend: 'pdf',
        title: 'Attributes',
        text: '<i class="ri-file-pdf-line me-1"></i>Pdf',
        className: 'dropdown-item',
        exportOptions: {
          columns: [1, 2, 3, 4, 5, 6, 7],
          format: {
            body: function (data, row, column, node) {
              if (column === 1) {
                var $content = $(data);
                // Extract the value of data-Attribute-name attribute (Attribute Name)
                var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                return AttributeName;
              } else if (column === 3) {
                // Extract the value of data-is-verified attribute (Is Verified)
                var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                return isVerified === 'True' ? 'Verified' : 'Not Verified';
              }
              return data;
            }
          }
        }
      },
      {
        extend: 'copy',
        title: 'Attributes',
        text: '<i class="ri-file-copy-line me-1"></i>Copy',
        className: 'dropdown-item',
        exportOptions: {
          columns: [1, 2, 3, 4, 5, 6, 7],
          format: {
            body: function (data, row, column, node) {
              if (column === 1) {
                var $content = $(data);
                // Extract the value of data-Attribute-name attribute (Attribute Name)
                var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
                return AttributeName;
              } else if (column === 3) {
                // Extract the value of data-is-verified attribute (Is Verified)
                var isVerified = /data-is-verified="(.*?)"/.exec(data)[1];
                return isVerified === 'True' ? 'Verified' : 'Not Verified';
              }
              return data;
            }
          }
        }
      }
    ]
  }
}

export default clsPageDataTable;
