/**
 * Class page forms base
 */

'use strict';

import clsPageFormsForm from './cls-page-forms-form.js';
import { clsPage } from '../cls-page.js';
import { clsModal, inputs } from './html/index.js';
await import('../../../vendor/libs/axios/axios.js').then((ax) => {
  console.log('Load Axios: ', window.axios);
});
export class clsPageFormsBase extends HTMLDivElement {
  /**
   * 
   * @param {clsPage} page
   * @param {string} id
   * @param {Array<inputs.Options>} fields
   * @param {boolean} isModal
   */
  constructor(action, page, id, fields = [], isModal = false, dataItem = {}) {
    //constructor() {
    super();
    this.Page = page;
    this.id = id;
    this.fields = fields;
    this.IsModal = isModal;
    this.dataItem = dataItem;
    this.TriggerSelector = "";
    this.FormActionName = action.name || "Base";
    this.Form = new clsPageFormsForm(this, action);
    this.Page.ready(this.init.bind(this), 1000);
    //this.init();
  }

  init() {

    this.IsModal ? this._initModel() : this._initOffCanvas();
    let modal = new clsModal(this);
    //document.body.appendChild(this);
    this.Page.Element.appendChild(this);
    this.id = this._getId();

    this.addEventListener('hide.bs.modal', function (event) {
      const buttonElement = document.activeElement; // as HTMLElement; // Get the currently focused element
      buttonElement.blur();
      this.blur();
    });
    this.Form.onsubmit = this.OnSubmit;
  }

  _initModel(/** @type {Array<HTMLDivElement> | undefined} */childrens, size = "xl") {

    this.className = "modal fade";
    this.setAttribute('tabindex', '-1');
    this.setAttribute('aria-labelledby', 'modalScrollableTitle');
    // let modal = new clsModal(this);

    // modal.render();
    /*let dialog = document.createElement('div'),
      content = document.createElement('div');
      // this.setAttribute('aria-hidden', true);
      dialog.className = "modal-dialog modal-dialog-scrollable modal-" + size;
      dialog.setAttribute('role', 'document');
      content.className = "modal-content";
      content.appendChild(this._generateModalHeader('Bye', 'Add ' + this.Page.EntityName));
      let modalBody = document.createElement('div')
      modalBody.width = 500;
      modalBody.height = 150;
      modalBody.className = "modal-body";
      content.appendChild(modalBody);
      childrens?content.appendChild(childrens):null;
      dialog.appendChild(content);
      this.appendChild(dialog);*/
    /*`<div class="modal fade" id="createPostTypeModal" tabindex="-1" aria-labelledby="modalScrollableTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-xl" role="document">
          <div class="modal-content">`
    */
  }

  _initOffCanvas = function () {
    this.className = "offcanvas offcanvas-end";
    this.setAttribute('tabindex', '-1');
    this.setAttribute('aria-labelledby', 'createAttributeOffcanvasLabel');
    /*
    `<div class="offcanvas offcanvas-end" tabindex="-1" id="createAttributeOffcanvas" aria-labelledby="createAttributeOffcanvasLabel">`
    */
  }

  submitFormAndSetSuccessFlag = function () {
    this.Form.submit();
    sessionStorage.setItem(this._getId(), 'true');
  }


  _getId = function () {
    return this.FormActionName.toLowerCase() + this.Page.EntityName + (this.IsModal ? "Modal" : "OffCanvas");
  }


  //_generateModalHeader = function(lable, text) {
  //  let header = document.createElement('div'),
  //    title = document.createElement('h5'),
  //    btnClose = document.createElement('button');
  //  header.className = this.IsModal ? "modal-header" : "offcanvas-header border-bottom";
  //  title.id = "modalScrollableTitle";
  //  title.className = this.IsModal ? "modal-title" : "offcanvas-title";
  //  title.innerText = text;
  //  header.appendChild(title);
  //  btnClose.className = this.IsModal ? "btn-close" : "btn-close text-reset";
  //  btnClose.setAttribute('type', 'button');
  //  btnClose.setAttribute('data-bs-dismiss', this.IsModal ? 'modal' :'offcanvas');
  //  btnClose.setAttribute('aria-label', lable ?? 'Close');
  //  header.appendChild(btnClose);
  //  return header;
  //}

  remove = function () {
    this.parentElement.removeChild(this);
  }

  setRepeater(selector, fnOnShow, fnOnHide) {
    this._pageRepeater = new clsPageRepeater(selector, this, fnOnShow, fnOnHide);
    return this._pageRepeater;
  }


  OnSubmit(e) {
    // Store reference to form to make later code easier to read
    const form = e.target;

    // get status message references
    const statusBusy = form.querySelector('.status-busy');
    const statusFailure = form.querySelector('.status-failure');

    //// Get AntiForgery Token
    //fetch('/api/antiforgery/token').then((token) => {
    // Post data using the Fetch API
    let formData = new FormData(form);
    fetch(form.action, {
      method: form.method,
      //headers: {
      //  // 'Accept': 'application/json, text/plain',
      //  // 'Content-Type': 'application/json;charset=UTF-8',
      //  // 'Content-Type': 'multipart/form-data;charset=UTF-8',
      //  // "Content-Type": "application/x-www-form-urlencoded",
      //  "X-XSRF-TOKEN" : token,
      //  "RequestVerificationToken" : token
      //},
      body: formData,
    })
      // We turn the response into text as we expect HTML
      .then((res) => res.text())

      // Let's turn it into an HTML document
      .then((text) => new DOMParser().parseFromString(text, 'text/html'))

      // Now we have a document to work with let's replace the <form>
      .then((doc) => {
        // Create result message container and copy HTML from doc
        const result = document.createElement('div');
        result.innerHTML = doc.body.innerHTML;

        // Allow focussing this element with JavaScript
        result.tabIndex = -1;

        // And replace the form with the response children
        form.parentNode.replaceChild(result, form);

        // Move focus to the status message
        result.focus();
      })
      .catch((err) => {
        // Unlock form elements
        Array.from(form.elements).forEach(
          (field) => (field.disabled = false)
        );

        // Return focus to active element
        lastActive.focus();

        // Hide the busy state
        statusBusy.hidden = false;

        // Show error message
        statusFailure.hidden = false;
      });
    // });


    // Before we disable all the fields, remember the last active field
    const lastActive = document.activeElement;

    // Show busy state and move focus to it
    statusBusy.hidden = false;
    statusBusy.tabIndex = -1;
    statusBusy.focus();

    // Disable all form elements to prevent further input
    Array.from(form.elements).forEach((field) => (field.disabled = true));

    // Make sure connection failure message is hidden
    statusFailure.hidden = true;

    // Prevent the default form submit
    e.preventDefault();
  }

  //toElement = function () {
  //  // var string = '<div id="cardId" class="card c9"></div>'
  //  var string = this.outerHTML;

  //  var e = /<(.+?)\s/g.exec(string)[1];
  //  var id = /id="(.+?)"/g.exec(string)[1];
  //  var cls = /class="(.+?)"/g.exec(string)[1];
  //  var element = document.createElement(e);
  //  element.hash = id;
  //  element.className = cls;
  //}
}

window.customElements.define('cls-page-forms-base', clsPageFormsBase, { extends: 'div' });

export default clsPageFormsBase;
