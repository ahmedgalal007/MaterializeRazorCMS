export class clsInputGroupOptions {
  constructor(prepend='',append='') {
    this.Preppend = prepend;
    this.Append = append;
  }
}
export class clsSelectOptions {
  constructor(label = '', value = '') {
    this.Lable = label;
    this.Value = value.length > 0 ? value : label;
  }
}

export class clsFieldValidationOptions {
  constructor(required = false, email = false, minLength = 0, maxLength=0, pattern='') {
    this.Required = required;
    this.Email = email;
    this.minLength = minLength;
    this.maxLength = maxLength;
    this.Required = required;
    this.Pattern = pattern;
  }
}
export class clsFieldOptions {
  constructor(type = 'text', name = 'name', label = 'Name', placeholder = '...', validation = {}, defaultValue = null, options = [], fields = [], inputGroup = {}) {
    this.Type = type;
    this.Name = name;
    this.Label = label;
    this.Placeholder = placeholder;
    this.Validation = validation;
    this.DefaultValue = defaultValue;
    if (options.length > 0) this.Options = options;
    if (fields.length > 0) this.Fields = fields;
    if (Object.keys(inputGroup).length > 0) this.InputGroup = inputGroup;
  }
}


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
