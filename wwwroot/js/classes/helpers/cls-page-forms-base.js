/**
 * Class page forms base
 */

'use strict';

import clsPageFormsForm from './cls-page-forms-form.js';
export class clsPageFormsBase extends HTMLDivElement {
  constructor(page, isModal = true) {
  //constructor() {
    super();
    this.Page = page;
    this.Form = new clsPageFormsForm(this);
    /** @type {boolean | undefined} */
    this.IsModal = isModal;
    this.TriggerSelector = "";
    this.FormType = "Base";
    //this.Page.ready(this.init.bind(this),1000);
    this.init();
  }

  init() {
    this.IsModal?this._initModel():this._initOffCanvase();
    document.body.appendChild(this);
    this.id = this._getId();
  }

  _initModel(/** @type {Array<HTMLDivElement> | undefined} */childrens, size = "xl"){
    let dialog = document.createElement('div'),
      content = document.createElement('div');
      this.className = "modal-backdrop fade";
      this.setAttribute('tabindex', '-1');
      this.setAttribute('aria-labelledby', 'modalScrollableTitle');
      this.setAttribute('aria-hidden', true);
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
      this.appendChild(dialog);
    /*`<div class="modal fade" id="createPostTypeModal" tabindex="-1" aria-labelledby="modalScrollableTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-xl" role="document">
          <div class="modal-content">`
    */
  }

  _initOffCanvase = function(){
     this.className = "offcanvas offcanvas-end";
      this.setAttribute('tabindex', '-1');
      this.setAttribute('aria-labelledby', 'createAttributeOffcanvasLabel');
    /*
    `<div class="offcanvas offcanvas-end" tabindex="-1" id="createAttributeOffcanvas" aria-labelledby="createAttributeOffcanvasLabel">`
    */
  }

  _getId = function() {
    return this.FormType.toLowerCase()+this.Page.EntityName+ (this.IsModal?"Modal":"OffCanvase");
  }


  _generateModalHeader = function(lable, text) {
    let header = document.createElement('div'),
      title = document.createElement('h5'),
      btnClose = document.createElement('button');
    header.className = this.IsModal ? "modal-header" : "offcanvas-header border-bottom";
    title.id = "modalScrollableTitle";
    title.className = this.IsModal ? "modal-title" : "offcanvas-title";
    title.innerText = text;
    header.appendChild(title);
    btnClose.className = this.IsModal ? "btn-close" : "btn-close text-reset";
    btnClose.setAttribute('type', 'button');
    btnClose.setAttribute('data-bs-dismiss', this.IsModal ? 'modal' :'offcanvas');
    btnClose.setAttribute('aria-label', lable ?? 'Close');
    header.appendChild(btnClose);
    return header;
  }

  remove = function(){
    this.parentElement.removeChild(this);
  }

  toElement = function () {
    // var string = '<div id="cardId" class="card c9"></div>'
    var string = this.outerHTML;

    var e = /<(.+?)\s/g.exec(string)[1];
    var id = /id="(.+?)"/g.exec(string)[1];
    var cls = /class="(.+?)"/g.exec(string)[1];
    var element = document.createElement(e);
    element.hash = id;
    element.className = cls;
  }
}

window.customElements.define('cls-page-forms-base', clsPageFormsBase, { extends: 'div' });

export default clsPageFormsBase;
