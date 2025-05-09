import {Group } from "@mui/icons-material";
import { AppBar, Box, Container, MenuItem, Toolbar, Typography } from "@mui/material";
import MenuItemLink from "../shared/components/MenuItemLink";


export default function NavBar() {
  return (
    <Box sx={{flexGrow: 1}}>
        <AppBar position="static" sx={{ 
        backgroundImage: 'linear-gradient(135deg,rgb(33, 115, 24) 0%,rgb(33, 174, 118) 69%,rgb(32, 142, 172) 89%)' 
        }}>
            <Container maxWidth="xl">
                <Toolbar sx={{display: 'flex', justifyContent: 'space-between'}}>
                    <Box>
                        <MenuItem sx={{display: 'flex', gap: 2}}>
                        <Group fontSize="large"></Group>
                        <Typography variant="h5" fontWeight='bold'>VET</Typography>
                        </MenuItem>
                    </Box>
                    <Box sx={{display: "flex"}}>
                        <MenuItemLink  to='/pet'>
                            Pets
                        </MenuItemLink>
                        <MenuItemLink  to={"#"}>
                            Medicine
                        </MenuItemLink>
                        <MenuItemLink  to={"#"}>
                            Diseases
                        </MenuItemLink>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    </Box>
  )
}
