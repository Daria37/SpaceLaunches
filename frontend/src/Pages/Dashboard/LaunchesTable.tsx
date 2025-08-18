import { Table } from '@mantine/core';
import { useAppSelector } from '../../store/store';

export const LaunchesTable = () => {
  const { launches } = useAppSelector((state) => state.launches);

  return (
    <Table>
      <thead>
        <tr>
          <th>Mission</th>
          <th>Date</th>
          <th>Rocket</th>
        </tr>
      </thead>
      <tbody>
        {launches.map((launch) => (
          <tr key={launch.id}>
            <td>{launch.name}</td>
            <td>{launch.date}</td>
            <td>{launch.rocket}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};