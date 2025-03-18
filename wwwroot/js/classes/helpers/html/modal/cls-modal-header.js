/*
 *  class html modal header
 */

export class clsHelpersHtmlModalHeader extends HTMLDivElement {
  constructor(modal,isModal, title) {
    super();
    this.IsModal = isModal
    this.render(title);
  }

  render = function (text) {

    this.className = this.IsModal ? "modal-header" : "offcanvas-header border-bottom";
    // let header = document.createElement('div'),
        let title = document.createElement('h5'),
        btnClose = document.createElement('button');
      title.id = "modalScrollableTitle";
      title.className = this.IsModal ? "modal-title" : "offcanvas-title";
      title.innerText = text;
      this.appendChild(title);
      btnClose.className = this.IsModal ? "btn-close" : "btn-close text-reset";
      btnClose.setAttribute('type', 'button');
      btnClose.setAttribute('data-bs-dismiss', this.IsModal ? 'modal' : 'offcanvas');
      btnClose.setAttribute('aria-label', 'Close');
      this.appendChild(btnClose);
      // return header;
    }
}

window.customElements.define('cls-helpers-html-modal-header', clsHelpersHtmlModalHeader, { extends: 'div' });

export default clsHelpersHtmlModalHeader;
