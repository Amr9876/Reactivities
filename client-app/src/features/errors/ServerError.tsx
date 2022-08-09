import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Container, Header, Segment } from "semantic-ui-react";
import { ServerError } from "../../app/models/serverError";
import { useStore } from "../../app/stores/store";

export default observer(function ServerError() {

  const [error, setError] = useState<ServerError>(() => {
    const data = sessionStorage.getItem("error");
    return JSON.parse(data!);
  });

  return (
    <Container>
        <Header as='h1' content="Server Error" />
        <Header sub as='h5' color='red' content={error.message} />
        <Segment>
            <Header as='h4' content="Stack trace" color='teal' />
            <span style={{marginTop: '10px'}}>{error.details}</span>
        </Segment>
    </Container>
  )
})
