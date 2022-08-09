import ReactDOM from 'react-dom/client'
import { unstable_HistoryRouter as HistoryRouter } from 'react-router-dom'
import App from './app/layout/App'
import 'react-calendar/dist/Calendar.css'
import 'react-toastify/dist/ReactToastify.min.css'
import 'react-datepicker/dist/react-datepicker.css'
import './app/layout/styles.css'
import { store, StoreContext } from './app/stores/store'
import { createBrowserHistory } from 'history'
import ScrollToTop from './app/layout/ScrollToTop'

export const history = createBrowserHistory({ window });

ReactDOM.createRoot(document.getElementById('root')!).render(
  <StoreContext.Provider value={store}>
    <HistoryRouter history={history}>
      <ScrollToTop />
      <App />
    </HistoryRouter>
  </StoreContext.Provider>
);

