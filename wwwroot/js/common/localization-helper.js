/**
 * Localization Helper JS
 */
'use strict';
//#region Swal
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
