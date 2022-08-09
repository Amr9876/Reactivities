import { useField } from "formik";
import { Form, Label } from "semantic-ui-react";

interface Props {
    name: string;
    placeholder: string;
    type?: string;
    label?: string;
}

export default function MyTextInput(props: Props) {

  const { name, label } = props;

  const [field, meta] = useField(name);


  return (
    <Form.Field error={meta.touched && !!meta.error}>
        <label>{label}</label>
        <input {...field} {...props} />
        {meta.touched && meta.error ? (
            <Label basic color='red'>{meta.error}</Label>
        ) : null}
    </Form.Field>
  )
}