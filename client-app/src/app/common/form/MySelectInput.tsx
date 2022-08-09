import { useField } from "formik";
import { Form, Label, Select } from "semantic-ui-react";

interface Props {
    name: string;
    placeholder: string;
    options: any;
    label?: string;
}

export default function MySelectInput(props: Props) {

    const { name, label, options } = props;

    const [field, meta, helpers] = useField(name);

    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <label>{label}</label>
            <Select 
                clearable
                options={options}
                value={field.value || null}
                onChange={(e, d) => helpers.setValue(d.value)}
                onBlur={() => helpers.setTouched(true)} />
            {meta.touched && meta.error ? (
                <Label basic color='red'>{meta.error}</Label>
            ) : null}
        </Form.Field>
    )
}