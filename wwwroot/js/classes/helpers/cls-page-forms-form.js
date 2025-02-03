/**
 * Class page forms base
 */

'use strict';

export class clsPageFormsForm extends HTMLFormElement {
  constructor(pageForm) {
    super();
    this.PageForm = pageForm;
    // this.Controls = new HTMLCollection<HTMLFormControlElement>();
    console.log(this.elements);
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

window.customElements.define('cls-page-forms-form', clsPageFormsForm, { extends: 'form' });

export default clsPageFormsForm;
