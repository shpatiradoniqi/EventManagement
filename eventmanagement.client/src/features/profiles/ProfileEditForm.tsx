import React, { useContext, useState } from 'react';
import { RootStoreContext } from '../../app/stores/rootStore';
import { Tab, Grid, Header, Button, Form } from 'semantic-ui-react';
import { combineValidators, isRequired } from 'revalidate';
import { IProfile } from '../../app/models/profile';
import { Form as FinalForm, Field } from 'react-final-form';
import TextAreaInput from '../../app/common/form/TextAreaInput';
import TextInput from '../../app/common/form/TextInput';
import { observer } from 'mobx-react-lite';

const validate = combineValidators({
    displayName: isRequired('displayName')
});

interface IProps {
    updateProfile: (profile: IProfile) => void;
    profile: IProfile;

}

const ProfileEditForm: React.FC<IProps> = ({ updateProfile, profile }) => {
    return (
        <FinalForm
            onSubmit={updateProfile}
            validate={validate}
            initialValues={profile!}
            render={({ handleSubmit, invalid, pristine, submitting }) => (
                <Form onSubmit={handleSubmit} error>
                    <Field
                        name='displayName'
                        placeholder='displayName'
                        value={profile.displayName}
                        component={TextInput}
                    />
                    <Field
                        name='bio'
                        placeholder='bio'
                        rows={3}
                        value={profile.bio}
                        component={TextAreaInput}
                    />
                    <Button
                        loading={submitting}
                        floated='right'
                        disabled={invalid || pristine}
                        positive
                        content='Update Profile'
                    />
                </Form>
            )}
        />
    );
}
export default observer(ProfileEditForm);