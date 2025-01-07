import React from 'react';
import { FieldRenderProps } from 'react-final-form';
import { Form, Label } from 'semantic-ui-react';
import { DateTimePicker } from 'react-widgets';

interface IProps extends FieldRenderProps<Date, HTMLInputElement> {
    id?: string;
    width?: number;
    placeholder?: string;
    showDate?: boolean;
    showTime?: boolean;
}

const DateInput: React.FC<IProps> = ({
    input,
    width,
    placeholder = 'Select date and/or time',
    showDate = true,
    showTime = true,
    meta: { touched, error },
    ...rest
}) => {
    return (
        <Form.Field error={touched && !!error} width={width as any}>
            <DateTimePicker
                placeholder={placeholder}
                value={input.value ? new Date(input.value) : null} // Ensure the value is a valid Date object
                onChange={input.onChange}
                onBlur={input.onBlur}
                {...(showDate && showTime ? {} : { format: showDate ? 'yyyy-MM-dd' : 'hh:mm tt' })} // Format for date or time only
                {...rest}
            />
            {touched && error && (
                <Label basic color="red">
                    {error}
                </Label>
            )}
        </Form.Field>
    );
};

export default DateInput;
