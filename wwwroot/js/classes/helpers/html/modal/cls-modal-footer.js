/*
 *  class html modal footer
 */

export class clsHelpersHtmlModalFooter extends HTMLDivElement {
  constructor(modal) {
    super();
    this.render();
  }

  render = function () {
    /**
     *  <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
     */
    this.className = "modal-footer";
    let btnClose = document.createElement("button");
    btnClose.className = "btn btn-outline-secondary";
    btnClose.setAttribute('type', 'button');
    btnClose.setAttribute('data-bs-dismiss', 'modal');
    btnClose.innerText = "Close";
    this.appendChild(btnClose);
    let btnSave = document.createElement("button");
    btnSave.className = "btn btn-outline-secondary";
    btnSave.setAttribute('type', 'button');
    btnSave.innerText = "Save changes";
    this.appendChild(btnSave);
  }
}

window.customElements.define('cls-helpers-html-modal-footer', clsHelpersHtmlModalFooter, { extends: 'div' });

export default clsHelpersHtmlModalFooter;
