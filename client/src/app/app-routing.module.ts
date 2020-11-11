import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListCommandComponent } from './command/list-command/list-command.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { AboutComponent } from './shared/components/about/about.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  {path: '', component: HomeComponent },
  {path: 'command',
    loadChildren: () => import('./command/command.module').then(m => m.CommandModule),
  },
  {path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule),
  },
  {path: 'about',
    component: AboutComponent
  },
  {path: 'not-found',
    component: NotFoundComponent
  },
  {path: '**', redirectTo: 'not-found', pathMatch: 'full', },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
