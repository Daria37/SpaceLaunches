import { Grid } from '@mantine/core';
import { LaunchesTable } from '../../Pages/Dashboard/LaunchesTable';
import { Charts } from '../../Pages/Dashboard/Charts';

export const DashboardPage = () => {
  return (
    <Grid>
      <Grid.Col span={12}>
        <LaunchesTable />
      </Grid.Col>
      <Grid.Col span={6}>
        <Charts type="byYear" />
      </Grid.Col>
      <Grid.Col span={6}>
        <Charts type="byCountry" />
      </Grid.Col>
    </Grid>
  );
};