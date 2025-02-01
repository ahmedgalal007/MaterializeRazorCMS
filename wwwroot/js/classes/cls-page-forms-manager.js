/**
 * Class page forms validators
 */

'use strict';

export class clsPageFormsManager {
  constructor(page) {
    this.Page = page;
    // this.Notifications = new clsPageNotifications(Page.entityName, Page.entryNameSelector);

    this.CreateForm = document.getElementById(`create${this.Page.entityName}Form`);
    this.EditForm = document.getElementById(`edit${this.Page.entityName}Form`);
    console.log("Run Page initialized");
    this.Page.ready(this.init.bind(this));
    this.setValidationForm(this.CreateForm, this.Page.CreateValidationFields, "new");
    this.setValidationForm(this.EditForm, this.Page.EditValidationFields, "edit");
    document
      .querySelectorAll("[id$='-edit" + this.Page.entityName + "']")
      .forEach(editButton => {
        const entityId = editButton.id.split('--')[0];
        editButton.addEventListener('click', () => this.Page.EditHandler(editButton, this));
      });

  }

  init() {
    // let borderColor, bodyBg, headingColor;
    console.log("Page initialized");
    this.Page.Notifications.checkAndShowSuccessAlert('successFlag', 'Deleted');
    this.Page.Notifications.checkAndShowSuccessAlert(`new${this.Page.entityName}Flag`, 'Created');
    this.Page.Notifications.checkAndShowSuccessAlert(`edit${this.Page.entityName}Flag`, 'Updated');
  }

  
  //#region Helpers
  // Function to set element attributes (asp-for)
  setElementAttributes(element, attribute, value) {
    element.setAttribute(attribute, value);
  }

  // Function to set form attributes (route and action)
  setFormAttributes(form, entityId, createHandlerName) {
    const routeAttribute = 'asp-route-id';
    this.setElementAttributes(form, routeAttribute, entityId);
    form.action = `${baseURL}?handler=${createHandlerName}&id=${entityId}`;
  }

  // Function to submit the form and set the success flag (Set success flags for delete, create and update)
  submitFormAndSetSuccessFlag(form, flagName) {
    form.submit();
    sessionStorage.setItem(flagName, 'true');
  }

  setValidationForm(frm, fields, prefix = "new") {
    console.log("SubmitFormAndSetSuccessFlag : ", this.submitFormAndSetSuccessFlag);
    console.log("FormValidation : ", this.Page.FormValidation);
    this.Page.FormValidation.formValidation(frm, {
      fields: fields,
      plugins: {
        trigger: new this.Page.FormValidation.plugins.Trigger(),
        bootstrap5: new this.Page.FormValidation.plugins.Bootstrap5({
          eleValidClass: 'is-valid',
          rowSelector: function (field, ele) {
            return '.mb-5';
          }
        }),
        submitButton: new this.Page.FormValidation.plugins.SubmitButton({
          // Specify the selector for your submit button
          button: '[type="submit"]'
        }),
        // Submit the form when all fields are valid
        // defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
        autoFocus: new this.Page.FormValidation.plugins.AutoFocus()
      }
    })
      .on('core.form.valid', function () {
        // if fields are valid then
        this.submitFormAndSetSuccessFlag(frm, `${prefix}${this.Page.entityName}Flag`);
      })
      .on('core.form.invalid', function () {
        // if fields are invalid
        return;
      });
  }
  //#endregion

}

export default clsPageFormsManager;
