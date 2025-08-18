import { AppShell, Button, Title, rem } from '@mantine/core';
import { Outlet } from 'react-router-dom';
import { useAppDispatch } from '../../store/store';
import { logout } from '../../store/slices/authSlice';

export const Layout = () => {
  const dispatch = useAppDispatch();

  const handleLogout = () => {
    dispatch(logout()); // Теперь корректно типизировано
  };

  return (
    <AppShell
      navbar={{ width: 300, breakpoint: 'sm' }}
      header={{ height: 60 }}
      padding="md"
    >
      <AppShell.Navbar p="md">
        <AppShell.Section grow>
          <Button 
            fullWidth 
            variant="subtle"
            component="a" 
            href="/"
            mb="sm"
          >
            Dashboard
          </Button>
          <Button 
            fullWidth 
            variant="subtle"
            component="a" 
            href="/admin"
          >
            Admin
          </Button>
        </AppShell.Section>
        <AppShell.Section>
          <Button 
            fullWidth 
            onClick={handleLogout}
            variant="light"
            color="red"
          >
            Logout
          </Button>
        </AppShell.Section>
      </AppShell.Navbar>

      <AppShell.Header p="xs">
        <Title order={3} style={{ lineHeight: rem(60) }}>
          Space Launches
        </Title>
      </AppShell.Header>

      <AppShell.Main>
        <Outlet />
      </AppShell.Main>
    </AppShell>
  );
};

// export const Layout = () => {
//   const dispatch = useAppDispatch();

//   const handleLogout = () => {
//     dispatch(logout());
//   };

//   return (
//     <AppShell
//       navbar={
//         <Navbar width={{ base: 300 }} p="md">
//           <Navbar.Section>
//             <Button 
//               fullWidth 
//               variant="subtle"
//               component="a" 
//               href="/" // Исправляем навигацию
//             >
//               Dashboard
//             </Button>
//             <Button 
//               fullWidth 
//               variant="subtle"
//               component="a" 
//               href="/admin"
//             >
//               Admin
//             </Button>
//           </Navbar.Section>
//           <Navbar.Section mt="auto">
//             <Button 
//               fullWidth 
//               onClick={handleLogout}
//               variant="light"
//               color="red"
//             >
//               Logout
//             </Button>
//           </Navbar.Section>
//         </Navbar>
//       }
//       header={
//         <Header height={60} p="xs">
//           <Title order={3} sx={{ padding: '12px 0' }}>
//             Space Launches
//           </Title>
//         </Header>
//       }
//       padding="md"
//     >
//       <Outlet />
//     </AppShell>
//   );
// };