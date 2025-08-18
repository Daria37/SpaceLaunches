import { useEffect, useState } from 'react';
import { api } from '../../Services/api';
import { BarChart } from '@mantine/charts';

interface ChartDataItem {
  name: string;
  launches: number;
}

interface ChartsProps {
  type: 'byYear' | 'byCountry';
}

export const Charts = ({ type }: ChartsProps) => {
  const [data, setData] = useState<ChartDataItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const response = await api.get(`/launches/stats?type=${type}`);
        
        const chartData = response.data.map((item: { key: string; count: number }) => ({
          name: type === 'byYear' ? `Year ${item.key}` : item.key,
          launches: item.count,
        }));

        setData(chartData);
      } catch (err) {
        setError('Failed to load chart data');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [type]);

  if (loading) return <div>Loading chart data...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <BarChart
      data={data}
      dataKey="name"
      series={[{ name: 'launches', color: 'blue' }]}
      h={300}
      tickLine="y"
      withTooltip
    />
  );
};