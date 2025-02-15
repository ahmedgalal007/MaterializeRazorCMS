/*
 *  class html modal 
 */
import clsModalHeader from './cls-modal-header'
import clsModalFooter from './cls-modal-footer'
export class clsHelpersHtmlModal extends HTMLElement{
  constructor(page, size="xl") {
    super();
    this.Page = page;
    this.Size = size;
    this.Header = new clsModalHeader(this);
    this.Footer = new clsModalFooter(this);
    this.Page.ready(this.render.bind(this));
  }

  render = function () {
    let dialog = document.createElement('div'),
      content = document.createElement('div');
    this.className = "modal fade";
    this.setAttribute('tabindex', '-1');
    this.setAttribute('aria-labelledby', 'modalScrollableTitle');
    // this.setAttribute('aria-hidden', true);
    dialog.className = "modal-dialog modal-dialog-scrollable modal-" + this.Size;
    dialog.setAttribute('role', 'document');
    content.className = "modal-content";
    content.appendChild(this._generateModalHeader('Bye', 'Add ' + this.Page.EntityName));
    let modalBody = document.createElement('div')
    modalBody.width = 500;
    modalBody.height = 150;
    modalBody.className = "modal-body";
    content.appendChild(modalBody);
    childrens ? content.appendChild(childrens) : null;
    dialog.appendChild(content);
    this.Page.Element.appendChild(this);
  }

  remove = function () {
    this.parentElement.removeChild(this);
  }
}

export default clsHelpersHtmlModal;
