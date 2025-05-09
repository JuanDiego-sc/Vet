import { Box, Container, CssBaseline } from '@mui/material'
import NavBar from './layout/NavBar'
import { Outlet } from 'react-router'
import HomePage from '../features/home/HomePage'

function App() {


  return (
    <Box sx={{bgcolor: "#eeeeee", minHeight: '100vh'}}>
    <CssBaseline />
    {location.pathname === '/' ? <HomePage></HomePage> : (
      <>
        <NavBar></NavBar>
        <Container maxWidth='xl' sx={{marginTop:3}}>
        <Outlet></Outlet>
        </Container>
      </>
    )};
    </Box>
  )
}

export default App
