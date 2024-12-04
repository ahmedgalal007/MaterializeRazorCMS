/**
 * Attribute CRUD JS
 */

'use strict';
// Get the Create for validation
const entityName = "Keyword";
const InputPrefix = "NewEntry";
const EditPrefix = "edit";
const entryNameStartWithSelector = ".keyword-name-";

//todo createFormValidationFields
const createFormValidatorsFields = {
  'NewEntry.KeywordURI': {
    validators: {
      notEmpty: {
        message: 'Please enter a Uri for the keyword'
      },
      stringLength: {
        min: 4,
        max: 40,
        message: 'The keyword Uri must be more than 6 and less than 20 characters long'
      }
    }
  },
  //'NewAttribute.Description': {
  //  validators: {
  //    notEmpty: {
  //      message: 'Please enter an email address'
  //    },
  //    emailAddress: {
  //      message: 'Please enter a valid email address'
  //    },
  //    stringLength: {
  //      max: 50,
  //      message: 'The email address must be less than 50 characters long'
  //    }
  //  }
  //},
  //'NewAttribute.SelectedBaseType': {
  //  validators: {
  //    notEmpty: {
  //      message: 'Please select a base type'
  //    }
  //  }
  //},
  //'NewAttribute.SelectedReturnType': {
  //  validators: {
  //    notEmpty: {
  //      message: 'Please select a return type'
  //    }
  //  }
  //}
};

//todo editFormValidatorsFields
const editFormValidatorsFields = {
  'NewEntry.KeywordURI': {
    validators: {
      notEmpty: {
        message: 'Please enter a Uri for the keyword'
      },
      stringLength: {
        min: 4,
        max: 40,
        message: 'The keyword Uri must be more than 6 and less than 20 characters long'
      }
    }
  },
  //'Attribute.Email': {
  //  validators: {
  //    notEmpty: {
  //      message: 'Please enter an email address'
  //    },
  //    emailAddress: {
  //      message: 'Please enter a valid email address'
  //    },
  //    stringLength: {
  //      max: 50,
  //      message: 'The email address must be less than 50 characters long'
  //    }
  //  }
  //},
  //'Attribute.ContactNumber': {
  //  validators: {
  //    notEmpty: {
  //      message: 'Please enter a contact number'
  //    },
  //    phone: {
  //      country: 'US',
  //      message: 'Please enter a valid phone number'
  //    },
  //    stringLength: {
  //      min: 12,
  //      message: 'The contact number must be 10 characters long'
  //    }
  //  }
  //},
  //'Attribute.SelectedRole': {
  //  validators: {
  //    notEmpty: {
  //      message: 'Please select a role'
  //    }
  //  }
  //},
  //'Attribute.SelectedPlan': {
  //  validators: {
  //    notEmpty: {
  //      message: 'Please select a plan'
  //    }
  //  }
  //}
};
//todo HandelEditForm
const handleEditKeywordModal = function (editButton, setFormAttributes, setElementAttributes) {
  //// Get the Attribute details from the table
  const KeywordId = editButton.id.split('--')[0];
  const TableRow = document.getElementById(`${KeywordId}--editKeyword`).parentElement.parentElement;
  const RowIdx = TableRow.attributes.idx.value;
  //  const Slug = document.querySelector(`${entryNameStartWithSelector}${KeywordId}`).innerText;
  const KeywordURI = TableRow.children[2].innerText;
  const Slug = TableRow.children[3].innerText;
  const Schema = TableRow.children[4].innerText;

  // Set the form attributes (route and action)
  const editForm = document.getElementById(`edit${entityName}Form`);
  setFormAttributes(editForm, KeywordId, 'EditOrUpdate');

  // Set the input asp-for attributes (for model binding)
  setElementAttributes(document.getElementById(`edit${InputPrefix}_KeywordURI`), 'asp-for', `TableItems[${RowIdx}].KeywordURI`);
  setElementAttributes(document.getElementById(`edit${InputPrefix}_Slug`), 'asp-for', `TableItems[${RowIdx}].Slug`);
  setElementAttributes(document.getElementById(`edit${InputPrefix}_Schema`), 'asp-for', `TableItems[${RowIdx}].Schema`);

  // Set the input values (for value binding)
  var Control_KeywordURI = document.getElementById(`edit${InputPrefix}_KeywordURI`);
  Control_KeywordURI.value = KeywordURI;
  document.getElementById(`edit${InputPrefix}_Slug`).value = Slug;
  document.getElementById(`edit${InputPrefix}_Schema`).value = Schema;
}


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
  const createKeywordForm = document.getElementById(`create${entityName}Form`);
  const editKeywordForm = document.getElementById(`edit${entityName}Form`);

  SetupCRUDPage(
    entityName,
    entryNameStartWithSelector, // EntryNameStartWithSelector,
    "/Apps/Keywords/List",
    editKeywordForm,
    editFormValidatorsFields,
    handleEditKeywordModal,
    createKeywordForm,
    createFormValidatorsFields);

  // Attribute List DataTable Initialization (For Attribute CRUD Page)
  $('#KeywordsTable').DataTable({
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
        buttons: [
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
      },
      {
        // For Create Attribute Button (Add New Attribute)
        text: '<i class="ri-add-line ri-16px me-0 me-sm-1_5"></i><span class="d-none d-sm-inline-block">Add Keyword</span>',
        className: 'add-new btn btn-primary waves-effect waves-light',
        attr: {
          'data-bs-toggle': 'offcanvas',
          'data-bs-target': '#createKeywordOffcanvas'
        }
      }
    ],
    responsive: true,
    // For responsive popup
    rowReorder: {
      selector: 'td:nth-child(2)'
    },
    // For responsive popup button and responsive priority for Attribute name
    columnDefs: [
      {
        // For Responsive Popup Button (plus icon)
        className: 'control',
        searchable: false,
        orderable: false,
        responsivePriority: 2,
        targets: 0,
        render: function (data, type, full, meta) {
          return '';
        }
      },
      {
        // For Id
        targets: 1,
        responsivePriority: 4
      },
      {
        // For KeywordURI
        targets: 2,
        responsivePriority: 3
      },
      {
        // For Slug
        targets: 3,
        responsivePriority: 9
      },
      {
        // For Schema
        targets: 4,
        responsivePriority: 5
      },
      {
        // For Actions
        targets: -1,
        searchable: false,
        orderable: false,
        responsivePriority: 1
      }
    ],
    responsive: {
      details: {
        display: $.fn.dataTable.Responsive.display.modal({
          header: function (row) {
            var data = row.data();
            var $content = $(data[2]);
            // Extract the value of data-Attribute-name attribute (Attribute Name)
            var AttributeName = $content.find('[class^="Attribute-name-full-"]').text();
            return 'Details of ' + AttributeName;
          }
        }),
        type: 'column',
        renderer: function (api, rowIdx, columns) {
          var data = $.map(columns, function (col, i) {
            // Exclude the last column (Action)
            if (i < columns.length - 1) {
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
      }
    }
  });




  var formRepeater = $('.form-repeater');

  // Form Repeater
  // ! Using jQuery each loop to add dynamic id and class for inputs. You may need to improve it based on form fields.
  // -----------------------------------------------------------------------------------------------------------------

  function RemoveTabContentsDublication(TabContentsSelector) {
    let TabPans = $(TabContentsSelector);
    let tempIds = [];
    TabPans.each(function () {
      let itemExists = false;
      let elemId = $(this).attr('id');
      tempIds.forEach(function (itm) {

        if (elemId == itm.id) itemExists = true;
      });
      if (!itemExists) tempIds.push({ id: elemId, $el: $(this), checked: false });
    });

    TabPans.each(function () {
      let elemId = $(this).attr('id');
      let temp = [...tempIds].reverse();
      temp.forEach(function (itm) {
        if (elemId == itm.id) {
          if (itm.checked)
            itm.$el.remove();
          itm.checked = true;
        }
      });
    });
  }

  let SelectedLanguage;

  if (formRepeater.length) {
    let firstTab = $(formRepeater[0]).find('.tab-pane')[0];
    firstTab.classList.add("active");
    firstTab.classList.add("show");
    $(formRepeater[0]).find('[role="tablist"]>li>button')[0].classList.add("active");
    var row = 1;
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


        ///////////////////////////////////////////////////////
        var items = formRepeater.find('.language-tab-content[data-repeater-item]')
        var tabLang = formRepeater.find('#inputGroupSelectLanguage')[0].selectedOptions[0].value.split('-')[0];
        let navTabs = formRepeater.find('.nav-tabs');
        items.each(function (i) {

          let arr = $(this).attr('id').split('-');
          let lang = arr[arr.length - 1];
          // let tabControl = formRepeater.find('[data-bs-target="#language-tabs-' + tabLang + '"]');
          let tabControl = formRepeater.find('[data-bs-target="#language-tabs-' + tabLang + '"]');
          if (tabControl.length == 0) {
            formRepeater.find('.nav-item button').each(function () {
              $(this).removeClass('active');
            })
            formRepeater.find('.tab-pane').each(function () {
              $(this).removeClass('active show');
            })

            const newTabId = 'language-tabs-' + tabLang;
            let newLi = $('<li class="nav-item"></li>');
            let newBtn = $('<button class="nav-link active waves-effect" data-bs-toggle="tab" data-bs-target="#' + newTabId + '" role="tab" onclick="() => { SelectedLanguage = ' + tabLang + '; }"></button>');
            newBtn.append('<span class="ri-user-line ri-20px d-sm-none"></span>');
            newBtn.attr('data-bs-target', '#' + newTabId);
            newBtn.append('<span class="d-none d-sm-block">' + tabLang + '</span>');
            newLi.append(newBtn);
            navTabs.prepend(newLi);


            const newTabContent = $('[data-repeater-list="group-a"] div:first-child.tab-pane');
            newTabContent.attr('id', newTabId).addClass('active show');
            const tab = new bootstrap.Tab($(newBtn));
            tab.show();
            // $(this).attr('name', 'language-tabs-' + tabLang);
            // $(this).attr('id', 'language-tabs-' + tabLang).addClass('active');
          }


        });

        RemoveTabContentsDublication('[data-repeater-list="group-a"] .tab-pane');
        //const triggerFirstTabEl = document.querySelector('[data-bs-target="^#language-tabs-"].active');
        // bootstrap.Tab.getInstance(triggerFirstTabEl).show();
        const $activeTabContent = $('.language-tab-content.active');
        // const $activeTab = $('button[data-bs-target="#' + $activeTabContent.attr('id') + '"]');
        const $activeTab = document.querySelectorAll(`button[data-bs-target="#${$activeTabContent.attr('id')}"]`);

        //$('button[data-bs-target^="#language-tabs-"]').each(function () {
        //  // bootstrap.Tab.getInstance($(this)[0]).hide();
        //  $(this).removeClass('active');
        //  $(this).attr('aria-selected','false');
        //})
        $('.language-tab-content').removeClass('active show').attr('aria-selected', 'false');
        $('button[data-bs-target^="#language-tabs-"]').removeClass('active').attr('aria-selected', 'false');

        // if ($activeTabContent.length > 0) $($activeTabContent[$activeTabContent.length - 1]).addClass('active show');
        bootstrap.Tab.getInstance($activeTab[0]).show();
        // $($activeTab).addClass('active').attr('aria-selected', 'true')
        // $($activeTab).trigger('click');

        // const cleanTabContentId = $('[data-repeater-list="group-a"] div:first-child.tab-pane').attr('id');
        // const tempContent = $(`[id='${cleanTabContentId}']`);

        //$('.select2-container').remove();
        //$('.select2.form-select').select2({
        //  placeholder: 'Placeholder text'
        //});
        //$('.select2-container').css('width', '100%');
        //var $this = $(this);
        //select2Focus($this);
        //$('.form-repeater:first .form-select').select2({
        //  dropdownParent: $(this).parent(),
        //  placeholder: 'Placeholder text'
        //});
        //$('.position-relative .select2').each(function () {
        //  $(this).select2({
        //    dropdownParent: $(this).closest('.position-relative')
        //  });
        //});
      }
    });
  }
});

// For Modal to close on edit button click
var editKeywordOffcanvas = $('#editKeywordOffcanvas');

// Event listener for the "Edit" offcanvas opening
editKeywordOffcanvas.on('show.bs.offcanvas', function () {
  // Close any open modals
  $('.modal').modal('hide');
});

// Filter Form styles to default size after DataTable initialization
setTimeout(() => {
  $('.dt-buttons').addClass('d-flex align-items-center');
  $('#AttributeTable_length').addClass('mt-0 mt-md-3 me-2');
}, 300);
