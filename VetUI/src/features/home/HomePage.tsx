import { Group } from "@mui/icons-material";
import { Box, Button, Paper, Typography } from "@mui/material";
import { Link } from "react-router";

export default function HomePage() {
  return (
    <Paper
      sx={{
        color: 'white',
        display: 'flex',
        flexDirection: 'column',
        gap: 6,
        alignItems: 'center',
        alignContent: 'center',
        justifyContent: 'center',
        height : '100vh',
        backgroundImage: 'linear-gradient(135deg,rgb(51, 115, 24) 0%,rgb(33, 174, 80) 69%,rgb(32, 172, 125) 89%)' 
      }}
    >
      <Box sx={{
        display: 'flex', 
        alignItems: 'center', 
        alignContent:'center', 
        color:'white', gap: 3
        }}>
          <Group sx={{height: 110, width:110}}></Group>
          <Typography variant="h1">
              Vet
          </Typography>
      </Box>
      <Typography variant="h2">
        Welcome to your Vet
      </Typography>
        <Button 
        component={Link} 
        to='/pet' 
        size="large" 
        variant="contained" 
        sx={{
          height:80, 
          borderRadius:4,
          fontSize:'1.5rem'}}
          >
        GO!
        </Button>
    </Paper>
  )
}
