/**
 * Attribute CRUD JS
 */

'use strict';
// Get the Create for validation
const entityName = "Category";
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

//todo Localization Helper
const LocalizationHelper = {
  Mode: "create",
  LocalsStore: [],
  AddLocalization: function () {
    const frm = $(`#${LocalizationHelper.Mode + entityName}Form`);
    const newLang = frm.find('#inputGroupSelectLanguage').find(":selected").val();
    const newTab = new Object();
    const that = this;
    if (!(LocalizationHelper.IsLocalExists(newLang))) {
      frm.find('.language-tab-content:first input').each(function () {
        const col = $(this).data().column;
        console.log(newLang, ': ', col);
        newTab[col] = col == "LanguageID" ? newLang : "";
      });
      LocalizationHelper.LocalsStore.push(newTab);
      LocalizationHelper.GenerateAllLocalizations(frm.find('ul.nav-tabs'), frm.find('.localizations-tabs-contents'));
    }
  },
  IsHidden: function (prop) {
    return ["LanguageID", "Id"].includes(prop)
  },
  IsLocalExists: function (Lang) {
    let result = false;
    this.LocalsStore.forEach(function (e) {
      if (e.LanguageID == Lang) result = true;
    });
    return result;
  },
  GenerateLocalizationTabHeader: function (tabLang, idx = 0) {
    const newTabId = this.Mode + '-language-tabs-' + tabLang;
    const button = '<button data-lang-code="' + tabLang + '" class="close btn p-0 my-0 mx-2 text-danger" type="button" title="Remove this page">X</button>';
    let newLi = $('<li class="nav-item" role="presentation"></li>');
    let newBtn = $('<button type="button" class="nav-link waves-effect" data-bs-toggle="tab" data-bs-target="#' + newTabId + '" role="tab" aria-controls="' + tabLang + '" aria-selected="false"></button>');
    newBtn.append('<span class="ri-user-line ri-20px d-sm-none"></span>');
    newBtn.attr('data-bs-target', '#' + newTabId);
    newBtn.append('<span class="d-none d-sm-block">' + tabLang + '</span>' + (idx > 0 ? button : ''));
    newLi.append(newBtn);
    return newLi;
  },
  GenerateLocalizationInput: function (langTwoLetters, propName, value = '', idx = 0) {
    return $.parseHTML(`<div class="row g-12 ${(this.IsHidden(propName) ? 'd-sm-none' : '')}">
											<div class="col-md-12">
												<div class="input-group input-group-merge">
													<div class="form-floating form-floating-outline">
														<input type="text"
																	 data-column="${propName}"
																	 data-sort-index="${idx}"
																	 data-language-code="${langTwoLetters}"
																	 name="NewEntry.Locales[${idx}].${propName}"
																	 id="newentry-locales-${langTwoLetters}-${propName}"
																	 class="form-control"
																	 placeholder="please enter ${propName}"
																	 aria-label="${propName}"
																	 aria-describedby="formtabs-${propName}"
																	 value="${value}" />
														<label for="newentry-locales-${langTwoLetters}-${propName}">${propName}</label>
													</div>
													<span class="input-group-text" id="formtabs-${propName}">&#64;${propName}</span>
												</div>
											</div>
										</div>`);
  },
  GenerateLocalizationTabContent: function (langTwoLetters, locl, idx = 0) {
    const tabContentId = this.Mode + '-language-tabs-' + langTwoLetters;
    let tabContent = $('<div id="' + tabContentId + '"  role="tabpanel" class="language-tab-content tab-pane fade" >'); // ' + (idx==0?'show active':'') + '
    for (const [key, value] of Object.entries(locl)) {
      if (typeof value === 'string' || value instanceof String) {
        tabContent.append(this.GenerateLocalizationInput(locl.LanguageID, key, value, idx));
      }
    }
    return tabContent;
  },
  GenerateNewLocalization: function (locl, tabHeaderContainer, tabContentContainer, idx = 0) {
    const Lang = locl.LanguageID; //.substring(0, 2);
    tabContentContainer.append(this.GenerateLocalizationTabContent(Lang, locl, idx));
    tabHeaderContainer.append(this.GenerateLocalizationTabHeader(Lang, idx));
  },
  GenerateAllLocalizations: function (tabHeaderContainer, tabContentContainer) {
    tabHeaderContainer.empty();
    tabContentContainer.empty();
    for (const [index, value] of this.LocalsStore.entries()) {
      this.GenerateNewLocalization(value, tabHeaderContainer, tabContentContainer, index);
    }
    var lastTabEl = tabHeaderContainer.find('li:last-child button');
    lastTabEl.addClass('active');
    tabContentContainer.find('.language-tab-content:last-child').addClass('show active');
    tabHeaderContainer.find('.close').on('click', function () {
      let tabBtn = $(this).parents('button.nav-link');
      let langToRemove = $(this).data('langCode');
      LocalizationHelper.LocalsStore.forEach((loc, index) => {
        if (loc.LanguageID === langToRemove) {
          LocalizationHelper.LocalsStore.splice(index, 1);
        }
      });
      $(this).parents('li').remove();
      $(tabBtn.attr('data-bs-target')).remove();
      //display first tab
      var tabFirst = tabHeaderContainer.find('li:first-child button.nav-link');
      // resetTab();
      bootstrap.Tab.getInstance(tabFirst[0]).show();

      // $(tabFirst.attr('data-bs-target')).addClass('show active')
    });
  },
  init: function (Locals, tabHeaderContainer, tabContentContainer, mode = "create") {
    this.LocalsStore = Locals;
    this.Mode = mode;
    this.GenerateAllLocalizations(tabHeaderContainer, tabContentContainer);
    $('.add-new-Language-btn').on('click', this.AddLocalization);
  }
}

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
  // setElementAttributes(document.getElementById(`edit${InputPrefix}_KeywordURI`), 'asp-for', `TableItems[${RowIdx}].KeywordURI`);
  // setElementAttributes(document.getElementById(`edit${InputPrefix}_Slug`), 'asp-for', `TableItems[${RowIdx}].Slug`);
  // setElementAttributes(document.getElementById(`edit${InputPrefix}_Schema`), 'asp-for', `TableItems[${RowIdx}].Schema`);

  // Set the input values (for value binding)
  var Control_KeywordURI = document.getElementById(`edit${InputPrefix}_KeywordURI`);
  Control_KeywordURI.value = KeywordURI;
  document.getElementById(`edit${InputPrefix}_Slug`).value = Slug;
  document.getElementById(`edit${InputPrefix}_Schema`).value = Schema;

  const Locals = JSON.parse(TableRow.children[5].innerText);
  // $(`#create${entityName}Form`).empty();
  LocalizationHelper.init(
    Locals,
    $(`#edit${entityName}Form`).find('ul.nav-tabs'),
    $(`#edit${entityName}Form`).find('.localizations-tabs-contents'),
    "edit");

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
        // For KeywordURI
        targets: 2,
        responsivePriority: 3
      },
      {
        // For Slug
        targets: 3,
        responsivePriority: 3
      },
      {
        // For Schema
        targets: 4,
        responsivePriority: 5
      },
      {
        // For Locals
        targets: 5,
        responsivePriority: 100,
        // visible: false
      },
      {
        // For Actions
        targets: 6,
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
    responsive: {
      details: {
        display: $.fn.dataTable.Responsive.display.modal({
          header: function (row) {
            var data = row.data();
            var $content = $(data[3]);
            // Extract the value of data-Attribute-name attribute (Attribute Name)
            var AttributeName = $content.find('[class^="keyword-name-"]').text();
            return 'Details of ' + AttributeName;
          }
        }),
        type: 'column',
        renderer: function (api, rowIdx, columns) {
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
      }
    }
  });

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
