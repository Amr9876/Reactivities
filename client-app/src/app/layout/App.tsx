import { Container } from 'semantic-ui-react'
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import { observer } from 'mobx-react-lite';
import { Route, Routes, useLocation } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import ActivityForm from '../../features/activities/form/ActivityForm';
import ActivityDetails from '../../features/activities/details/ActivityDetails';
import { ToastContainer } from 'react-toastify';
import NotFound from '../../features/errors/NotFound';
import ServerError from '../../features/errors/ServerError';
import { useStore } from '../stores/store';
import { CSSProperties, FunctionComponent, useEffect } from 'react';
import LoadingComponent from './LoadingComponent';
import ModalContainer from '../common/modals/ModalContainer';
import ProfilePage from '../../features/profiles/ProfilePage';

interface VerifyProps {
  component: FunctionComponent
}

function App() {

  const location = useLocation();
  const { commonStore, userStore } = useStore();

  const { isLoggedIn } = userStore;

  const errorMessageStyles: CSSProperties = {
    color: 'red',
    textAlign: 'center',
    marginTop: '1rem'
  }

  useEffect(() => {
    if(commonStore.token) {
      userStore.getUser().finally(() => commonStore.setAppLoaded());
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore])

  if(!commonStore.appLoaded) return <LoadingComponent content='Loading app...' />

  const Verify = ({ component: Component }: VerifyProps) => 
    isLoggedIn ? <Component /> : <h2 style={errorMessageStyles}>Your not logged in!</h2>

  return (
    <>

      <ToastContainer position='bottom-right' hideProgressBar />
      <ModalContainer />

      <Routes>
        <Route path='/' element={<HomePage />} />
      </Routes>
      
      <Container style={{ marginTop: '7em' }}>
        <Routes>
          <Route path='/activities' element={<Verify component={ActivityDashboard} />} />
          <Route path='/activities/:id' element={<Verify component={ActivityDetails} />} />
          <Route key={location.key} path='/createActivity' element={<Verify component={ActivityForm} />} />
          <Route key={location.key} path='/manage/:id' element={<Verify component={ActivityForm} />} />
          <Route path='/profiles/:username' element={<Verify component={ProfilePage} />} />
          <Route path='/server-error' element={<ServerError />} />
          <Route element={<NotFound />} />
        </Routes>
      </Container>

    </>
  )
}

export default observer(App);
