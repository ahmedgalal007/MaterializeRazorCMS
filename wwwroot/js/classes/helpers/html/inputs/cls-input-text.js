/**
 * Class page dataTable
 */

'use strict';

export class clsInputText extends HTMLDivElement {

  constructor(form, parent, name, id, caption, size, rounded=false) {

    //<div class="mb-3">
    //  <label for="largeInput" class="form-label">Large input</label>
    //  <input id="largeInput" class="form-control form-control-lg" type="text" placeholder=".form-control-lg" />
    //</div>
    this.className = "mb-3"
    ///////////////////////////////////////////////
    let label = document.createElement('div');
    input.setAttribute("for", id);
    label.className = "form-label";
    label.innerText = caption ? caption : name;
    this.appendChild(label);
    ///////////////////////////////////////////////
    let input = document.createElement('input');
    input.className = "form-control";
    input.setAttribute("id", id);
    input.setAttribute("name", name);
    input.setAttribute("type", "text");
    input.setAttribute("placeholder", ".form-control-lg");
    if (size == "lg" || size == "sm") {
      input.className = input.className + " form-control-"+ size;
    }
    if (rounded) {
      input.className = input.className + " rounded-pill";
    }
    this.appendChild(input);
    parent.appendChild(this);
  }
}

window.customElements.define('input-text', clsInputText, { extends: 'div' });
export default clsInputText;
