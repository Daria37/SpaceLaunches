import { Notifications } from '@mantine/notifications';

export const handleApiError = (error: unknown) => {
  const message = error instanceof Error ? error.message : 'Unknown error';
  Notifications.show({ title: 'Error', message, color: 'red' });
};