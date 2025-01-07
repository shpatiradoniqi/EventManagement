import React from 'react';
import { Segment, Button, Header, Icon, SegmentInline } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

const NotFound = () => {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name='search' />
                Oops we've looked everywhere but couldn't find this.
            </Header>
            <SegmentInline>
                <Button as={Link} to='/activities' primary>
                    Return to Activities page
                </Button>
            </SegmentInline>
        </Segment>
    );
};

export default NotFound;