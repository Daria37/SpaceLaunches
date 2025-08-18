import { Button, TextInput, Title } from '@mantine/core';
import { useForm } from '@mantine/form';
import { useAppDispatch } from '../../store/store';
import { login } from '../../Services/auth';
import { setCredentials } from '../../store/slices/authSlice';

export const LoginPage = () => {
  const dispatch = useAppDispatch();

  const form = useForm({
    initialValues: { email: '', password: '' },
  });

  const handleSubmit = async (values: { email: string; password: string }) => {
    try {
      const data = await login(values.email, values.password);
      dispatch(setCredentials({ token: data.token, role: data.role }));
    } catch (error) {
      console.error('Login failed:', error);
    }
  };

  return (
    <div>
      <Title>Login</Title>
      <form onSubmit={form.onSubmit(handleSubmit)}>
        <TextInput label="Email" {...form.getInputProps('email')} />
        <TextInput label="Password" type="password" {...form.getInputProps('password')} />
        <Button type="submit">Login</Button>
      </form>
    </div>
  );
};