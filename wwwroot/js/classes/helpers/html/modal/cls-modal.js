/*
 *  class html modal 
 */
import clsModalHeader from './cls-modal-header.js'
import clsModalFooter from './cls-modal-footer.js'
import { clsPageFormsBase } from '../../cls-page-forms-base.js'
import { clsDynamicFormEngine  } from '../../../forms/engine/index.js';
import { inputs } from '../index.js';
export class clsHelpersHtmlModal extends HTMLDivElement{
  /**
   * 
   * @param {clsPageFormsBase} form
   * @param {string} size
   */
  constructor(form, size="xl") {
    super();
    this.Form = form;
    this.Size = size;
    this.Header = new clsModalHeader(this, true, 'Add ' + this.Form.Page.EntityName);
    this.Footer = new clsModalFooter(this);
    // this.Page.ready(this.render.bind(this));
    this.render();
    // this.Childrens = new NodeList();
  }

  render = function () {
    let  content = document.createElement('div');
    // this.setAttribute('aria-hidden', true);
    this.className = "modal-dialog modal-dialog-scrollable modal-" + this.Size;
    this.setAttribute('role', 'document');
    content.className = "modal-content";
    // content.appendChild(this._generateModalHeader('Bye', 'Add ' + this.Page.EntityName));
    content.appendChild(this.Header);
    let modalBody = document.createElement('div')
    modalBody.width = 500;
    modalBody.height = 150;
    modalBody.className = "modal-body";

    // Add Ststus Divs
    let statusFailure = document.createElement('div');
    statusFailure.setAttribute('role', 'alert');
    statusFailure.className = "status-failure";
    statusFailure.innerHTML = "Connection failure, please try again.";
    this.Form.Form.appendChild(statusFailure);
    let statusBusy = document.createElement('div');
    statusBusy.setAttribute('role', 'alert');
    statusBusy.className = "status-busy";
    statusBusy.innerHTML = "Busy sending data, please wait.";
    this.Form.Form.appendChild(statusBusy);
    statusFailure.hidden = true;
    statusBusy.hidden = true;
    new clsDynamicFormEngine(this.Form, modalBody);
    //let bTxt = new inputs.Basic.TextField(this.Form, "BasicTestName", {});
    //modalBody.appendChild(bTxt);
    //let bDate = new inputs.Basic.DateField(this.Form, "BasicDateField", {size:'lg'});
    //modalBody.appendChild(bDate);
    //let gTxt = new inputs.Groups.TextField(this.Form, "GroupTestName", { validation: { text: 'this is a reguired field!' } });
    //modalBody.appendChild(gTxt);
    //let vTxt = new inputs.Groups.TextField(this.Form, "TestName", { validation: { text: 'this is a reguired field!' }, group: { prepend: { text: '@' } } });
    //modalBody.appendChild(vTxt);

    content.appendChild(modalBody);
    this.childNodes.forEach(nd => {
      content.appendChild(nd);
    })

    
    // content.appendChild(this.Footer);
    this.Form.Form.appendChild(this.Footer);
    this.appendChild(content);
    this.Form.appendChild(this);
  }

  remove = function () {
    this.parentElement.removeChild(this);
  }
}
window.customElements.define('cls-helpers-html-modal', clsHelpersHtmlModal, { extends: 'div' });

export default clsHelpersHtmlModal;
