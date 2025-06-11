/**
 * Class page
 */

'use strict';

import clsPageFormsManager from './cls-page-forms-manager.js';
import clsPageNotifications from './cls-page-notifications.js';
import clsPageDataTable from './cls-page-data-table.js';
import clsPageRepeater from './cls-page-repeater.js';
import { clsResultObject } from './forms/cls-result-object.js';

await import('../../vendor/libs/axios/axios.js').then((ax) => {
  console.log('Load Axios: ', window.axios);
});
export class clsPage {
  #currentPage = 1;
  #take = 7;
  #takeOptions = [7, 10, 15, 20, 50, 100];
  #loadingTable = false;
  #response = new clsResultObject();
  /**
   * 
   * @param {document| globalThis| Global} that
   * @param {string} selector
   * @param {Object} config
   * @param {string} entityName
   * @param {string} baseUrl
   * @param {Function} editHandler
   * @param {Object} frmValidation
   * @param {string} dataTableSelector
   * @param {Object} dataTableOptions
   * @param {Object} createValidationFields
   * @param {Object} editValidationFields
   * @param {string} entryNameSelector
   */
  constructor(
    that,
    selector,
    config,
    entityName,
    baseUrl,
    editHandler,
    frmValidation,
    dataTableSelector,
    dataTableOptions,
    createValidationFields = {},
    editValidationFields = {},
    entryNameSelector = ".post-type-name-"
  ) {
    this.That = that;
    this.Element = document.querySelector(selector);
    this.Config = config;
    this.EntityName = entityName;
    this.BaseUrl = baseUrl;
    this.EntryNameSelector = entryNameSelector;
    this.EditHandler = editHandler;
    this.FormValidation = frmValidation;
    this.DataTableOptions = dataTableOptions;
    this.CreateValidationFields = createValidationFields;
    this.EditValidationFields = editValidationFields;
    this.Notifications = new clsPageNotifications(entityName, entryNameSelector);
    // this.FormManager = new clsPageFormsManager(this);
    this.PageDataTable = new clsPageDataTable(dataTableSelector, this, true);
    this._pageRepeater = null;
    this.ready(() => {
      //this.Element = document.querySelector(selector);
      this.getData.bind(this)();
    });

  }

  ready(fn, ticks = 1) {
    // see if DOM is already available
    if (document.readyState === "complete" || document.readyState === "interactive") {
      // call on next available tick
      setTimeout(fn, ticks);
    } else {
      document.addEventListener("DOMContentLoaded", fn);
    }
  }

  getData() {
    let table = this.PageDataTable.$DataTable;
    let oSettings = table.settings;
    // table.clear();
    //table.columns = [
    //  { data: 'select' },
    //  { data: 'id' },
    //  { data: 'name' },
    //  { data: 'email' },
    //  { data: 'actions' },
    //  { data: 'extra' }
    //];
    // table.processing = true;
    // table.serverSide = true; 
    // table.ajax.url(`/api/PostType/?page=${this.#currentPage}&take=${this.#take}`).load();
    // table.ajax.reload();
    /*axios.get(`/api/PostType/?page=${this.#currentPage}&take=${this.#take}`)
      .then(data => {
        this.#response.data = data;
       
        // table.ajax.json(data.data);
        table.data = data.data;
        table.columns = [
          { data: 'id' },
          { data: 'name' },
          { data: 'email' }
        ];
   
        table.draw();
        
        //for (var i = 0; i < data.data.data.length; i++) {
        //  table.oApi._fnAddData(oSettings, data.data.data[i]);
        //}

        //oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
        //table.fnDraw();
      });*/
  }
  getItemById(itemId) {
    return this.#response.data.find(e => e.id === itemId);
  }
  getItemIndexById(itemId) {
    return this.#response.data.findIndex(e => e.id === itemId);
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

  setRepeater(selector, fnOnShow, fnOnHide) {
    this._pageRepeater = new clsPageRepeater(selector, this, fnOnShow, fnOnHide);
    return this._pageRepeater;
  }
}

export default clsPage;
