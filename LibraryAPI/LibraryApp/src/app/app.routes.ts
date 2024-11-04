import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login/login.component';
import { HomepageComponent } from './pages/homepage/homepage/homepage.component';
import { AdminLoginComponent } from './pages/login/admin-login/admin-login.component';
import { CreateAccountComponent } from './pages/login/create-account/create-account.component';

export const routes: Routes = [

    {
        path: '',
        redirectTo: "/login",
        pathMatch: 'full'
    },
    {
        path: 'login',
        children: [
            {
                path: '', 
                component: LoginComponent, 
                pathMatch: 'full'
            },
            {
                path: 'admin',
                component: AdminLoginComponent,
                pathMatch: 'full'
            },
            {
                path: 'new-account',
                component: CreateAccountComponent,
                pathMatch: 'full'
            },
        ]
    },
    
    {
        path: 'homepage',
        children: [
            {
                path: '',
                component: HomepageComponent,
            },
            {
                path: 'true',
                redirectTo: '',
                pathMatch: 'full',
            },
            {
                path: 'false',
                redirectTo: "login",
                pathMatch: 'full',
            }
        ]
    }
];
