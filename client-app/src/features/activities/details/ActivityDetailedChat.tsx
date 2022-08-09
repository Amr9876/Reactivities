import { Formik, Form } from 'formik';
import { observer } from 'mobx-react-lite'
import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Segment, Header, Comment, Button } from 'semantic-ui-react'
import MyTextArea from '../../../app/common/form/MyTextArea';
import { useStore } from '../../../app/stores/store';
import * as Yup from 'yup';
import { formatDistanceToNow } from 'date-fns';

interface Props {
    activityId: string;

}

export default observer(function ActivityDetailedChat({ activityId }: Props) {

    const { commentStore } = useStore();

    const { createHubConnection, clearComments, comments, addComment } = commentStore;

    useEffect(() => {
        if(activityId) {
            createHubConnection(activityId);
        }

        return () => {
            clearComments();
        }
    }, [commentStore, activityId]);

    return (
        <>
            <Segment
                textAlign='center'
                attached='top'
                inverted
                color='teal'
                style={{ border: 'no1ne' }}
            >
                <Header>Chat about this event</Header>
            </Segment>
            <Segment attached clearing>
                <Comment.Group>
                    <Formik
                        onSubmit={(values, { resetForm }) =>
                            addComment(values).then(() => resetForm())}
                        initialValues={{ body: '' }}
                        validationSchema={Yup.object({
                            body: Yup.string().required()
                        })}
                    >
                        {({ isSubmitting, isValid }) => (
                            <Form className='ui form'>
                                <MyTextArea placeholder='Add comment'
                                    name='body'
                                    rows={2} />
                                <Button
                                    loading={isSubmitting}
                                    disabled={isSubmitting || !isValid}
                                    content='Add Reply'
                                    labelPosition='left'
                                    icon='edit'
                                    primary
                                    type='submit'
                                    floated='right'
                                />
                            </Form>
                        )}
                    </Formik>
                    
                    {comments.map(comment => (
                        <Comment key={comment.id}>
                            <Comment.Avatar src={comment.image || '/assets/user.png'} />
                            <Comment.Content>
                                <Comment.Author as={Link} to={`/profiles/${comment.username}`}>
                                    {comment.displayName}
                                </Comment.Author>
                                <Comment.Metadata>
                                    <div>{formatDistanceToNow(comment.createdAt)} ago</div>
                                </Comment.Metadata>
                                <Comment.Text style={{whiteSpace: 'pre-wrap'}}>{comment.body}</Comment.Text>
                            </Comment.Content>
                        </Comment>
                    ))}
                </Comment.Group>

            </Segment>
        </>

    )
})