import { FORM_ERROR } from 'final-form';
import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { Form as FinalForm, Field } from 'react-final-form';
import { combineValidators, isRequired } from 'revalidate';
import { Button, Header, Form, Divider } from 'semantic-ui-react';
import TextInput from '../../app/common/form/TextInput';
import { IUserFormValues } from '../../app/models/user';
import { RootStoreContext } from '../../app/stores/rootStore';
import ErrorMessage from '../../components/ErrorMessage';
import SocialLogin from './SocialLogin';

const validate = combineValidators({
    email: isRequired('email'),
    password: isRequired('password')
})

const LoginForm = () => {
    const rootStore = useContext(RootStoreContext);
    const { login,fbLogin, loading } = rootStore.userStore;
    return (
        <FinalForm
            onSubmit={(values: IUserFormValues) => login(values).catch(error => ({
                [FORM_ERROR]: error
            }))}
            validate={validate}
            render={({
                handleSubmit,
                submitting,
                submitError,
                invalid,
                pristine,
                dirtySinceLastSubmit
            }) => (
                    <Form onSubmit={handleSubmit} error>
                        <Header as='h2' content='Login to Activities' color='teal' />
                        <Field name='email' component={TextInput} placeholder='Email' />
                        <Field
                            name='password'
                            component={TextInput}
                            placeholder='Password'
                            type='password'
                        />
                        {submitError && !dirtySinceLastSubmit && (
                            <ErrorMessage error={submitError} text='Invalid email or password' />
                        )}
                        <Button
                            disabled={invalid && !dirtySinceLastSubmit || pristine}
                            loading={submitting}
                            color='teal'
                            content='Login'
                            fluid
                    />
                    <Divider horizontal>Or</Divider>
                    <SocialLogin loading={loading} fbCallback={fbLogin} />
                    </Form>
                )}
        />
    )
}

export default observer(LoginForm);