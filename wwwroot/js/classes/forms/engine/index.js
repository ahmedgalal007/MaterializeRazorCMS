
import('../../../../vendor/js/lodash.js/lodash.js')
import { inputs, clsModal } from '../../helpers/html/index.js';
export class clsDynamicFormEngine {
  constructor(formBase, parent) {
    this.formContainer = formBase;
    this.dynamicForm = formBase.Form;
    this.parent = parent;
    this.formErrorsContainer = document.createElement('div');
    this.resetButton = document.createElement('button');
    this._generateFormFields();
    this.parent.appendChild(this.dynamicForm);
  }


  _generateFormFieldHTML = function (field) {
    let options = _.merge((new inputs.Options()), field);
    let controlls = options.isGroup() ? inputs['Groups'] : inputs['Basic'];
    let clsInput = controlls.hasOwnProperty(options.typeName) ? controlls[options.typeName] : undefined;
    if (clsInput) return new clsInput(this.formContainer, options.name, options);

    // return html;
  }



  _generateFormFields = function () {
    this.formContainer.innerHTML = ''; // Clear previous fields
    this.formContainer.fields.forEach((field) => {
      const fieldObj = this._generateFormFieldHTML(field);
      if (fieldObj) this.dynamicForm.appendChild(fieldObj);
      // const fieldHTML = generateFormFieldHTML(field);
      // formContainer.insertAdjacentHTML('beforeend', fieldHTML);
    });
  }



//  _validateField = function (fieldName, value) {
//    const fieldSchema = formSchema.fields.find((f) => f.name === fieldName);
//    if (!fieldSchema || !fieldSchema.validation) {
//      return null; // No validation rules
//    }

//    const { required, minLength, maxLength, email, pattern } = fieldSchema.validation;
//    let errorMessage = null;

//    if (required && !value) {
//      errorMessage = `${fieldSchema.label} is required`;
//    } else if (value) {
//      if (minLength && value.length < minLength) {
//        errorMessage = `${fieldSchema.label} must be at least ${minLength} characters`;
//      } else if (maxLength && value.length > maxLength) {
//        errorMessage = `${fieldSchema.label} cannot be more than ${maxLength} characters`;
//      } else if (email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
//        errorMessage = `${fieldSchema.label} must be a valid email address`;
//      } else if (pattern && !pattern.test(value)) {
//        errorMessage = `${fieldSchema.label} is not in the correct format`;
//      }
//    }

//    return errorMessage;
//  }

//  _validateForm = function (formData) {
//    const errors = {};
//    for (const fieldName in formData) {
//      if (formData.hasOwnProperty(fieldName)) { //prevents errors with prototype pollution
//        const value = formData[fieldName];
//        const error = validateField(fieldName, value);
//        if (error) {
//          errors[fieldName] = error;
//        }
//      }
//    }
//    return errors;
//  }

//  _displayFormErrors = function (errors) {
//    formErrorsContainer.innerHTML = ''; // Clear previous errors
//    if (Object.keys(errors).length === 0) {
//      formErrorsContainer.style.display = 'none';
//      return;
//    }

//    let errorListHTML = '<ul>';
//    for (const fieldName in errors) {
//      errorListHTML += `<li>${errors[fieldName]}</li>`;
//    }
//    errorListHTML += '</ul>';
//    formErrorsContainer.innerHTML = errorListHTML;
//    formErrorsContainer.style.display = 'block';
//  }

//  _getFormData = function () {
//  const formData = {};
//  const formElements = dynamicForm.querySelectorAll('input, select, textarea'); //DO NOT use form.elements
//  formElements.forEach(inputElement => {
//    if (inputElement.type === 'checkbox') {
//      formData[inputElement.name] = inputElement.checked;
//    } else if (inputElement.type === 'radio') {
//      if (inputElement.checked) {
//        formData[inputElement.name] = inputElement.value;
//      }
//      // If no radio button is selected, the value will be undefined.
//      // We don't need to do anything here, as the field will be validated later.
//    } else {
//      formData[inputElement.name] = inputElement.value;
//    }
//  });
//  return formData;
//}

//  submit = function () {
//    dynamicForm.addEventListener('submit', (event) => {
//      event.preventDefault();
//      const formData = getFormData();
//      const errors = validateForm(formData);
//      displayFormErrors(errors);

//      if (Object.keys(errors).length === 0) {
//        // Form is valid, handle submission (e.g., send data to server)
//        console.log('Form Data:', formData);
//        alert('Form submitted successfully! (See console for data)');
//        dynamicForm.reset();
//        generateFormFields(); // Reset to default values
//      }
//    });
//  }
//  reset = function () {
//    resetButton.addEventListener('click', (event) => {
//      event.preventDefault();
//      dynamicForm.reset();
//      generateFormFields();
//      formErrorsContainer.style.display = 'none';
//    });
  //  }

}

export default clsDynamicFormEngine;
