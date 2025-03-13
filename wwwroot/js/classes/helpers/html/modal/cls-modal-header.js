/*
 *  class html modal header
 */

export class clsHelpersHtmlModalHeader extends HTMLElement {
  constructor(modal) {
    super();
  }

  render = function (lable, text) {
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
      btnClose.setAttribute('data-bs-dismiss', this.IsModal ? 'modal' : 'offcanvas');
      btnClose.setAttribute('aria-label', lable ?? 'Close');
      header.appendChild(btnClose);
      return header;
    }
}

export default clsHelpersHtmlModalHeader;
