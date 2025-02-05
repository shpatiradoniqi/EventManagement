﻿import React from 'react';
import { Grid, GridColumn, Icon, Segment } from 'semantic-ui-react';
import { IActivity } from '../../../app/models/activity';
import { format } from 'date-fns';

const ActivityDetailedInfo: React.FC<{ activity: IActivity }> = ({activity}) => {
    return (
        <Segment.Group>
            <Segment attached='top'>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size="large" color="teal" name="info" />
                    </Grid.Column>
                    <Grid.Column width={15}>
                        <p>{activity.description}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid verticalAlign="middle">
                    <Grid.Column>
                    <Icon name='calendar' size='large' color='teal' />
                    </Grid.Column>
                    <Grid.Column width={15}>
                        <span>{format(activity.date, 'eeee do MMMM')} at  {format(activity.date, 'dd MMM yyyy h:mm aa')} </span>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='marker' size='large' color='teal' />
                    </Grid.Column>
                    <span>{activity.venue}, {activity.city}</span>
                  </Grid>
            </Segment>
        </Segment.Group>

    )
}

export default ActivityDetailedInfo;
