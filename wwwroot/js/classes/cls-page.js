/**
 * Class page 
 */

'use strict';

import clsPageFormsManager from './cls-page-forms-manager.js';
import clsPageNotifications from './cls-page-notifications.js'
import clsPageDataTable from './cls-page-data-table.js'
class clsPage {
  constructor(
    that,
    config,
    entityName,
    baseUrl,
    editHandler,
    frmValidation,
    dataTableSelector,
    dataTableOptions,
    createValidationFields = {},
    editValidationFields = {},
    entryNameSelector = ".post-type-name-") {
    this.That = that;
    this.Config = config;
    this.entityName = entityName;
    this.BaseUrl = baseUrl;
    this.entryNameSelector = entryNameSelector;
    this.EditHandler = editHandler;
    this.FormValidation = frmValidation;
    this.DataTableOptions = dataTableOptions;
    this.CreateValidationFields = createValidationFields;
    this.EditValidationFields = editValidationFields;
    this.Notifications = new clsPageNotifications(entityName, entryNameSelector);
    this.FormManager = new clsPageFormsManager(this);
    this.PageDataTable = new clsPageDataTable(dataTableSelector,this);
  }

  ready(fn, ticks=1) {
    // see if DOM is already available
    if (document.readyState === "complete" || document.readyState === "interactive") {
      // call on next available tick
      setTimeout(fn, ticks);
    } else {
      document.addEventListener("DOMContentLoaded", fn);
    }
  }

  getConfigColors(isDarkStyle) {
    if (isDarkStyle) {
      return {
        borderColor: this.Config.colors_dark.borderColor,
        bodyBg: this.Config.colors_dark.bodyBg,
        headingColor: this.Config.colors_dark.headingColor
      }
    } else {
      return {
        borderColor: this.Config.colors.borderColor,
        bodyBg: this.Config.colors.bodyBg,
        headingColor: this.Config.colors.headingColor
      }
    }
  }
}

export default clsPage;
