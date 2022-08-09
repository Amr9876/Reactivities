import { observer } from 'mobx-react-lite';
import { ChangeEvent, useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom';
import { Button, Form, Header, Segment } from 'semantic-ui-react'
import LoadingComponent from '../../../app/layout/LoadingComponent';
import { ActivityFormValues } from '../../../app/models/activity';
import { useStore } from '../../../app/stores/store';
import { Formik } from 'formik';
import * as Yup from 'yup'; 
import MyTextInput from '../../../app/common/form/MyTextInput';
import MyTextArea from '../../../app/common/form/MyTextArea';
import MySelectInput from '../../../app/common/form/MySelectInput';
import { categoryOptions } from '../../../app/common/options/categoryOptions';
import MyDateInput from '../../../app/common/form/MyDateInput';
import { useNavigate } from 'react-router-dom';
import { v4 as uuid } from 'uuid';
import NavBar from '../../../app/layout/NavBar';

export default observer(function ActivityForm() {

  const { activityStore } = useStore();
  const { createActivity, updateActivity, loading, loadActivity, loadingInitial } = activityStore;

  const { id } = useParams<'id'>();

  const navigate = useNavigate();

  const [activity, setActivity] = useState<ActivityFormValues>(new ActivityFormValues());

  const validationSchema = Yup.object({
    title: Yup.string().required('the activity is required'),
    description: Yup.string().required('the activity description is required'),
    category: Yup.string().required(),
    date: Yup.string().required('Date is required').nullable(),
    venue: Yup.string().required(),
    city: Yup.string().required(),
  });

  useEffect(() => {
    if (id) loadActivity(id)
            .then(activity => setActivity(new ActivityFormValues(activity)));
            
  }, [id, loadActivity]);

  function handleFormSubmit(activity: ActivityFormValues) {
    if(activity!.id!.length === 0) {
      let newActivity = { ...activity, id: uuid() };
      createActivity(newActivity).then(() => navigate(`/activities/${newActivity.id}`));
    } else {
      updateActivity(activity).then(() => navigate(`/activities/${activity.id}`));
    }
  }

  function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
    event.preventDefault();

    const { name, value } = event.target;

    setActivity({ ...activity, [name]: value });

  }

  if (loadingInitial) return <LoadingComponent content='Loading...' />;

  return (
    <>

    <NavBar />

    <Segment clearing>
        <Header content='Activity Details' sub color='teal' />
        <Formik 
            validationSchema={validationSchema}
            enableReinitialize
            initialValues={activity} 
            onSubmit={values => handleFormSubmit(values)}>
          {({ handleSubmit, isValid, isSubmitting, dirty }) => (
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <MyTextInput name='title' placeholder='Title' />
                <MyTextArea rows={3} placeholder='Description' name='description' />            
                <MySelectInput options={categoryOptions} placeholder='Category' name='category' />            
                <MyDateInput 
                  placeholderText='Date' 
                  name='date'
                  showTimeSelect
                  timeCaption='time'
                  dateFormat='MMMM d, yyyy h:mm aa' />            
                <Header content='Location Details' sub color='teal' />
                <MyTextInput placeholder='City' name='city' />            
                <MyTextInput placeholder='Venue' name='venue' />            

                <Button
                  disabled={!dirty || !isValid || isSubmitting} 
                  loading={loading} 
                  floated='right' 
                  positive 
                  type='submit' 
                  content='Submit' />
                <Button floated='right' type='button' content='Cancel' as={Link} to='/activities' />
            </Form>
          )}
        </Formik>
    </Segment>
    </>
  )
})
