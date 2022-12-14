import { useEffect } from 'react'
import { useParams } from 'react-router-dom';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { Grid } from 'semantic-ui-react';
import ActivityDetailedHeader from './ActivityDetailedHeader';
import ActivityDetailedInfo from './ActivityDetailedInfo';
import ActivityDetailedChat from './ActivityDetailedChat';
import ActivityDetailedSidebar from './ActivityDetailedSidebar';
import NavBar from '../../../app/layout/NavBar';

export default observer(function ActivityDetails() {

  const { activityStore } = useStore();

  const { selectedActivity: activity, loadActivity, loadingInitial, clearSelectedActivity } = activityStore;

  const { id } = useParams<'id'>();

  useEffect(() => {

    if(id) loadActivity(id);

    return () => clearSelectedActivity();

  }, [id, loadActivity, clearSelectedActivity]);

  if(loadingInitial || !activity) return <LoadingComponent content='Loading activity...' />;

  return (
    <>
      <NavBar />
      <Grid>

        <Grid.Column width={10}>
          <ActivityDetailedHeader activity={activity} />
          <ActivityDetailedInfo activity={activity} />
          <ActivityDetailedChat activityId={activity.id} />
        </Grid.Column>

        <Grid.Column width={6}>
          <ActivityDetailedSidebar activity={activity} />
        </Grid.Column>

      </Grid>
    </>
  )
});
