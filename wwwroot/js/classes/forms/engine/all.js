const formSchema = {
            fields: [
                {
                    type: 'text',
                    name: 'name',
                    label: 'Name',
                    placeholder: 'Enter your name',
                    validation: {
                        required: true,
                        minLength: 2,
                        maxLength: 100,
                    },
                },
                {
                    type: 'email',
                    name: 'email',
                    label: 'Email',
                    placeholder: 'Enter your email',
                    validation: {
                        required: true,
                        email: true,
                    },
                },
                {
                    type: 'select',
                    name: 'role',
                    label: 'Role',
                    options: [
                        { label: 'Admin', value: 'admin' },
                        { label: 'Editor', value: 'editor' },
                        { label: 'Viewer', value: 'viewer' },
                    ],
                    validation: {
                        required: true,
                    },
                },
                {
                    type: 'textarea',
                    name: 'bio',
                    label: 'Bio',
                    placeholder: 'Enter your bio',
                    validation: {
                        required: true,
                        minLength: 10,
                        maxLength: 200,
                    },
                },
                {
                    type: 'checkbox',
                    name: 'newsletter',
                    label: 'Subscribe to newsletter',
                    defaultValue: true,
                },
                 {
                    type: 'radio',
                    name: 'gender',
                    label: 'Gender',
                    options: [
                      { label: 'Male', value: 'male' },
                      { label: 'Female', value: 'female' },
                      { label: 'Other', value: 'other' },
                    ],
                    validation: {
                      required: true,
                    },
                },
                {
                    type: 'date',
                    name: 'birthdate',
                    label: 'Birthdate',
                    placeholder: 'Select your birthdate',
                    validation: {
                        required: true,
                    },
                },
                {
                  type: 'group',
                  name: 'social',
                  label: 'Social Links',
                  fields: [
                    {
                      type: 'text',
                      name: 'facebook',
                      label: 'Facebook',
                      placeholder: 'Enter Facebook URL'
                    },
                    {
                      type: 'text',
                      name: 'twitter',
                      label: 'Twitter',
                      placeholder: 'Enter Twitter URL'
                    }
                  ]
                },
                {
                    type: 'text',
                    name: 'phone',
                    label: 'Phone Number',
                    placeholder: 'Enter your phone number',
                    validation: {
                        required: true,
                        pattern: /^\d{10}$/, // Example: 10-digit phone number
                    },
                    inputGroup: {
                        prepend: '+1', // Example: Prepend "+1" for US phone numbers
                    }
                },
            ],
        };

        const formContainer = document.getElementById('form-fields-container');
        const dynamicForm = document.getElementById('dynamic-form');
        const formErrorsContainer = document.getElementById('form-errors');
        const resetButton = document.getElementById('reset-form');

        function generateFormFields() {
            formContainer.innerHTML = ''; // Clear previous fields
            formSchema.fields.forEach((field) => {
                const fieldHTML = generateFormFieldHTML(field);
                formContainer.insertAdjacentHTML('beforeend', fieldHTML);
            });
        }


        function generateFormFieldHTML(field) {
            let html = '';
            const fieldId = `field-${field.name}`;
            const labelContent = field.label ? `<label for="${fieldId}" class="form-group-label">${field.label}</label>` : '';

            switch (field.type) {
                case 'text':
                case 'email':
                case 'date':
                    if (field.inputGroup) {
                        html = `
                            <div class="form-group">
                                ${labelContent}
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">${field.inputGroup.prepend}</span>
                                    </div>
                                    <input
                                        type="${field.type}"
                                        id="${fieldId}"
                                        name="${field.name}"
                                        class="form-control"
                                        placeholder="${field.placeholder || ''}"
                                        value="${field.defaultValue || ''}"
                                    />
                                </div>
                            </div>
                        `;
                    } else if (field.type === 'date') {
                         html = `
                            <div class="form-group">
                                ${labelContent}
                                <div class="date-input-container">
                                     <span class="date-input-icon">&#128194;</span>
                                    <input
                                        type="date"
                                        id="${fieldId}"
                                        name="${field.name}"
                                        class="form-control"
                                        placeholder="${field.placeholder || ''}"
                                        value="${field.defaultValue || ''}"
                                    />
                                </div>
                            </div>
                        `;
                    }

                    else {
                        html = `
                            <div class="form-group">
                                ${labelContent}
                                <input
                                    type="${field.type}"
                                    id="${fieldId}"
                                    name="${field.name}"
                                    class="form-control"
                                    placeholder="${field.placeholder || ''}"
                                    value="${field.defaultValue || ''}"
                                />
                            </div>
                        `;
                    }
                    break;
                case 'textarea':
                    html = `
                        <div class="form-group">
                            ${labelContent}
                            <textarea
                                id="${fieldId}"
                                name="${field.name}"
                                class="form-control"
                                placeholder="${field.placeholder || ''}"
                                rows="4"
                            >${field.defaultValue || ''}</textarea>
                        </div>
                    `;
                    break;
                case 'select':
                    html = `
                        <div class="form-group">
                            ${labelContent}
                            <select id="${fieldId}" name="${field.name}" class="form-select">
                                <option value="">Select ${field.label}</option>
                                ${field.options
                                    .map(
                                        (option) => `
                                            <option value="${option.value}" ${field.defaultValue === option.value ? 'selected' : ''}>
                                                ${option.label}
                                            </option>
                                        `
                                    )
                                    .join('')}
                            </select>
                        </div>
                    `;
                    break;
                case 'checkbox':
                    html = `
                        <div class="form-check">
                            <input
                                type="checkbox"
                                id="${fieldId}"
                                name="${field.name}"
                                class="form-check-input"
                                ${field.defaultValue ? 'checked' : ''}
                            />
                            <label for="${fieldId}" class="form-check-label">
                                ${field.label}
                            </label>
                        </div>
                    `;
                    break;
                case 'radio':
                    html = `
                        <div class="form-group">
                            ${labelContent}
                            ${field.options
                                .map(
                                    (option, index) => `
                                        <div class="form-check">
                                            <input
                                                type="radio"
                                                id="${fieldId}-${index}"
                                                name="${field.name}"
                                                class="form-check-input"
                                                value="${option.value}"
                                                ${field.defaultValue === option.value ? 'checked' : ''}
                                            />
                                            <label for="${fieldId}-${index}" class="form-check-label">
                                                ${option.label}
                                            </label>
                                        </div>
                                    `
                                )
                                .join('')}
                        </div>
                    `;
                    break;
                case 'group':
                    html = `<div class="form-group">
                                <label class="form-group-label">${field.label}</label>
                            <div style="border: 1px solid #e2e8f0; padding: 1rem; border-radius: 0.375rem; margin-bottom: 1rem;">`;
                    field.fields.forEach(nestedField => {
                        html += generateFormFieldHTML(nestedField);
                    });
                    html += `</div></div>`;
                    break;
                default:
                    html = `
                        <div class="form-group">
                            ${labelContent}
                            <input
                                type="text"
                                id="${fieldId}"
                                name="${field.name}"
                                class="form-control"
                                placeholder="Unknown field type: ${field.type}"
                                disabled
                            />
                        </div>
                    `;
            }

            return html;
        }

        function validateField(fieldName, value) {
            const fieldSchema = formSchema.fields.find((f) => f.name === fieldName);
            if (!fieldSchema || !fieldSchema.validation) {
                return null; // No validation rules
            }

            const { required, minLength, maxLength, email, pattern } = fieldSchema.validation;
            let errorMessage = null;

            if (required && !value) {
                errorMessage = `${fieldSchema.label} is required`;
            } else if (value) {
                if (minLength && value.length < minLength) {
                    errorMessage = `${fieldSchema.label} must be at least ${minLength} characters`;
                } else if (maxLength && value.length > maxLength) {
                    errorMessage = `${fieldSchema.label} cannot be more than ${maxLength} characters`;
                } else if (email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
                    errorMessage = `${fieldSchema.label} must be a valid email address`;
                } else if (pattern && !pattern.test(value)) {
                    errorMessage = `${fieldSchema.label} is not in the correct format`;
                }
            }

            return errorMessage;
        }

        function validateForm(formData) {
            const errors = {};
            for (const fieldName in formData) {
              if (formData.hasOwnProperty(fieldName)) { //prevents errors with prototype pollution
                const value = formData[fieldName];
                const error = validateField(fieldName, value);
                if (error) {
                    errors[fieldName] = error;
                }
              }
            }
            return errors;
        }

        function displayFormErrors(errors) {
            formErrorsContainer.innerHTML = ''; // Clear previous errors
            if (Object.keys(errors).length === 0) {
                formErrorsContainer.style.display = 'none';
                return;
            }

            let errorListHTML = '<ul>';
            for (const fieldName in errors) {
                errorListHTML += `<li>${errors[fieldName]}</li>`;
            }
            errorListHTML += '</ul>';
            formErrorsContainer.innerHTML = errorListHTML;
            formErrorsContainer.style.display = 'block';
        }

        function getFormData() {
            const formData = {};
             const formElements = dynamicForm.querySelectorAll('input, select, textarea'); //DO NOT use form.elements
             formElements.forEach(inputElement => {
              if (inputElement.type === 'checkbox') {
                formData[inputElement.name] = inputElement.checked;
              } else if (inputElement.type === 'radio') {
                if (inputElement.checked) {
                  formData[inputElement.name] = inputElement.value;
                }
                // If no radio button is selected, the value will be undefined.
                // We don't need to do anything here, as the field will be validated later.
              } else {
                formData[inputElement.name] = inputElement.value;
              }
            });
            return formData;
        }

        dynamicForm.addEventListener('submit', (event) => {
            event.preventDefault();
            const formData = getFormData();
            const errors = validateForm(formData);
            displayFormErrors(errors);

            if (Object.keys(errors).length === 0) {
                // Form is valid, handle submission (e.g., send data to server)
                console.log('Form Data:', formData);
                alert('Form submitted successfully! (See console for data)');
                dynamicForm.reset();
                generateFormFields(); // Reset to default values
            }
        });

        resetButton.addEventListener('click', (event) => {
            event.preventDefault();
            dynamicForm.reset();
            generateFormFields();
            formErrorsContainer.style.display = 'none';
        });

        // Generate form fields on page load
        generateFormFields();
