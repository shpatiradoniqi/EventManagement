﻿import React from 'react';
import { Tab } from 'semantic-ui-react';
import ProfilePhotos from './ProfilePhotos';
import ProfileDescription from './ProfileDescription';
import { observer } from 'mobx-react-lite';
import ProfileFollowing from './ProfileFollowings';
import ProfileActivities from './ProfileActivities';

const panes = [
    { menuItem: 'About', render: () => <ProfileDescription />},
    { menuItem: 'Photos', render: () => <ProfilePhotos /> },
    { menuItem: 'Activities', render: () => <ProfileActivities /> },
    { menuItem: 'Followers', render: () => <ProfileFollowing/> },
    { menuItem: 'Following', render: () => <ProfileFollowing /> }
]

interface IProps {
    setActiveTab: (activeIndex: any) => void
}

const ProfileContent: React.FC<IProps> = ({ setActiveTab }) => {
    return (
        <Tab
            menu={{ fluid: true, vertical: true }}
            menuPosition='right'
            panes={panes}
            onTabChange={(e, data) => setActiveTab(data.activeIndex)}
        />
    )
}

export default observer(ProfileContent)